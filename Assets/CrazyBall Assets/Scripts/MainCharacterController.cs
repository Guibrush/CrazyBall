using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class MainCharacterController : MonoBehaviour
{
    public float MaxVelocity;
    public GameObject BallPrefab;

    private Rigidbody2D Rigidbody2DComponent;
    private SpriteRenderer SpriteComponent;

    private GameObject Ball;

    private bool BallLaunched;

    public Vector2 GetBallInitialPosition()
    {
        return new Vector2(transform.position.x, transform.position.y + SpriteComponent.bounds.size.y);
    }

	// Use this for initialization
	private void Start ()
    {
        BallLaunched = false;

        Rigidbody2DComponent = GetComponent<Rigidbody2D>();
        SpriteComponent = GetComponent<SpriteRenderer>();

        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + SpriteComponent.bounds.size.y);
        Ball = Instantiate(BallPrefab, newPosition, transform.rotation) as GameObject;
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
        Rigidbody2DComponent.velocity = new Vector2(h * MaxVelocity, Rigidbody2DComponent.velocity.y);

        if (!BallLaunched && (h != 0.0f))
        {
            Ball.GetComponent<MainCharacterBallController>().LaunchBall();

            BallLaunched = true;
        }
    }
}
