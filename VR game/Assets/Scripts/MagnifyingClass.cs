using UnityEngine;

public class MagnifierLensController : MonoBehaviour
{
    public Transform lens;
    public Transform VRCamera;

    private void Update()
    {
        Vector3 player  = lens.InverseTransformPoint(VRCamera.position);

        Vector3 mirror = lens.TransformPoint(new Vector3(-player.x, -player.y, -player.z));
        transform.LookAt(mirror, lens.up);
    }
}