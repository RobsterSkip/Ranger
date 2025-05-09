using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject _inventory;

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Tab))
       {
           _inventory.SetActive(!_inventory.activeSelf);
       }
    }
}
