using System;
using TMPro;
using UnityEngine;

public class CollectionPercentage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _percentageText;

    private float _currentCollected = 0;
    private float _totalCollection = 9;

    private void Start()
    {
        UpdatePercentageText();
    }

    public void AddNewEntry()
    {
        _currentCollected++;
        UpdatePercentageText();
        CollectionCompleted();
    }

    private void UpdatePercentageText()
    {
        float percentage = (_currentCollected / _totalCollection) * 100f;
        _percentageText.text = "Collected: " + percentage.ToString("F1") + "%";
    }

    private void CollectionCompleted()
    {
        if (_currentCollected >= _totalCollection)
        {
            _percentageText.text = "JOURNAL COMPLETE!!!!";
        }
    }
}
