using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Fishing : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public GameObject BaitPrefab;

    private GameObject _bait;
    private Rigidbody _baitRB;

    private float _counter;
    private bool _isFishing;

    public GameObject QuickTimeUI;
    public GameObject Right;
    public GameObject Left;
    public GameObject Pull;

    private float _quickTimeBuffer = 0.2f;
    private float _quickTimeBufferCounter;

    public GameObject FishingSlider;
    public GameObject Slider;

    public GameObject DropBug;

    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        QuickTimeUI = GameObject.FindGameObjectWithTag("quicktime");
        Right = QuickTimeUI.transform.GetChild(0).gameObject;
        Left = QuickTimeUI.transform.GetChild(1).gameObject;
        Pull = QuickTimeUI.transform.GetChild(2).gameObject;
        QuickTimeUI.SetActive(false);
        FishingSlider.gameObject.SetActive(false);

        _counter = 0;
        _isFishing = false;
        PlayerMovement.IsFishing = false;
    }

    void Update()
    {
        if (_isFishing && Input.GetMouseButtonUp(0) && PlayerMovement.CanThrowRod == true)
        {
            if (_bait != null)
            {
                Destroy(_bait);
            }

            PlayerMovement.IsFishing = true;
            _bait = Instantiate(BaitPrefab,new Vector3(transform.position.x, transform.position.y+8, transform.position.z) + transform.forward*3, transform.rotation);
            _baitRB = _bait.GetComponent<Rigidbody>();
            _baitRB.AddForce(_bait.transform.forward * _counter * 1f);
        }

        if (PlayerMovement.CanFish && Input.GetMouseButton(0))
        {
            DropBug.SetActive(false);
            FishingSlider.SetActive(true);

            Slider.GetComponent<RectTransform>().sizeDelta = new Vector2(_counter*400/220, 40);

            if (_counter >= 220)
            {
                _counter = 220;
            }
            else
            {
                _counter++;
            }
            _isFishing = true;
        }
        else
        {
            _counter = 0;
            _isFishing = false;
            FishingSlider.gameObject.SetActive(false);
        }

        if(PlayerMovement.IsFishing && (Input.GetKey(KeyCode.Escape) || Input.GetMouseButton(0)))
        {
            _isFishing = false;
            PlayerMovement.IsFishing = false;
            Destroy(_bait);
        }
    }

    public bool FishOnce(int num)
    {
        Debug.Log(num);
        QuickTimeUI.SetActive(true);
        
        if (num == 0)
        {
            _quickTimeBufferCounter += Time.deltaTime;
            Right.SetActive(false);
            if(_quickTimeBufferCounter >= _quickTimeBuffer)
            {
                Left.SetActive(true);
            }
            Pull.SetActive(false);
            if (Input.GetKeyDown(KeyCode.A) && _quickTimeBufferCounter >= _quickTimeBuffer)
            {
               _quickTimeBufferCounter = 0;
                Left.SetActive(false);
               return true;
            }
        }
        
        if (num == 1)
        {
            _quickTimeBufferCounter += Time.deltaTime;
            if (_quickTimeBufferCounter >= _quickTimeBuffer)
            {
                Right.SetActive(true);
            }
            Left.SetActive(false);
            Pull.SetActive(false);
            if (Input.GetKeyDown(KeyCode.D) && _quickTimeBufferCounter >= _quickTimeBuffer)
            {
               _quickTimeBufferCounter = 0;
                Right.SetActive(false);
               return true;
            }
        }
        
        if (num == 2)
        {
            _quickTimeBufferCounter += Time.deltaTime;
            Right.SetActive(false);
            Left.SetActive(false);
            if (_quickTimeBufferCounter >= _quickTimeBuffer)
            {
                Pull.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.S) && _quickTimeBufferCounter >= _quickTimeBuffer)
            {
               _quickTimeBufferCounter = 0;
                Pull.SetActive(false);
               return true;
            }
        }
        return false;
    }

    public void DisableQuickTimeUI()
    {
        QuickTimeUI.SetActive(false);
    }
}