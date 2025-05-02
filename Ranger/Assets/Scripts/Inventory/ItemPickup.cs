using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] 
    private Items _item;
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private GameObject _pickupPanel;
    [SerializeField]
    private GameObject _player;

    private KeyCode _itemPickupCode = KeyCode.E;
    private bool _isInRange;

    private void Update()
    {
        if (_isInRange && Input.GetKeyDown(_itemPickupCode))
        {
           if (_inventory.IsFull() == false)
           {
                _inventory.AddItem(Instantiate(_item));
                Destroy(gameObject);
                _pickupPanel.SetActive(false);
           }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _isInRange = true;
            _pickupPanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _isInRange = false;
        _pickupPanel.SetActive(false);
    }
}
