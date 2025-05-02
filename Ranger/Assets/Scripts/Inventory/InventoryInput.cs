using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    private KeyCode _inventoryToggleKeys = KeyCode.Tab;
    [SerializeField] GameObject _inventory;

    private void Update()
    {
       if (Input.GetKeyDown(_inventoryToggleKeys))
       {
           _inventory.SetActive(!_inventory.activeSelf);
       }
    }
}
