using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionPercentage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _percentageText;
   
    
    [SerializeField] private GameObject _closeMenuButton;
    [SerializeField] private TextMeshProUGUI _endText;
    [SerializeField] private TextMeshProUGUI _thanks;
    [SerializeField] private GameObject _overlay;
    [SerializeField] private Image[] _collectibleImages;

    [SerializeField]
    private float _currentCollected = 0;
    private float _totalCollection = 18;

    private void Start()
    {
        UpdatePercentageText();

        _overlay.SetActive(false);
        _endText.enabled = false;
        _thanks.enabled = false;
        _closeMenuButton.SetActive(false);

        foreach (Image sprite in _collectibleImages)
        {
            sprite.enabled = false;
        }
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
            BooleanManager.IsJournalCompleted = true;
            _percentageText.text = "JOURNAL COMPLETE!!!!";

            _closeMenuButton.SetActive(true);
            _overlay.SetActive(true);
            _endText.enabled = true;
            _thanks.enabled = true;

            foreach (Image sprite in _collectibleImages)
            {
                sprite.enabled = true;
            }

            BooleanManager._everythingCollected = true;
        }
    }

    public void CloseJournal()
    {
        _overlay.SetActive(false);
        _endText.enabled = false;
        _thanks.enabled = false;
        _closeMenuButton.SetActive(false);

        foreach (Image sprite in _collectibleImages)
        {
            sprite.enabled = false;
        }

        BooleanManager._everythingCollected = false;
    }
}
