using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    public float FollowVelocity;
    public Vector2 LookOffset;

    [HideInInspector]
    public Vector3 CurrentVelocity;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 newPos = Target.transform.position + new Vector3(LookOffset.x, LookOffset.y) - Vector3.forward;

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref CurrentVelocity, FollowVelocity);
    }
}
