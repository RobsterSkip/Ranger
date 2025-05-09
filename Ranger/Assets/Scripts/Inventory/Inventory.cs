
using System.Collections.Generic;
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

    public bool _isDropped = false;

    private GameObject _panel;

    private Inventory _inventoryClass;
    public GameObject _inventory;

    private void Start()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
       {
            _itemSlots[i].OnRightClickEvent += ItemDropped;
       }
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
        float spawnPointX = Random.Range(-4f, 0);
        float spawnPointZ = Random.Range(-4f, 0);

        Vector3 dropPosition = new Vector3(_player.transform.localPosition.x + spawnPointX, _player.transform.localPosition.y + 0.5f, 
            _player.transform.localPosition.z + spawnPointZ);
        Instantiate(item.ItemPrefab, dropPosition, Quaternion.identity);
        _items.Remove(item);
        Destroy(item);     
        RefreshUI();
    }

    public bool AddItem(Items item)
    {
        if (IsFull())
        {
            return false;
        }

        _items.Add(item);
        RefreshUI();
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
            if(item != null && item._ID == itemID)
            {
                _itemSlots[i].Item = null;
                return item;
            }
        }
        return null;
    }
    
    public bool IsFull()
    {
        for (int i = 0; i < _itemSlots.Length;i++)
        {
            if (_itemSlots[i].Item == null)
            {
                return false;
            }
        }
        return true;
    }
    
    public int ItemCount(string itemID)
    {
        int number = 0;

        for (int i = 0; i < _itemSlots.Length; ++i)
        {
            if (_itemSlots[i].Item._ID == itemID)
            {
                number++;
            }
        }
        return number;
    }
}