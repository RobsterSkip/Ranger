using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField]
    private CharacterController _player;

    private Quaternion _targetRotation;
    private Quaternion _originalRotation;
    
    public void Interact()
    {
        Debug.Log("Interact");
    }

    private void Start()
    {
        _originalRotation = Quaternion.identity;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _player.transform.localRotation, 100f * Time.deltaTime);
        }
    }
}
