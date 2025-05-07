using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public Transform Cam;

    private float _defaultSpeed = 7f;
    private float _crouchingSpeed = 4f;
    private float _currentSpeed;
    private float _ySpeed = -0.5f;

    private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    private bool _inventoryOpen;

    public bool IsCrouching;

    public bool CanFish;
    [SerializeField] LayerMask waterLayer;
    public GameObject WaterSurface;

    public bool IsFishing;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _inventoryOpen = !_inventoryOpen;
        }

        if (_inventoryOpen == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (_inventoryOpen == false)
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

            CheckCrouching();
            CheckFishing();

            if (IsFishing)
            {
                CanFish = false;
            }

            //Debug.Log(CanFish);
        }
    }

    private void CheckFishing()
    {
        if(Physics.CheckSphere(transform.position, 2, waterLayer)) //is in range
        {
            Vector3 direction= (new Vector3(WaterSurface.transform.position.x, 0 , WaterSurface.transform.position.x) - new Vector3(transform.position.x, 0, transform.position.x)).normalized;
            float dotProd = Vector3.Dot(direction, transform.forward);

            //Debug.Log(CanFish);
            
            if (dotProd > 0.1 && dotProd < 0.95) //is facing water
            {
                CanFish = true;
            }
            else
            {
                CanFish = false;
            }
        }
        else
        {
            CanFish = false;
        }
    }

    void CheckCrouching()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            IsCrouching = true;
            _currentSpeed = _crouchingSpeed;
        }
        else
        {
            IsCrouching = false;
            _currentSpeed = _defaultSpeed;
        }
    }
}