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
    private TextMeshPro _bubbleText;
    private Vector2 padding = new Vector2(-1f, 7f);

    public GameObject Inventory;
    public InventoryManager Manager; 

    public Journal Journal;
    public GameObject _journal;

    private JournalEntries _journalEntries;
    private GameObject _journalEntry;

    private CameraMovement _cameraMovement;
    [SerializeField] private GameObject _camera;

    [SerializeField] private LayerMask _playerMask;

    private bool _itemGiving;
    public bool _npcInventory;
    public bool _dialogueChanged;
    public bool _itemGiven;
    public bool _inventoryNPCExited;

    private float _chatRandomTimer = 3f;
    private float _chatRandomTimerCounter;

    private void Awake()
    {
        _backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        _iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        _bubbleText = transform.Find("Text").GetComponent<TextMeshPro>();

        _backgroundSpriteRenderer.enabled = false;
        _iconSpriteRenderer.enabled = false;
        _bubbleText.enabled = false;

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
        _bubbleText.SetText(text);
        _bubbleText.ForceMeshUpdate();

        Vector2 textSize = _bubbleText.GetRenderedValues(false);

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

    public void RandomText()
    {
        _backgroundSpriteRenderer.enabled = true;
        _iconSpriteRenderer.enabled = true;
        _bubbleText.enabled = true;
        if(_dialogueChanged == false && _inventoryNPCExited == false)
        {
            SetUp(IconType.Question, "Hello Ranger! \nDo you have anything for me?");
        }


        String[] _randomTexts = { "I heard that bugs like freshly-picked plants!", "Don't forget to check out the journal!", "Can't go fishing without any bait!" };
        System.Random randomString = new System.Random();
        int maxNumber = _randomTexts.Length;
        int newRandomNumber = randomString.Next(0, maxNumber);
        string result = _randomTexts[newRandomNumber];


        _chatRandomTimerCounter += Time.deltaTime;
        if (_chatRandomTimerCounter >= _chatRandomTimer && _itemGiven == false && _inventoryNPCExited == false)
        {
            _dialogueChanged = true;
            Debug.Log("Dialogue changed");
            SetUp(IconType.Question, result);
            _chatRandomTimerCounter = 0;
            _bubbleText.ForceMeshUpdate();
        }
    }

    public void ItemGiven()
    {
        if (_npcInventory == false && Input.GetKeyDown( KeyCode.E))
        {
            Manager.Inventory.SetActive(true);
            _cameraMovement._inventoryOpen = true;
            _itemGiving = true;
            _npcInventory = true;
        }
        else if (_npcInventory == true && _itemGiving == true && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)))
        {
            Manager.Inventory.SetActive(false);
            _cameraMovement._inventoryOpen = false;
            _npcInventory = false;

            SetUp(IconType.Question, "Goodbye! See you soon!");
            _inventoryNPCExited = true;
            _bubbleText.ForceMeshUpdate();

            _itemGiving = false;
        }

        if (Physics.CheckSphere(transform.parent.transform.position, 5f, _playerMask) && _itemGiving == true)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.parent.position, 5f);

            foreach (Collider col in hitColliders)
            {
                GameObject droppedItem = col.gameObject;
                string droppedName = droppedItem.name.Replace("(Clone)", "").Trim().ToLower();

                if (droppedItem.CompareTag("fish"))
                {
                    string collectibleNameCarp = _collectibles[6].name.Trim().ToLower();
                    string collectibleNamePerch = _collectibles[7].name.Trim().ToLower();
                    string collectibleNameTrout = _collectibles[8].name.Trim().ToLower();

                    Manager.Inventory.SetActive(false);
                    _cameraMovement._inventoryOpen = false;
                    _npcInventory = false;

                    SetUp(IconType.Fish, "Thank you for the fish!");
                    _bubbleText.ForceMeshUpdate();

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
                    _itemGiven = true;
                }

                else if (droppedItem.CompareTag("bugDropped"))
                {
                    string collectibleNameMoth = _collectibles[0].name.Trim().ToLower();
                    string collectibleNameCatterpillar = _collectibles[1].name.Trim().ToLower();
                    string collectibleNameLadybug = _collectibles[2].name.Trim().ToLower();

                    Manager.Inventory.SetActive(false);
                   _cameraMovement._inventoryOpen = false;
                   _npcInventory = false;

                   SetUp(IconType.Bug, "Thank you for the bug!");
                   _bubbleText.ForceMeshUpdate();
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
                    _itemGiven = true;
                }

                else if (droppedItem.CompareTag("PlantDropped"))
                {
                    string collectibleNameWildflower = _collectibles[3].name.Trim().ToLower();
                    string collectibleNameSunflower = _collectibles[4].name.Trim().ToLower();
                    string collectibleNameForgetmenot = _collectibles[5].name.Trim().ToLower();

                    Manager.Inventory.SetActive(false);
                   _cameraMovement._inventoryOpen = false;
                   _npcInventory = false;

                   SetUp(IconType.Plant, "Thank you for the plant!");
                   _bubbleText.ForceMeshUpdate();

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
                    _itemGiven = true;
                }
            }
        }
    }

    public void Remove()
    {
        _backgroundSpriteRenderer.enabled = false;
        _iconSpriteRenderer.enabled = false;
        _bubbleText.enabled = false;
    }
}