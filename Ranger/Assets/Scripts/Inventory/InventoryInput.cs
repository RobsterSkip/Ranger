using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject _inventory;

    Journal _journalClass;
    [SerializeField]
    private GameObject _journal;

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
          _journalClass._journalOpen = true;
       }
       else if(_journalClass._journalOpen == true && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab)))
       {
           _inventory.SetActive(false);
           _journalClass._journalOpen = false;
        }
    }
}
