using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private Collider[] _colliders;
    private float _interactRange = 2f;
    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.F))
       {
           _colliders = Physics.OverlapSphere(transform.position, _interactRange);
           foreach (Collider collider in _colliders)
           {
               if(collider.TryGetComponent(out NPCInteractable npcInteractable))
               {
                   npcInteractable.Interact();
               }
           }

       }


    }
}
