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

    private bool _isInRange;

    private void Update()
    {
        if (_isInRange && Input.GetKeyDown(KeyCode.E))
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
