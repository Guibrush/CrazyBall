using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class MainCharacterController : MonoBehaviour
{
    public float MaxVelocity;
    public GameObject Ball;

    private Rigidbody2D rigidbody2DComponent;

	// Use this for initialization
	private void Start ()
    {
        rigidbody2DComponent = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void Update ()
    {
	}

    private void FixedUpdate()
    {
        // Read the inputs.
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        // Move the character
        rigidbody2DComponent.velocity = new Vector2(h * MaxVelocity, rigidbody2DComponent.velocity.y);
    }
}
