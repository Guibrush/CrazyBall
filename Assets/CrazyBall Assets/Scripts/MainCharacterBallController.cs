using UnityEngine;
using System.Collections;

public class MainCharacterBallController : MonoBehaviour
{
    public float InitialVelocity;

    private Rigidbody2D Rigidbody2DComponent;

    private Vector2 PreviousVelocity;
    private bool BallLaunched;

    public void LaunchBall()
    {
        Rigidbody2DComponent.velocity = new Vector2(0, InitialVelocity);
        BallLaunched = true;
    }

    // Use this for initialization
    private void Start()
    {
        Rigidbody2DComponent = GetComponent<Rigidbody2D>();
        BallLaunched = false;
    }

    // Update is called once per frame
    private void Update()
    {
        PreviousVelocity = Rigidbody2DComponent.velocity;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (Rigidbody2DComponent && BallLaunched)
        {
            Vector2 ExitMovement = Vector2.zero;
            if (coll.gameObject.name == "MainCharacter")
            {
                Vector2 BallInitialPosition = coll.gameObject.GetComponent<MainCharacterController>().GetBallInitialPosition();
                Vector2 ContactPoint = coll.contacts[0].point;
                float PointDistance = ContactPoint.x - BallInitialPosition.x;

                if (PointDistance > 0.5f)
                {
                    float radAngle = 45f * Mathf.Deg2Rad;
                    ExitMovement = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
                }
                else if (PointDistance < -0.5f)
                {
                    float radAngle = 135f * Mathf.Deg2Rad;
                    ExitMovement = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
                }
                else
                {
                    ExitMovement = coll.contacts[0].normal;
                }
            }
            else
            {
                Vector2 ContactNormal = new Vector2(coll.contacts[0].normal.x, coll.contacts[0].normal.y);
                Vector2 MovementNormal = PreviousVelocity.normalized;

                ExitMovement = Vector2.Reflect(MovementNormal, ContactNormal);
            }

            Rigidbody2DComponent.velocity = ExitMovement * InitialVelocity;
        }
    }
}
