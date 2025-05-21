using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Journal : MonoBehaviour
{
    private InventoryInput _inventoryInput;
    [SerializeField] private List<GameObject> _items;
    [SerializeField] private GameObject _journalUI;
    [SerializeField] private GameObject _interactPanel;

    private bool _inTrigger;
    public bool _journalOpen;

    private void Update()
    {
        if (_inTrigger == true)
        {
            OpenInventory();
            _interactPanel.SetActive(true);
        }
        if (_inTrigger == false)
        {
            _interactPanel.SetActive(false);
        }
    }

    private void OpenInventory()
    {
        if(_journalOpen == false && Input.GetKeyDown(KeyCode.E))
        {
            _journalUI.SetActive(true);
            _journalOpen = true;
        }
        else if(_journalOpen == true && ( Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)))
        {
            _journalUI.SetActive(false);
            _journalOpen = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _inTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _inTrigger = false;
    }
}
