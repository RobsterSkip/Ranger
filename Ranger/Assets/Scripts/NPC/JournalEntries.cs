using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalEntries : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _entry;
    [SerializeField] Sprite[] _sprites;
    [SerializeField] Image _panelImage;

    [SerializeField]
    private GameObject _ui;

    private int _entryIndex = 0;
    private int _maxEntries = 8;

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
        _panelImage.enabled = false;
        _ui.SetActive(false);
    }
    void Update()
    {
        if (_journalClass._journalOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _entryIndex--;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
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
        SetPages();
    }

    private void SetPages()
    {
        switch (_entryIndex)
        {
            case 0:
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
                _entry.SetText("There is more where that came from.");
                _panelImage.sprite = _sprites[0];
                break;

            case 1:
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
                _entry.SetText("Looks like a distinguished gentleman to me!");
                _panelImage.sprite = _sprites[1];
                break;

            case 2:
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
                _entry.SetText("The ever-humble Catterpillar");
                _panelImage.sprite = _sprites[2];
                break;

            case 3:
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
                _entry.SetText("It is gonna get wild!");
                _panelImage.sprite = _sprites[3];
                break;

            case 4:
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
                _entry.SetText("You are my sunshine");
                _panelImage.sprite = _sprites[4];
                break;

            case 5:
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
                _entry.SetText("I forgor");
                _panelImage.sprite = _sprites[5];
                break;

            case 6:
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
                _entry.SetText("Carp? It doesnt look THAT bad.");
                _panelImage.sprite = _sprites[6];
                break;

            case 7:
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
                _entry.SetText("Thats not a bird though?");
                _panelImage.sprite = _sprites[7];
                break;

            case 8:
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
                _entry.SetText("I'm very proud of this trout!");
                _panelImage.sprite = _sprites[8];
                break;

        }
    }
}
