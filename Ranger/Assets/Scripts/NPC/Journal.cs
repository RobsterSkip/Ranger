using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Journal : MonoBehaviour
{
    [SerializeField] private List<GameObject> _items;
    [SerializeField] GameObject _journalUI;

    private bool _inTrigger;
    public bool _journalOpen;

    private void Update()
    {
        if(_inTrigger == true)
        {
            OpenInventory();
        }
    }

    private void OpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            _journalUI.SetActive(true);
            _journalOpen = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            _journalUI.SetActive(false);
            _journalOpen = false;
        }
    }

    public void ItemToJournal(GameObject entry)
    {
        _items.Add(entry);
        Debug.Log(_items);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _inTrigger = true;
        }
    }
}
