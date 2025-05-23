using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;
    public Inventory InventoryScript;
    public GameObject PickupPanel;
    public TextMeshProUGUI CanFish;

    public TextMeshProUGUI _pickupPanelText;

    void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("Inventory");
        PickupPanel = GameObject.FindGameObjectWithTag("Panel");
        PickupPanel.SetActive(false);
        InventoryScript = Inventory.GetComponent<Inventory>();
        SetPickupText();
        Inventory.SetActive(false);
    }

    private void Update()
    {
        InventoryScript.IsFull();
        SetPickupText();
    }

    void SetPickupText()
    {
        if (InventoryScript.InventoryFull)
        {
            _pickupPanelText.text = "Press E to pick up";
        }
        else
        {
            _pickupPanelText.text = "Inventory full!";
        }
    }
}