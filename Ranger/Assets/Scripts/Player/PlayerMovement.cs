using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public Transform Cam;

    public float Speed = 7f;
    private float _ySpeed = -0.5f;
    //private float RotationSpeed = 150;

    private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    private bool _inventoryOpen;

    public bool IsCrouching;

    public bool CanFish;
    [SerializeField] LayerMask waterLayer;
    public GameObject WaterSurface;

    private void Start()
    {
        if(_inventoryOpen == false)
        {
           Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
           // _inventoryOpen = !_inventoryOpen;
        }


        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime, 
                                        0f, Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime).normalized;

        if (direction.magnitude >= 0.1f) //smooth turn
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Vector3 velocity = moveDir.normalized * Speed;
            velocity.y = _ySpeed;  //collider fix

            Controller.Move(velocity * Time.deltaTime);
        }

        CheckCrouching();
        CheckFishing();

        Debug.Log(CanFish);
    }

    void CheckFishing()
    {
        if(Physics.CheckSphere(transform.position, 2, waterLayer)) //is in range
        {
            Vector3 direction= (new Vector3(WaterSurface.transform.position.x, 0 , WaterSurface.transform.position.x) - new Vector3(transform.position.x, 0, transform.position.x)).normalized;
            float dotProd = Vector3.Dot(direction, transform.forward);
            
            if (dotProd > 0.1 && dotProd < 0.9) //is facing water
            {
                CanFish = true;
            }
            else
            {
                CanFish = false;
            }
        }
    }

    void CheckCrouching()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            IsCrouching = true;
        }
        else
        {
            IsCrouching = false;
        }
    }
}