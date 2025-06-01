using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalEntries : MonoBehaviour
{
    private CollectionPercentage _collectionPercentage;

    [SerializeField] TextMeshProUGUI _title, _entry, _pageNumber, _missingEntry, _talkToRanger;
    [SerializeField] GameObject _pages, _AButton, _DButton, _journalObject;
    [SerializeField] Sprite[] _sprites;
    [SerializeField] Image _panelImage;
    [SerializeField] GameObject _journalColour;

    [SerializeField]
    private GameObject _ui;
    [SerializeField]
    private GameObject _collection;

    private int _entryIndex = 0;
    
    private readonly int _maxEntries = 18;

    Journal _journalClass;
    private GameObject _journal;

    public bool _enable1, _enable2, _enable3, _enable4, _enable5, _enable6, _enable7, _enable8, _enable9, 
        _enable10, _enable11, _enable12, _enable13, _enable14, _enable15, _enable16, _enable17, _enable18;
    public bool _added1, _added2, _added3, _added4, _added5, _added6, _added7, _added8, _added9,
        _added10, _added11, _added12, _added13, _added14, _added15, _added16, _added17, _added18;

    private void Start()
    {
        _collectionPercentage = _collection.GetComponent<CollectionPercentage>();

        _journal = GameObject.FindGameObjectWithTag("Journal");
        _journalClass = _journal.GetComponent<Journal>();

        _title.SetText("Moth");
        _entry.SetText("0");

        _title.enabled = false;
        _entry.enabled = false;
        _missingEntry.enabled = false;
        _talkToRanger.enabled = false;
        _pages.SetActive(false);
        _panelImage.enabled = false;
        _ui.SetActive(false);
    }
    void Update()
    {
        if (_journalClass._journalOpen == true)
        {
            _pages.SetActive(true);
            _AButton.SetActive(true);
            _DButton.SetActive(true);
            _journalObject.SetActive(true);
            _panelImage.enabled=true;
            
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
            else if (_entryIndex >= _maxEntries)
            {
                _entryIndex = 0;
            }
        }
        else
        {
            _pages.SetActive(false);
            _AButton.SetActive(false);
            _DButton.SetActive(false);
            _journalObject.SetActive(false);
            _panelImage.enabled=false;
        }
        SetPages();
    }

    private void SetPages()
    {
        var color = _journalColour.GetComponent<Image>().color;
        switch (_entryIndex)
        {
            case 0:
                _pageNumber.SetText("1/18");
                if(_enable1 == true) 
                {
                    color = Color.magenta;
                    color.a = 0.75f;

                    if (!_added1)
                    {
                        _collectionPercentage.AddNewEntry();
                        _added1 = true;
                    }

                    _title.enabled = true;
                    _entry.enabled = true;

                    _missingEntry.enabled = false;
                    _talkToRanger.enabled = false;

                    _panelImage.sprite = _sprites[0];

                    _journalColour.GetComponent<Image>().color = color;
                }
                else
                {
                    color = Color.magenta;
                    color.a = 0.75f;

                    _title.enabled = false;
                    _entry.enabled = false;

                    _missingEntry.enabled = true;
                    _talkToRanger.enabled = true;

                    _panelImage.sprite = _sprites[9];

                    _journalColour.GetComponent<Image>().color = color;
                }
                _title.SetText("Moth");
                _entry.SetText("It was no flyby.");
                break;
            case 1:
                _pageNumber.SetText("2/18");
                if (_enable2 == true)
                {
                    color = Color.magenta;
                    color.a = 0.75f;

                    if (!_added2)
                    {
                        _collectionPercentage.AddNewEntry();
                        _added2 = true;
                    }

                    _title.enabled = true;
                    _entry.enabled = true;

                    _missingEntry.enabled = false;
                    _talkToRanger.enabled = false;

                    _panelImage.sprite = _sprites[1];

                    _journalColour.GetComponent<Image>().color = color;
                }
                else
                {
                    color = Color.magenta;
                    color.a = 0.75f;

                    _title.enabled = false;
                    _entry.enabled = false;

                    _missingEntry.enabled = true;
                    _talkToRanger.enabled = true;

                    _panelImage.sprite = _sprites[10];

                    _journalColour.GetComponent<Image>().color = color;
                }
                _title.SetText("Ladybug");
                _entry.SetText("A distinguished gentleman!");
                break;
            case 2:
                _pageNumber.SetText("3/18");
                if (_enable3 == true)
                {
                    color = Color.magenta;
                    color.a = 0.75f;

                    if (!_added3)
                    {
                        _collectionPercentage.AddNewEntry();
                        _added3 = true;
                    }

                    _title.enabled = true;
                    _entry.enabled = true;

                    _missingEntry.enabled = false;
                    _talkToRanger.enabled = false;

                    _panelImage.sprite = _sprites[2];

                    _journalColour.GetComponent<Image>().color = color;
                }
                else
                {
                    color = Color.magenta;
                    color.a = 0.75f;

                    _title.enabled = false;
                    _entry.enabled = false;

                    _missingEntry.enabled = true;
                    _talkToRanger.enabled = true;

                    _panelImage.sprite = _sprites[11];

                    _journalColour.GetComponent<Image>().color = color;
                }
                _title.SetText("Catterpillar");
                _entry.SetText("There is a catter-pile of them.");
                break;
            case 3:
                _pageNumber.SetText("4/18");
                if (_enable4 == true)
                {
                    color = Color.yellow;
                    color.a = 0.75f;

                    if (!_added4)
                    {
                        _collectionPercentage.AddNewEntry();
                        _added4 = true;
                    }

                    _title.enabled = true;
                    _entry.enabled = true;

                    _missingEntry.enabled = false;
                    _talkToRanger.enabled = false;

                    _panelImage.sprite = _sprites[3];

                    _journalColour.GetComponent<Image>().color = color;
                }
                else
                {
                    color = Color.yellow;
                    color.a = 0.75f;

                    _title.enabled = false;
                    _entry.enabled = false;

                    _missingEntry.enabled = true;
                    _talkToRanger.enabled = true;

                    _panelImage.sprite = _sprites[12];

                    _journalColour.GetComponent<Image>().color = color;
                }
                _title.SetText("Wildflower");
                _entry.SetText("This is wild!");
                break;

            case 4:
                _pageNumber.SetText("5/18");
                if (_enable5 == true)
                {
                    color = Color.yellow;
                    color.a = 0.75f;

                    if (!_added5)
                    {
                        _collectionPercentage.AddNewEntry();
                        _added5 = true;
                    }

                    _title.enabled = true;
                    _entry.enabled = true;

                    _missingEntry.enabled = false;
                    _talkToRanger.enabled = false;

                    _panelImage.sprite = _sprites[4];

                    _journalColour.GetComponent<Image>().color = color;
                }
                else
                {
                    color = Color.yellow;
                    color.a = 0.75f;

                    _title.enabled = false;
                    _entry.enabled = false;

                    _missingEntry.enabled = true;
                    _talkToRanger.enabled = true;

                    _panelImage.sprite = _sprites[13];

                    _journalColour.GetComponent<Image>().color = color;
                }
                _title.SetText("Sunflower");
                _entry.SetText("The brightest of them all.");
                break;
            case 5:
                _pageNumber.SetText("6/18");
                if (_enable6 == true)
                {
                    color = Color.yellow;
                    color.a = 0.75f;

                    if (!_added6)
                    {
                        _collectionPercentage.AddNewEntry();
                        _added6 = true;
                    }

                    _title.enabled = true;
                    _entry.enabled = true;

                    _missingEntry.enabled = false;
                    _talkToRanger.enabled = false;

                    _panelImage.sprite = _sprites[5];

                    _journalColour.GetComponent<Image>().color = color;
                }
                else
                {
                    color = Color.yellow;
                    color.a = 0.75f;

                    _title.enabled = false;
                    _entry.enabled = false;

                    _missingEntry.enabled = true;
                    _talkToRanger.enabled = true;

                    _panelImage.sprite = _sprites[14];

                    _journalColour.GetComponent<Image>().color = color;
                }
                _title.SetText("Forget-me-not");
                _entry.SetText("I forgot.");
                break;
            case 6:
                _pageNumber.SetText("7/18");
                if (_enable7 == true)
                {
                    color = Color.blue;
                    color.a = 0.75f;

                    if (!_added7)
                    {
                        _collectionPercentage.AddNewEntry();
                        _added7 = true;
                    }

                    _title.enabled = true;
                    _entry.enabled = true;

                    _missingEntry.enabled = false;
                    _talkToRanger.enabled = false;

                    _panelImage.sprite = _sprites[6];

                    _journalColour.GetComponent<Image>().color = color;
                }
                else
                {
                    color = Color.blue;
                    color.a = 0.75f;

                    _title.enabled = false;
                    _entry.enabled = false;

                    _missingEntry.enabled = true;
                    _talkToRanger.enabled = true;

                    _panelImage.sprite = _sprites[15];

                    _journalColour.GetComponent<Image>().color = color;
                }
                _title.SetText("Carp");
                _entry.SetText("It is not that bad.");
                break;
            case 7:
                _pageNumber.SetText("8/18");
                if (_enable8 == true)
                {
                    color = Color.blue;
                    color.a = 0.75f;

                    if (!_added8)
                    {
                        _collectionPercentage.AddNewEntry();
                        _added8 = true;
                    }

                    _title.enabled = true;
                    _entry.enabled = true;

                    _missingEntry.enabled = false;
                    _talkToRanger.enabled = false;

                    _panelImage.sprite = _sprites[7];

                    _journalColour.GetComponent<Image>().color = color;
                }
                else
                {
                    color = Color.blue;
                    color.a = 0.75f;

                    _title.enabled = false;
                    _entry.enabled = false;

                    _missingEntry.enabled = true;
                    _talkToRanger.enabled = true;

                    _panelImage.sprite = _sprites[16];

                    _journalColour.GetComponent<Image>().color = color;
                }
                _title.SetText("Perch");
                _entry.SetText("It was not a long search.");
                break;
            case 8:
                _pageNumber.SetText("9/18");
                if (_enable9 == true)
                {
                    color = Color.blue;
                    color.a = 0.75f;

                    if (!_added9)
                    {
                        _collectionPercentage.AddNewEntry();
                        _added9 = true;
                    }

                    _title.enabled = true;
                    _entry.enabled = true;

                    _missingEntry.enabled = false;
                    _talkToRanger.enabled = false;

                    _panelImage.sprite = _sprites[8];

                    _journalColour.GetComponent<Image>().color = color;
                }
                else
                {
                    color = Color.blue;
                    color.a = 0.75f;

                    _title.enabled = false;
                    _entry.enabled = false;

                    _missingEntry.enabled = true;
                    _talkToRanger.enabled = true;

                    _panelImage.sprite = _sprites[17];

                    _journalColour.GetComponent<Image>().color = color;
                }
                _title.SetText("Trout");
                _entry.SetText("Proud of this trout!");
                break;
            case 9:
                _pageNumber.SetText("10/18");
                if (_enable10 == true)
                {
                    color = Color.blue;
                    color.a = 0.75f;

                    if (!_added10)
                    {
                        _collectionPercentage.AddNewEntry();
                        _added9 = true;
                    }

                    _title.enabled = true;
                    _entry.enabled = true;

                    _missingEntry.enabled = false;
                    _talkToRanger.enabled = false;

                    _panelImage.sprite = _sprites[8];

                    _journalColour.GetComponent<Image>().color = color;
                }
                else
                {
                    color = Color.blue;
                    color.a = 0.75f;

                    _title.enabled = false;
                    _entry.enabled = false;

                    _missingEntry.enabled = true;
                    _talkToRanger.enabled = true;

                    _panelImage.sprite = _sprites[17];

                    _journalColour.GetComponent<Image>().color = color;
                }
                _title.SetText("Trout");
                _entry.SetText("Proud of this trout!");
                break;
        }
    }
}
