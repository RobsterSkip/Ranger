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
        if (_isFishing && Input.GetMouseButtonUp(1))
        {
            //Debug.Log(_counter);
            if (_bait != null)
            {
                Destroy(_bait);
            }

            PlayerMovement.IsFishing = true;
            _bait = Instantiate(BaitPrefab,new Vector3(transform.position.x, transform.position.y+8, transform.position.z) + transform.forward*3, transform.rotation);
            _baitRB = _bait.GetComponent<Rigidbody>();
            _baitRB.AddForce(_bait.transform.forward * _counter * 1.5f);
            
        }

        if (PlayerMovement.CanFish && Input.GetMouseButton(1))
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

        if(PlayerMovement.IsFishing && Input.GetKey(KeyCode.Escape))
        {
            _isFishing = false;
            PlayerMovement.IsFishing = false;
            Destroy(_bait);
        }
    }

    public bool FishOnce(int num)
    {
        QuickTimeUI.SetActive(true);

        if (num == 0)
        {
            //Debug.Log("<--");
            Right.SetActive(false);
            Left.SetActive(true);
            Pull.SetActive(false);
            if (Input.GetKeyDown(KeyCode.A))
            {
                return true;
            }
        }
        
        if (num == 1)
        {
            //Debug.Log("-->");
            Right.SetActive(true);
            Left.SetActive(false);
            Pull.SetActive(false);
            if (Input.GetKeyDown(KeyCode.D))
            {
                return true;
            }
        }
        
        if (num == 2)
        {
            //Debug.Log("pull (S)");
            Right.SetActive(false);
            Left.SetActive(false);
            Pull.SetActive(true);
            if (Input.GetKeyDown(KeyCode.S))
            {
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