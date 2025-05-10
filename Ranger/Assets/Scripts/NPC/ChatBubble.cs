using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class ChatBubble : MonoBehaviour
{
    public enum IconType
    {
        Fish,
        Plant,
        Bug,
        Question
    }
    [SerializeField] private Sprite _fishIcon;
    [SerializeField] private Sprite _bugIcon;
    [SerializeField] private Sprite _plantIcon;
    [SerializeField] private Sprite _questonIcon;

    private SpriteRenderer _backgroundSpriteRenderer;
    private SpriteRenderer _iconSpriteRenderer;
    private TextMeshPro _textMeshPro;

    public GameObject Inventory;
    public InventoryManager Manager; 
    public Journal Journal;
    private Items _items;
    private CameraMovement _cameraMovement;

    [SerializeField]
    private GameObject _camera;
    public GameObject _journal;

    [SerializeField]
    private LayerMask _playerMask;

    private Vector2 padding = new Vector2(-1f, 7f);

    private bool _itemGiving;
    public bool _npcInventory;

    private void Awake()
    {
        _backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        _iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        _textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();

        _backgroundSpriteRenderer.enabled = false;
        _iconSpriteRenderer.enabled = false;
        _textMeshPro.enabled = false;

        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _cameraMovement = _camera.GetComponent<CameraMovement>();

        _journal = GameObject.FindGameObjectWithTag("Journal");
        Journal = _journal.GetComponent<Journal>();
        
        Inventory = GameObject.FindGameObjectWithTag("InventoryManager");
        Manager = Inventory.GetComponent<InventoryManager>();
    }

    private void SetUp(IconType icon, string text)
    {
        _textMeshPro.SetText(text);
        _textMeshPro.ForceMeshUpdate();
        Vector2 textSize = _textMeshPro.GetRenderedValues(false);

        _backgroundSpriteRenderer.size = textSize + padding;

        _iconSpriteRenderer.sprite = GetIconSprite(icon);
    }

    private Sprite GetIconSprite(IconType icon)
    {
        switch (icon)
        {
            default: 
                case IconType.Fish: return _fishIcon;
                case IconType.Plant: return _plantIcon;
                case IconType.Bug:return _bugIcon;
                case IconType.Question:return _questonIcon;
        }
    }

    public void Create()
    {
        _backgroundSpriteRenderer.enabled = true;
        _iconSpriteRenderer.enabled = true;
        _textMeshPro.enabled = true;
        SetUp(IconType.Question, "Hello Ranger! \nDo you have anything for me?");
    }

    public void ItemGiven()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Manager.Inventory.SetActive(true);
            _cameraMovement._inventoryOpen = true;
            _itemGiving = true;
            _npcInventory = true;
        }
        
        if(Physics.CheckSphere(transform.parent.transform.position, 5f, _playerMask) && _itemGiving == true)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.parent.position, 5f);
            foreach (Collider col in hitColliders)
            {
                GameObject droppedItem = col.gameObject;

               if (droppedItem.CompareTag("fish"))
               {
                  Manager.Inventory.SetActive(false);
                  _cameraMovement._inventoryOpen = false;
                  _npcInventory = false;

                  SetUp(IconType.Fish, "Thank you for the fish!!");
                  _textMeshPro.ForceMeshUpdate();

                    Journal.ItemToJournal(droppedItem);
                    Destroy(droppedItem.gameObject);
                    _itemGiving = false;
                }

               else if (droppedItem.CompareTag("bug"))
               {
                    Manager.Inventory.SetActive(false);
                   _cameraMovement._inventoryOpen = false;
                   _npcInventory = false;

                   SetUp(IconType.Bug, "Thank you for the bug!!");
                   _textMeshPro.ForceMeshUpdate();

                    Journal.ItemToJournal(droppedItem);
                    Destroy(droppedItem.gameObject);
                    _itemGiving = false;
                }

               else if (droppedItem.CompareTag("PlantDropped"))
               {
                    Manager.Inventory.SetActive(false);
                   _cameraMovement._inventoryOpen = false;
                   _npcInventory = false;

                   SetUp(IconType.Plant, "Thank you for the plant!!");
                   _textMeshPro.ForceMeshUpdate();

                    Journal.ItemToJournal(droppedItem);
                    Destroy(droppedItem.gameObject);
                    _itemGiving = false;

                }
               else if (Input.GetKeyDown(KeyCode.Escape))
               {
                   Manager.Inventory.SetActive(false);
                   _cameraMovement._inventoryOpen = false;
                   _npcInventory = false;

                   SetUp(IconType.Question, "Goodbye! See you soon!");
                   _textMeshPro.ForceMeshUpdate();

                    _itemGiving = false;
                }
            }
        }
    }
    public void Remove()
    {
        _backgroundSpriteRenderer.enabled = false;
        _iconSpriteRenderer.enabled = false;
        _textMeshPro.enabled = false;
    }
}