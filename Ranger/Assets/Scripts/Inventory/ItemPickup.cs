using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] 
    private Items _item;
    [SerializeField]
    private GameObject _player;

    public GameObject Inventory;
    public InventoryManager Manager;

    private bool _isInRange;

    private void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("InventoryManager");
        Manager = Inventory.GetComponent<InventoryManager>();
    }

    private void Update()
    {
        if (_isInRange && Input.GetKeyDown(KeyCode.E))
        {
           if (Manager.InventoryScript.IsFull() == false)
           {
                Manager.InventoryScript.AddItem(Instantiate(_item));
                Destroy(gameObject);
                Manager.PickupPanel.SetActive(false);
           }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _isInRange = true;
            Manager.PickupPanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _isInRange = false;
        Manager.PickupPanel.SetActive(false);
    }
}