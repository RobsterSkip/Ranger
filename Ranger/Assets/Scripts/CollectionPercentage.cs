using System;
using TMPro;
using UnityEngine;

public class CollectionPercentage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _percentageText;

    public float _currentCollected;
    private float _collectionCalculation;
    private float _totalCollection = 9;

    private void Start()
    {
        _currentCollected = 0;
        _collectionCalculation = (_currentCollected / _totalCollection) * 100;
    }

    void Update()
    {
       _percentageText.text = "Collected: " + _collectionCalculation + "%";

        CollectionCompleted();
    }

    private void CollectionCompleted()
    {
        if(_currentCollected >= _totalCollection)
        {
            _percentageText.text = "JOURNAL COMPLETE!!!!";
        }
    }
}
