using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Character;
    public GameObject CameraCenter;
    public float OffsetY = 1f;
    public float Sensitivity = 3f;
    public Camera Cam;

    public float ScrollSensitivity = 5;
    public float ScrollDampening = 6f;

    //zoom
    //fix it later
    public float ZoomMin = 3.5f;
    public float ZoomMax = 15f;
    public float ZoomDefault = 10f;
    public float ZoomDistance;

    public float CollisionSensitivity = 4.5f;

    private RaycastHit _camHit;
    private Vector3 _camDist;

    public bool _inventoryOpen;

    void Start()
    {
        _camDist = transform.localPosition;
        ZoomDistance = ZoomDefault;
        _camDist.z = ZoomDistance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _inventoryOpen = !_inventoryOpen;
        }
        if (_inventoryOpen == false)
        {
            CameraCenter.transform.position = new Vector3(Character.transform.position.x, Character.transform.position.y + OffsetY, Character.transform.position.z);

            var rotation = Quaternion.Euler(CameraCenter.transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * Sensitivity / 2,
                                            CameraCenter.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * Sensitivity,
                                            CameraCenter.transform.rotation.eulerAngles.z);

            CameraCenter.transform.rotation = rotation;

            /* //fix it later
            if(Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                var scrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;
                scrollAmount *= ZoomDistance * 0.1f;
                ZoomDistance *= -scrollAmount;
                ZoomDistance = Mathf.Clamp(ZoomDistance,ZoomMin,ZoomMax);


            if (ZoomDistance < ZoomMin)
            {
                ZoomDistance = ZoomMin + 0.5f;
            }
            if (ZoomDistance > ZoomMax)
            {
                ZoomDistance = ZoomMax - 0.5f;
            }
            }*/

            if (_camDist.z != -ZoomDistance) //fix it later
            {
                _camDist.z = Mathf.Lerp(_camDist.z, -ZoomDistance, Time.deltaTime * ScrollDampening);

            }

            Cam.transform.localPosition = _camDist;

            //no clipping into the floor
            GameObject obj = new GameObject();
            obj.transform.SetParent(Cam.transform.parent);
            obj.transform.localPosition = new Vector3(Cam.transform.localPosition.x, Cam.transform.localPosition.y, Cam.transform.localPosition.z - CollisionSensitivity);

            if (Physics.Linecast(CameraCenter.transform.position, obj.transform.position, out _camHit))
            {
                Cam.transform.position = _camHit.point;

                var localPosition = new Vector3(Cam.transform.localPosition.x, Cam.transform.localPosition.y, Cam.transform.localPosition.z - CollisionSensitivity);
                Cam.transform.localPosition = localPosition;
            }

            Destroy(obj);


            if (Cam.transform.localPosition.z > -1f)
            {
                Cam.transform.localPosition = new Vector3(Cam.transform.localPosition.x, Cam.transform.localPosition.y, -1f);
            }
        }
    }
}
