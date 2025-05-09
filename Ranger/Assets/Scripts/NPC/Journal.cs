using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
    [SerializeField] private List<GameObject> _items;

    private void Update()
    {
       
    }
   
    public void ItemToJournal(GameObject entry)
    {
        _items.Add(entry);
        Debug.Log(_items);
    }
}
