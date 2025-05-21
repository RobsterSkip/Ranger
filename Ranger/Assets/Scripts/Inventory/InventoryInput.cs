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
        if (Input.GetKeyDown(KeyCode.Tab) && _journalClass._journalOpen == false)
        {
           _inventory.SetActive(true);
            _inventoryOpen = true;
        }
        else if(_journalClass._journalOpen == false && Input.GetKeyDown(KeyCode.Escape) || _journalClass._journalOpen == false && Input.GetKeyDown(KeyCode.Tab))
        {
           _inventory.SetActive(false);
            _inventoryOpen = false;
        }
    }
}