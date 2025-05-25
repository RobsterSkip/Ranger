using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public Transform Cam;

    private readonly float _defaultSpeed = 10f;
    private readonly float _crouchingSpeed = 7f;
    private float _currentSpeed;
    private readonly float _sprintingSpeed = 15f;

    private readonly float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    public bool IsCrouching;
    private bool IsSprinting;

    public bool CanFish;
    public bool CanThrowRod;

    [SerializeField] LayerMask waterLayer;
    public GameObject WaterSurface;

    public bool IsFishing;

    private CameraMovement _cameraMovement;
    [SerializeField]
    private GameObject _camera;

    Journal _journalClass;
    [SerializeField]
    private GameObject _journal;

    [SerializeField]
    private BoxCollider _fishingCollider;

    public GameObject Inventory;
    public InventoryManager Manager;

    private Vector3 _controllerScaleDefault = new Vector3(0, 2.3f, 0);
    private Vector3 _controllerScaleCrouch = new Vector3(0, 3f, 0);

    private Vector3 _scaleDefault;
    private Vector3 _scaleCrouch = new Vector3(1.288253f, 1f, 1.288253f);
    public GameObject _model;

    private Vector3 Velocity;
    private float _gravity;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _cameraMovement = _camera.GetComponent<CameraMovement>();

        _journal = GameObject.FindGameObjectWithTag("Journal");
        _journalClass = _journal.GetComponent<Journal>();

        Inventory = GameObject.FindGameObjectWithTag("InventoryManager");
        Manager = Inventory.GetComponent<InventoryManager>();

        _scaleDefault = _model.transform.localScale;
        _currentSpeed = _defaultSpeed;

        _gravity = -9.81f;
    }

    void Update()
    {
        if (_cameraMovement._inventoryOpen == false && _journalClass._journalOpen == false)
        {
            Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal") * _currentSpeed * Time.deltaTime,
                                        0f, Input.GetAxisRaw("Vertical") * _currentSpeed * Time.deltaTime).normalized;

            if (direction.magnitude >= 0.1f && !IsFishing) //smooth turn
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                Velocity = moveDir.normalized * _currentSpeed;
                Velocity.y = _gravity;  //collider fix

                Controller.Move(Velocity * Time.deltaTime);
            }
            CheckCrouching();
            CheckFishing();
            CheckSprinting();
        }
        AddGravity();
    }

    private void AddGravity()
    {
       Velocity.y = _gravity * Controller.skinWidth;
    }

    private void CheckFishing()
    {
        if(Physics.CheckSphere(transform.position, 6, waterLayer)) //is in range
        {
            
            Vector3 direction= (new Vector3(WaterSurface.transform.position.x, 0 , WaterSurface.transform.position.x) - new Vector3(transform.position.x, 0, transform.position.x)).normalized;
            float dotProd = Vector3.Dot(direction, transform.forward);
            
            if (true)//dotProd > 0.1 && dotProd < 0.95) //is facing water needs a rework
            {
                
                Collider[] hitColliders = Physics.OverlapSphere(_fishingCollider.transform.position, 
                    _fishingCollider.bounds.extents.magnitude);
                foreach (Collider collider in hitColliders)
                {
                    if(Input.GetMouseButton(0) && !IsFishing && !CanFish)
                    {
                        Manager.CanFish.gameObject.SetActive(true);
                    }
                    else
                    {
                        Manager.CanFish.gameObject.SetActive(false);
                    }

                    GameObject droppedItem = collider.gameObject;
                    if(droppedItem.CompareTag("bugDropped"))
                    {
                        CanThrowRod = true;
                        CanFish = true;
                        Destroy(droppedItem);
                    }
                }
            }
            else
            {
                CanThrowRod = false;
               Manager.CanFish.gameObject.SetActive(false);
            }
        }
        else
        {
            CanThrowRod = false;
            Manager.CanFish.gameObject.SetActive(false);
        }
    }

    void CheckCrouching()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !IsSprinting)
        {
            IsCrouching = true;
            _model.transform.localScale = _scaleCrouch;
            Controller.center = _controllerScaleCrouch;
            _currentSpeed = _crouchingSpeed;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            IsCrouching = false;
            _model.transform.localScale = _scaleDefault;
            Controller.center = _controllerScaleDefault;
            _currentSpeed = _defaultSpeed;
        }
    }

    void CheckSprinting()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !IsCrouching)
        {
            _currentSpeed = _sprintingSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _currentSpeed = _defaultSpeed;
        }
    }

}