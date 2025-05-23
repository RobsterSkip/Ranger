using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject _inventory;

    Journal _journalClass;
    [SerializeField] private GameObject _journal;

    public bool _inventoryOpen;

    private void Start()
    {
        _journal = GameObject.FindGameObjectWithTag("Journal");
        _journalClass = _journal.GetComponent<Journal>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && _inventoryOpen == false)
        {
            _inventory.SetActive(true);
            _inventoryOpen = true;
            _journalClass._journalOpen = true;
        }
        else if(_inventoryOpen == true && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)))
        {
           _inventory.SetActive(false);
            _inventoryOpen = false;
            _journalClass._journalOpen = false;
        }
    }
}