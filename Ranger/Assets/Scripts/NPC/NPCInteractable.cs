using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCInteractable : MonoBehaviour
{
    private ChatBubble _chatBubble;

    [SerializeField]
    private CharacterController _player;

    [SerializeField]
    private float _rotationSpeedPlayer = 1.75f;
    private float _rotationSpeedOriginal = 1f;

    private Quaternion _targetRotation;
    private Quaternion _originalRotation;

    private bool _inRange;
    private bool _inTrigger;
    
    private void Start()
    {
        _chatBubble = GameObject.FindGameObjectWithTag("ChatBubble").GetComponent<ChatBubble>();
        _originalRotation = Quaternion.identity;
    }

    private void Update()
    {
        OriginalRotation();
        if (_inTrigger == true)
        {
            InTriggerUpdate();
        }
    }

    private void InTriggerUpdate()
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

            _chatBubble.ItemGiven();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _inTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interact();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _inRange = false;
            _inTrigger = false;
            _chatBubble.Remove();
        }
    }

    public void Interact()
    {
        _chatBubble.Create();
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
