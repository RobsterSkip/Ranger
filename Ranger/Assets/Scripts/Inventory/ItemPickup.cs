using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Items _item;

    public GameObject Player;

    public GameObject Inventory;
    public InventoryManager Manager;

    private bool _isInRange;

    private void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("InventoryManager");
        Manager = Inventory.GetComponent<InventoryManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (_isInRange && Input.GetKeyDown(KeyCode.E))
        {
            Items newItem = Instantiate(_item);
            if (Manager.InventoryScript.AddItem(newItem))
            {
                BooleanManager.IsItemPicked = true;
                Destroy(gameObject);
                Manager.PickupPanel.SetActive(false);
            }
            else
            {
                Destroy(newItem);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
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