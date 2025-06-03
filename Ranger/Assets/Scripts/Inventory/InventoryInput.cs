using TMPro;
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject _inventory;

    Journal _journalClass;
    [SerializeField] private GameObject _journal;
    [SerializeField] private TextMeshProUGUI _escapeText;
    [SerializeField] private TextMeshProUGUI _weightText;

    public bool _inventoryOpen;

    private void Start()
    {
        _journal = GameObject.FindGameObjectWithTag("Journal");
        _journalClass = _journal.GetComponent<Journal>();
        _escapeText.enabled = false;
        _weightText.enabled = false;
    }

    private void Update()
    {
        if (BooleanManager._everythingCollected == false)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && _inventoryOpen == false && _journalClass._journalOpen == false)
            {
                _inventory.SetActive(true);
                _inventoryOpen = true;
                _journalClass._journalOpen = true;
                _escapeText.enabled = true;
                _weightText.enabled = true;
            }
            else if (_inventoryOpen == true && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)))
            {
                _inventory.SetActive(false);
                _inventoryOpen = false;
                _journalClass._journalOpen = false;
                _escapeText.enabled = false;
                _weightText.enabled = false;
            }
            else if (_inventoryOpen == true && Input.GetMouseButtonUp(0))
            {
                _escapeText.enabled = false;
                _weightText.enabled = false;
            }

            if (_inventoryOpen == false)
            {
                _escapeText.enabled = false;
                _weightText.enabled = false;
            }
        }
    }
}