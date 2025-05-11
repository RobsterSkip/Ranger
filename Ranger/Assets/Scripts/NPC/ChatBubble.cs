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

    [SerializeField] private GameObject[] _collectibles;

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

    private JournalEntries _journalEntries;
    private GameObject _journalEntry;

    [SerializeField]
    private LayerMask _playerMask;

    private Vector2 padding = new Vector2(-1f, 7f);

    private bool _itemGiving;
    public bool _npcInventory;

    private string _gameObjectName;

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

        _journalEntry = GameObject.FindGameObjectWithTag("Finish");
        _journalEntries = _journalEntry.GetComponent<JournalEntries>();
        
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
                    _gameObjectName = droppedItem.ToString();
                    string droppedName = droppedItem.name.Replace("(Clone)", "").Trim().ToLower();
                    string collectibleNameCarp = _collectibles[6].name.Trim().ToLower();
                    string collectibleNamePerch = _collectibles[7].name.Trim().ToLower();
                    string collectibleNameTrout = _collectibles[8].name.Trim().ToLower();

                    Manager.Inventory.SetActive(false);
                  _cameraMovement._inventoryOpen = false;
                  _npcInventory = false;

                  SetUp(IconType.Fish, "Thank you for the fish!!");
                  _textMeshPro.ForceMeshUpdate();

                    if (droppedName == collectibleNameCarp)
                    {
                        _journalEntries._enable7 = true;
                    }
                    else if (droppedName == collectibleNamePerch)
                    {
                        _journalEntries._enable8 = true;
                    }
                    else if (droppedName == collectibleNameTrout)
                    {
                        _journalEntries._enable9 = true;
                    }

                    Destroy(droppedItem.gameObject);
                    _itemGiving = false;
                }

               else if (droppedItem.CompareTag("bugDropped"))
               {
                    _gameObjectName = droppedItem.ToString();
                    string droppedName = droppedItem.name.Replace("(Clone)", "").Trim().ToLower();
                    string collectibleNameMoth = _collectibles[0].name.Trim().ToLower();
                    string collectibleNameCatterpillar = _collectibles[1].name.Trim().ToLower();
                    string collectibleNameLadybug = _collectibles[2].name.Trim().ToLower();

                    Manager.Inventory.SetActive(false);
                   _cameraMovement._inventoryOpen = false;
                   _npcInventory = false;

                   SetUp(IconType.Bug, "Thank you for the bug!!");
                   _textMeshPro.ForceMeshUpdate();
                    if(droppedName == collectibleNameMoth)
                    {
                        _journalEntries._enable1 = true;
                    }
                    else if (droppedName == collectibleNameLadybug)
                    {
                        _journalEntries._enable2 = true;
                    }
                    else if(droppedName == collectibleNameCatterpillar)
                    {
                        _journalEntries._enable3 = true;
                    }

                    Destroy(droppedItem.gameObject);
                    _itemGiving = false;
                }

               else if (droppedItem.CompareTag("PlantDropped"))
               {
                    _gameObjectName = droppedItem.ToString();
                    string droppedName = droppedItem.name.Replace("(Clone)", "").Trim().ToLower();
                    string collectibleNameWildflower = _collectibles[3].name.Trim().ToLower();
                    string collectibleNameSunflower = _collectibles[4].name.Trim().ToLower();
                    string collectibleNameForgetmenot = _collectibles[5].name.Trim().ToLower();

                    Debug.Log(_gameObjectName);

                    Manager.Inventory.SetActive(false);
                   _cameraMovement._inventoryOpen = false;
                   _npcInventory = false;

                   SetUp(IconType.Plant, "Thank you for the plant!!");
                   _textMeshPro.ForceMeshUpdate();

                    if (droppedName == collectibleNameWildflower)
                    {
                        _journalEntries._enable4 = true;
                    }
                    else if (droppedName == collectibleNameSunflower)
                    {
                        _journalEntries._enable5 = true;
                    }
                    else if (droppedName == collectibleNameForgetmenot)
                    {
                        _journalEntries._enable6 = true;
                    }

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