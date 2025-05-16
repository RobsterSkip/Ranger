using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalEntries : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _entry;
    [SerializeField] TextMeshProUGUI _pageNumber;
    [SerializeField] GameObject _pages;
    [SerializeField] Sprite[] _sprites;
    [SerializeField] Image _panelImage;

    [SerializeField]
    private GameObject _ui;

    private int _entryIndex = 0;
    private int _maxEntries = 9;

    Journal _journalClass;
    private GameObject _journal;

    private Items _items;

    public bool _enable1, _enable2, _enable3, _enable4, _enable5, _enable6, _enable7, _enable8, _enable9;

    private void Start()
    {
        _journal = GameObject.FindGameObjectWithTag("Journal");
        _journalClass = _journal.GetComponent<Journal>();

        _title.SetText("Moth");
        _entry.SetText("0");

        _title.enabled = false;
        _entry.enabled = false;
        _pages.SetActive(false);
        _panelImage.enabled = false;
        _ui.SetActive(false);
    }
    void Update()
    {
        if (_journalClass._journalOpen == true)
        {
            _pages.SetActive(true);
            if (Input.GetKeyDown(KeyCode.A))
            {
                _entryIndex--;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _entryIndex++;
            }

            if (_entryIndex < 0)
            {
                _entryIndex = _maxEntries - 1;
            }
            else if (_entryIndex > _maxEntries)
            {
                _entryIndex = 0;
            }
        }
        else
        {
            _pages.SetActive(false);
        }
        SetPages();
    }

    private void SetPages()
    {
        switch (_entryIndex)
        {
            case 0:
                _pageNumber.SetText("1/9");
                if(_enable1 == true) 
                { 
                    _title.enabled = true;
                    _entry.enabled = true;
                    _panelImage.enabled = true;
                }
                else
                {
                    _title.enabled = false;
                    _entry.enabled = false;
                    _panelImage.enabled = false;
                }
                _title.SetText("Moth");
                _entry.SetText("There is more-th where that came from");
                _panelImage.sprite = _sprites[0];
                break;

            case 1:
                _pageNumber.SetText("2/9");
                if (_enable2 == true)
                {
                    _title.enabled = true;
                    _entry.enabled = true;
                    _panelImage.enabled = true;
                }
                else
                {
                    _title.enabled = false;
                    _entry.enabled = false;
                    _panelImage.enabled = false;
                }
                _title.SetText("Ladybug");
                _entry.SetText("A distinguished gentleman!");
                _panelImage.sprite = _sprites[1];
                break;

            case 2:
                _pageNumber.SetText("3/9");
                if (_enable3 == true)
                {
                    _title.enabled = true;
                    _entry.enabled = true;
                    _panelImage.enabled = true;
                }
                else
                {
                    _title.enabled = false;
                    _entry.enabled = false;
                    _panelImage.enabled = false;
                }
                _title.SetText("Catterpillar");
                _entry.SetText("There are a catter-pile of them");
                _panelImage.sprite = _sprites[2];
                break;

            case 3:
                _pageNumber.SetText("4/9");
                if (_enable4 == true)
                {
                    _title.enabled = true;
                    _entry.enabled = true;
                    _panelImage.enabled = true;
                }
                else
                {
                    _title.enabled = false;
                    _entry.enabled = false;
                    _panelImage.enabled = false;
                }
                _title.SetText("Wildflower");
                _entry.SetText("This is wild!");
                _panelImage.sprite = _sprites[3];
                break;

            case 4:
                _pageNumber.SetText("5/9");
                if (_enable5 == true)
                {
                    _title.enabled = true;
                    _entry.enabled = true;
                    _panelImage.enabled = true;
                }
                else
                {
                    _title.enabled = false;
                    _entry.enabled = false;
                    _panelImage.enabled = false;
                }
                _title.SetText("Sunflower");
                _entry.SetText("The sunshine of flowers");
                _panelImage.sprite = _sprites[4];
                break;

            case 5:
                _pageNumber.SetText("6/9");
                if (_enable6 == true)
                {
                    _title.enabled = true;
                    _entry.enabled = true;
                    _panelImage.enabled = true;
                }
                else
                {
                    _title.enabled = false;
                    _entry.enabled = false;
                    _panelImage.enabled = false;
                }
                _title.SetText("Forget-me-not");
                _entry.SetText("I forgot");
                _panelImage.sprite = _sprites[5];
                break;

            case 6:
                _pageNumber.SetText("7/9");
                if (_enable7 == true)
                {
                    _title.enabled = true;
                    _entry.enabled = true;
                    _panelImage.enabled = true;
                }
                else
                {
                    _title.enabled = false;
                    _entry.enabled = false;
                    _panelImage.enabled = false;
                }
                _title.SetText("Carp");
                _entry.SetText("It is not that bad");
                _panelImage.sprite = _sprites[6];
                break;

            case 7:
                _pageNumber.SetText("8/9");
                if (_enable8 == true)
                {
                    _title.enabled = true;
                    _entry.enabled = true;
                    _panelImage.enabled = true;
                }
                else
                {
                    _title.enabled = false;
                    _entry.enabled = false;
                    _panelImage.enabled = false;
                }
                _title.SetText("Perch");
                _entry.SetText("It was not a long search");
                _panelImage.sprite = _sprites[7];
                break;

            case 8:
                _pageNumber.SetText("9/9");
                if (_enable9 == true)
                {
                    _title.enabled = true;
                    _entry.enabled = true;
                    _panelImage.enabled = true;
                }
                else
                {
                    _title.enabled = false;
                    _entry.enabled = false;
                    _panelImage.enabled = false;
                }
                _title.SetText("Trout");
                _entry.SetText("Proud of this trout!");
                _panelImage.sprite = _sprites[8];
                break;

        }
    }
}
