using UnityEngine;

public class CameraRotationRestriction : MonoBehaviour
{
    private float RestrictionAngle = 15f;

    void Update()
    {
        var rotation = UnityEditor.TransformUtils.GetInspectorRotation(transform);

        if(rotation.x > RestrictionAngle) //restriction for max Y
        {
            UnityEditor.TransformUtils.SetInspectorRotation(transform, new Vector3(RestrictionAngle, rotation.y, rotation.z));
        }

    }
}
