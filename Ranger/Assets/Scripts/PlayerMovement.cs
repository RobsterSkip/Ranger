using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public Transform Cam;

    public float Speed = 7f;
    //private float RotationSpeed = 150;

    private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity; 

    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime, 0f , Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime).normalized;
        //transform.Rotate(0, Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime, 0);

        if(direction.magnitude >= 0.1f) //smooth turn
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Controller.Move(moveDir.normalized * Speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (collider.tag == "plant")
            {
                Debug.Log("plant picked up"); //figure out a better way of checking what was picked
                Destroy(collider.gameObject); // figure out how to send the tag to the inventory
            }
        }
    }
}
