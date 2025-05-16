using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;
    public Inventory InventoryScript;
    public GameObject PickupPanel;
    public TextMeshProUGUI CanFish;

    void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("Inventory");
        PickupPanel = GameObject.FindGameObjectWithTag("Panel");
        PickupPanel.SetActive(false);
        InventoryScript = Inventory.GetComponent<Inventory>();
        Inventory.SetActive(false);
        //CanFish.gameObject.SetActive(false);
    }
}