using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public Transform Cam;

    public float Speed = 7f;
    //private float RotationSpeed = 150;

    private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    private bool _inventoryOpen;

    public bool IsCrouching;
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
            Controller.Move(moveDir.normalized * Speed * Time.deltaTime);
        }

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