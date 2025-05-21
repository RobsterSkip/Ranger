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

    private float _quickTimeBuffer = 0.5f;
    private float _quickTimeBufferCounter;

    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        QuickTimeUI = GameObject.FindGameObjectWithTag("quicktime");
        Right = QuickTimeUI.transform.GetChild(0).gameObject;
        Left = QuickTimeUI.transform.GetChild(1).gameObject;
        Pull = QuickTimeUI.transform.GetChild(2).gameObject;
        QuickTimeUI.SetActive(false);

        _counter = 0;
        _isFishing = false;
        PlayerMovement.IsFishing = false;
    }

    void Update()
    {
        Debug.Log(_quickTimeBufferCounter);
        if (_isFishing && Input.GetMouseButtonUp(0))
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
        QuickTimeUI.SetActive(true);
        _quickTimeBufferCounter += Time.deltaTime;
        if (num == 0 && _quickTimeBufferCounter >= _quickTimeBuffer)
        {
            Right.SetActive(false);
            Left.SetActive(true);
            Pull.SetActive(false);
            if (Input.GetKeyDown(KeyCode.A))
            {
                return true;
            }
            _quickTimeBufferCounter = 0;
        }
        
        if (num == 1 && _quickTimeBufferCounter >= _quickTimeBuffer)
        {
            Right.SetActive(true);
            Left.SetActive(false);
            Pull.SetActive(false);
            if (Input.GetKeyDown(KeyCode.D))
            {
                return true;
            }
            _quickTimeBufferCounter = 0;
        }
        
        if (num == 2 && _quickTimeBufferCounter >= _quickTimeBuffer)
        {
            Right.SetActive(false);
            Left.SetActive(false);
            Pull.SetActive(true);
            if (Input.GetKeyDown(KeyCode.S))
            {
                return true;
            }
            _quickTimeBufferCounter = 0;
        }
        return false;
    }

    public void DisableQuickTimeUI()
    {
        QuickTimeUI.SetActive(false);
    }
}