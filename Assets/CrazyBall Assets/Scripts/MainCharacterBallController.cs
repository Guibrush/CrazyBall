using UnityEngine;
using System.Collections;
using PixelArtRotation;

public class MainCharacterBallController : MonoBehaviour
{
    public float InitialVelocity;

    private Rigidbody2D Rigidbody2DComponent;

    private Vector2 PreviousVelocity;
    private bool BallLaunched;
    private PixelRotation PixelRotationComponent;

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

        PixelRotationComponent = GetComponent<PixelRotation>();
    }

    // Update is called once per frame
    private void Update()
    {
        PreviousVelocity = Rigidbody2DComponent.velocity;

        PixelRotationComponent.Angle += (int)(Time.deltaTime * 100);
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
                float AngleDeviation = (Mathf.Abs(PointDistance) * 45) / coll.gameObject.GetComponent<SpriteRenderer>().bounds.size.x;

                if (PointDistance > 0f)
                {
                    float radAngle = (90f - AngleDeviation) * Mathf.Deg2Rad;
                    ExitMovement = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
                }
                else if (PointDistance < 0f)
                {
                    float radAngle = (90f + AngleDeviation) * Mathf.Deg2Rad;
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
