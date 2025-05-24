using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public Transform Cam;

    private readonly float _ySpeed = -0.5f;
    private readonly float _defaultSpeed = 7f;
    private readonly float _crouchingSpeed = 4f;
    private float _currentSpeed;

    private readonly float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    public bool IsCrouching;

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

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _cameraMovement = _camera.GetComponent<CameraMovement>();

        _journal = GameObject.FindGameObjectWithTag("Journal");
        _journalClass = _journal.GetComponent<Journal>();

        Inventory = GameObject.FindGameObjectWithTag("InventoryManager");
        Manager = Inventory.GetComponent<InventoryManager>();

        _scaleDefault = _model.transform.localScale;
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
                Vector3 velocity = moveDir.normalized * _currentSpeed;
                velocity.y = _ySpeed;  //collider fix

                Controller.Move(velocity * Time.deltaTime);
            }
        }
        CheckCrouching();
        CheckFishing();
    }

    private void CheckFishing()
    {
        if(Physics.CheckSphere(transform.position, 2, waterLayer)) //is in range
        {
            Vector3 direction= (new Vector3(WaterSurface.transform.position.x, 0 , WaterSurface.transform.position.x) - new Vector3(transform.position.x, 0, transform.position.x)).normalized;
            float dotProd = Vector3.Dot(direction, transform.forward);
            
            if (dotProd > 0.1 && dotProd < 0.95) //is facing water
            {
                CanThrowRod = true;
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            IsCrouching = true;
            _model.transform.localScale = _scaleCrouch;
            Controller.center = _controllerScaleCrouch;
            _currentSpeed = _crouchingSpeed;
        }
        else
        {
            IsCrouching = false;
            _model.transform.localScale = _scaleDefault;
            Controller.center = _controllerScaleDefault;
            _currentSpeed = _defaultSpeed;
        }
    }

}