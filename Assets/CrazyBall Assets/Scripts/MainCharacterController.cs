using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class MainCharacterController : MonoBehaviour
{
    public float MaxVelocity;
    public float JumpForce;
    public GameObject BallPrefab;

    private Rigidbody2D Rigidbody2DComponent;
    private SpriteRenderer SpriteComponent;

    private GameObject Ball;

    private bool BallLaunched = false;
    private bool FacingRight = true;

    public Vector2 GetBallInitialPosition()
    {
        return new Vector2(transform.position.x, transform.position.y + SpriteComponent.bounds.size.y);
    }

	// Use this for initialization
	private void Start ()
    {
        Rigidbody2DComponent = GetComponent<Rigidbody2D>();
        SpriteComponent = GetComponent<SpriteRenderer>();

        Vector3 newPosition = GetBallInitialPosition();
        Ball = Instantiate(BallPrefab, newPosition, transform.rotation) as GameObject;
	}
	
	// Update is called once per frame
	private void Update ()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && (Rigidbody2DComponent.velocity.y == 0))
        {
            Rigidbody2DComponent.AddForce(new Vector2(0f, JumpForce));
        }
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

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
