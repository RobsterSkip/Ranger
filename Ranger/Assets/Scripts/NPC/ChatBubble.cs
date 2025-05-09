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

    private float _bubbleTimerCount = 1f;
    private float _bubbleTime;

    public GameObject Inventory;
    public InventoryManager Manager; 

    private CameraMovement _cameraMovement;
    [SerializeField]
    private GameObject _camera;
    private Items _items;

    private Vector2 padding = new Vector2(-1f, 7f);

    private bool _itemGiving;

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
            SetUp(IconType.Question, "Do you have anything to give me?");
            _textMeshPro.ForceMeshUpdate();
            Manager.Inventory.SetActive(true);
            _cameraMovement._inventoryOpen = true;
            _itemGiving = true;
        }

        if(_itemGiving == true)
        {
            //Debug.Log("Destroyed");
            //_backgroundSpriteRenderer.enabled = false;
            //_iconSpriteRenderer.enabled = false;
            //_textMeshPro.enabled = false;
            //_bubbleTime = 0;
        }

            SetUp(IconType.Question, "Do you have anything to give me?");
            _textMeshPro.ForceMeshUpdate();

            //Manager.Inventory.SetActive(true);
            //_cameraMovement._inventoryOpen = true; //////////////////////////// commented this not sure if it's needed but the game broke with this here
            _itemGiving = true;

        if (_itemGiving == true)
        {
            if (Input.GetMouseButtonDown(1) && Manager.InventoryScript._isDropped == true && tag == "bug")
            {
                Debug.Log("Dropped1");
                Manager.Inventory.SetActive(false);
                _cameraMovement._inventoryOpen = false;
                SetUp(IconType.Fish, "Thank you for the fish!!");
            }
            else if (Input.GetMouseButtonDown(1) && Manager.InventoryScript._isDropped == true && tag == "bug")
            {
                Debug.Log("Dropped2");
                Manager.Inventory.SetActive(false);
                _cameraMovement._inventoryOpen = false;
                SetUp(IconType.Fish, "Thank you for the bug!!");
            }
            else if (Input.GetMouseButtonDown(1) && Manager.InventoryScript._isDropped == true && tag == "PlantDropped")
            {
                Debug.Log("Dropped3");
                Manager.Inventory.SetActive(false);
                _cameraMovement._inventoryOpen = false;
                SetUp(IconType.Fish, "Thank you for the plant!!");
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Dropped4");
                Manager.Inventory.SetActive(false);
                _cameraMovement._inventoryOpen = false;
                SetUp(IconType.Question, "Goodbye! I hope to see you soon");
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