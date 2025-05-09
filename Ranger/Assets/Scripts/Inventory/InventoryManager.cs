using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;
    public Inventory InventoryScript;
    public GameObject PickupPanel;

    void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("Inventory");
        PickupPanel = GameObject.FindGameObjectWithTag("Panel");
        PickupPanel.SetActive(false);
        //Debug.Log(Inventory);
        InventoryScript = Inventory.GetComponent<Inventory>();
        Inventory.SetActive(false);
    }

    void Update()
    {
        
    }
}
