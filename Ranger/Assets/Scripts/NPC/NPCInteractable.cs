using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField]
    private CharacterController _player;

    [SerializeField]
    private float _rotationSpeedPlayer = 1.75f;
    private float _rotationSpeedOriginal = 1f;

    private Quaternion _targetRotation;
    private Quaternion _originalRotation;

    private bool _inRange;
    
    public void Interact()
    {
        Debug.Log("Interact");
    }

    private void Start()
    {
        _originalRotation = Quaternion.identity;
    }

    private void Update()
    {
        OriginalRotation();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _inRange = true;

            Vector3 currentDirection = transform.forward;
            currentDirection.y = 0;
            currentDirection.Normalize();

            Vector3 targetRotation = _player.transform.position - transform.position;
            targetRotation.y = 0;
            targetRotation.Normalize();

            Vector3 newRotation = Vector3.RotateTowards(currentDirection, 
                targetRotation, _rotationSpeedPlayer * Time.deltaTime, 0.0f);

            transform.rotation = Quaternion.LookRotation(newRotation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _inRange = false;
            Debug.Log(_inRange);
        }
    }

    private void OriginalRotation()
    {
        if(_inRange == false)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _originalRotation,
                _rotationSpeedOriginal * Time.deltaTime * 100f);
        }
    }
}
