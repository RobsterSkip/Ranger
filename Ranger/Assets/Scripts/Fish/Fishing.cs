using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Fishing : MonoBehaviour
{
    public GameObject Inventory;
    public InventoryInput Manager;

    public PlayerMovement PlayerMovement;
    public GameObject BaitPrefab;

    private GameObject _bait;
    private Rigidbody _baitRB;

    private float _counter;
    private bool _isFishing;
    private bool _dropBugEnabled;

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

        DropBug.SetActive(false);

        Manager = Inventory.GetComponent<InventoryInput>();
    }

    void Update()
    {
        if (_isFishing && Input.GetMouseButtonUp(0) && PlayerMovement.CanThrowRod && PlayerMovement.NearWater)
        {
            if (_bait != null)
            {
                Destroy(_bait);
            }
            BooleanManager.IsLineCast = true;

            PlayerMovement.IsFishing = true;
            _bait = Instantiate(BaitPrefab,new Vector3(transform.position.x, transform.position.y+8, transform.position.z) + transform.forward*3, transform.rotation);
            _baitRB = _bait.GetComponent<Rigidbody>();
            _baitRB.AddForce(_bait.transform.forward * _counter * 2f);

            BooleanManager._baitCast = true;
        }

        if (PlayerMovement.CanFish == true && Input.GetMouseButton(0) && PlayerMovement.NearWater)
        {
            BooleanManager.MovementDisabled = true;
            DropBug.SetActive(false);
            FishingSlider.SetActive(true);

            Slider.GetComponent<RectTransform>().sizeDelta = new Vector2(_counter*400/300, 40);

            if (_counter >= 300)
            {
                _counter = 300;
            }
            else
            {
                _counter += 1;
            }
            _isFishing = true;
        }
        else
        {
            _counter = 0;
            _isFishing = false;
            FishingSlider.gameObject.SetActive(false);
            BooleanManager.MovementDisabled = false;

            _dropBugEnabled = true;
        }

        if(PlayerMovement.IsFishing && (Input.GetKey(KeyCode.Escape) || Input.GetMouseButton(0)))
        {
            _isFishing = false;
            PlayerMovement.IsFishing = false;
            Destroy(_bait);

            _dropBugEnabled = true;

            BooleanManager._baitCast = false;
        }

        if (PlayerMovement.OutsideWater && PlayerMovement.CanFish || PlayerMovement.OutsideWater && PlayerMovement.IsFishing  )
        {
            _dropBugEnabled = false;
            DropBug.SetActive(false);
        }

        if (_dropBugEnabled && Input.GetMouseButtonDown(0) && PlayerMovement.NearWater)
        {
            DropBug.SetActive(true);
        }
        else if (PlayerMovement.OutsideWater)
        {
            DropBug.SetActive(false);
        }
    }

    public bool FishOnce(int num)
    {
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