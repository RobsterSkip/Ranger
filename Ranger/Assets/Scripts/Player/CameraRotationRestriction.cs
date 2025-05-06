using UnityEngine;

public class CameraRotationRestriction : MonoBehaviour
{
    private float RestrictionAngle = 15f;
    private bool _inventoryOpen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _inventoryOpen = !_inventoryOpen;
        }
        if (_inventoryOpen == false)
        {
            var rotation = UnityEditor.TransformUtils.GetInspectorRotation(transform);

            if (rotation.x > RestrictionAngle) //restriction for max Y
            {
                UnityEditor.TransformUtils.SetInspectorRotation(transform, new Vector3(RestrictionAngle, rotation.y, rotation.z));
            }
        }

    }
}
