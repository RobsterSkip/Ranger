using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject _inventory;
   // private ChatBubble _chatBubble;
   // public GameObject _bubble;

   // private void Start()
   // {
   //     _bubble = GameObject.FindGameObjectWithTag("ChatBubble");
   //     _chatBubble = _bubble.GetComponent<ChatBubble>();
   //     _bubble.SetActive(false);
   // }
    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Tab))
       {
           _inventory.SetActive(!_inventory.activeSelf);
       }
    }
}
