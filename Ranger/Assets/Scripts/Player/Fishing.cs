using System.Runtime.CompilerServices;
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

    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
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
            _bait = Instantiate(BaitPrefab,new Vector3(transform.position.x, transform.position.y+8, transform.position.z), transform.rotation);
            _baitRB = _bait.GetComponent<Rigidbody>();
            _baitRB.AddForce(_bait.transform.forward * _counter * 1.5f);
            
        }

        if (PlayerMovement.CanFish && Input.GetMouseButton(1))
        {
            if (_counter >= 250)
            {
                _counter = 250;
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
}