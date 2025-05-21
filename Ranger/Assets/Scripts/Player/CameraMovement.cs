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

    public float CollisionSensitivity = 4.5f;

    private RaycastHit _camHit;
    private Vector3 _camDist;

    public bool _inventoryOpen;

    Journal _journalClass;
    [SerializeField]
    private GameObject _journal;

    [SerializeField]
    private LayerMask _playerMask;

    void Start()
    {
        _camDist = transform.localPosition;

        _journal = GameObject.FindGameObjectWithTag("Journal");
        _journalClass = _journal.GetComponent<Journal>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && _journalClass._journalOpen == false)
        {
            _inventoryOpen = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            _inventoryOpen = false;
        }

        if (_inventoryOpen == false && _journalClass._journalOpen == false)
        {
            CameraCenter.transform.position = new Vector3(Character.transform.position.x, Character.transform.position.y + OffsetY, Character.transform.position.z);

            var rotation = Quaternion.Euler(CameraCenter.transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * Sensitivity / 2,
                                            CameraCenter.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * Sensitivity,
                                            CameraCenter.transform.rotation.eulerAngles.z);

            CameraCenter.transform.rotation = rotation;

            Cam.transform.localPosition = _camDist;

            //no clipping into the floor
            GameObject obj = new GameObject();
            obj.transform.SetParent(Cam.transform.parent);
            obj.transform.localPosition = new Vector3(Cam.transform.localPosition.x, Cam.transform.localPosition.y, Cam.transform.localPosition.z - CollisionSensitivity);

            if (Physics.Linecast(CameraCenter.transform.position, obj.transform.position, out _camHit, ~_playerMask))
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