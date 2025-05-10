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
       }
       else if(Input.GetKeyUp(KeyCode.Escape))
       {
           _inventory.SetActive(false);
       }
    }
}
