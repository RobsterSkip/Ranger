
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Items> _items;
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ItemSlots[] _itemSlots;
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _prefabs;
    [SerializeField] private GameObject _itemDroppedText;
    [SerializeField] private TextMeshProUGUI _pickupPanelText;

    public bool _isDropped = false;

    Journal _journalClass;
    private GameObject _journal;

    private CameraMovement _cameraMovement;
    private GameObject _camera;

    private float _itemDroppedTime = 1.5f;
    private float _itemDroppedTimeCounter;

    private bool _inventoryFull;

    private void Start()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].OnRightClickEvent += ItemDropped;
        }

        _journal = GameObject.FindGameObjectWithTag("Journal");
        _journalClass = _journal.GetComponent<Journal>();

        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _cameraMovement = _camera.GetComponent<CameraMovement>();
    }

    private void Update()
    {
        RefreshUI();
    }

    private void OnValidate()
    {
        if (_itemsParent != null)
        {
            _itemSlots = _itemsParent.GetComponentsInChildren<ItemSlots>();
        }
    }

    private void RefreshUI()
    {
        int i = 0;
        for (; i < _items.Count && i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Item = _items[i];
        }

        for (; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Item = null;
        }
    }

    private void ItemDropped(Items item)
    {
        Instantiate(item.ItemPrefab, _player.transform.position + _player.transform.forward * 2 + _player.transform.up, Quaternion.identity);
        _isDropped = true;

        _items.Remove(item);
        Destroy(item);     
        RefreshUI();

        _journalClass._journalOpen = false;
        _cameraMovement._inventoryOpen = false;
        gameObject.SetActive(false);

        //ItemDroppedText();
    }

    private void ItemDroppedText()
    {
        _itemDroppedText.SetActive(true);
        _itemDroppedTimeCounter += Time.deltaTime;
        if(_itemDroppedTimeCounter >= _itemDroppedTime)
        {
            _itemDroppedText.SetActive(false);
            _itemDroppedTimeCounter = 0;
        }
    }

    public bool AddItem(Items item)
    {
        if (IsFull() && _inventoryFull == true)
        {
            return false;
        }

        _items.Add(item);
        RefreshUI();
        return true;
    }

    public bool IsFull()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == null)
            {
                _inventoryFull = true;
                return false;
            }
            else
            {
                _inventoryFull = false;
            }
        }
        return true;
    }
    public bool RemoveItem(Items item)
    {
        for (int i = 0; i < _itemSlots.Length;  i++) 
        {
            if (_itemSlots[i].Item == item)
            {
                _itemSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }

    public Items RemoveItem(string itemID)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            Items item = _itemSlots[i].Item;
            if(item != null && item.ID == itemID)
            {
                _itemSlots[i].Item = null;
                return item;
            }
        }
        return null;
    }

    public int ItemCount(string itemID)
    {
        int number = 0;

        for (int i = 0; i < _itemSlots.Length; ++i)
        {
            if (_itemSlots[i].Item.ID == itemID)
            {
                number++;
            }
        }
        return number;
    }
}