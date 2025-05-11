using UnityEngine;

public class CameraRotationRestriction : MonoBehaviour
{
    private bool _inventoryOpen;

    [SerializeField]
    private Vector2 _cameraClamping = new Vector2(-100, 60);
    private Vector2 turn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _inventoryOpen = !_inventoryOpen;
        }
        if (_inventoryOpen == false)
        {
            turn.y = Mathf.Clamp(turn.y, _cameraClamping.x, _cameraClamping.y);
        }

    }
}
