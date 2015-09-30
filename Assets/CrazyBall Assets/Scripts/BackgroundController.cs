using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundController : MonoBehaviour
{
    public GameObject BackgroundPrefab;
    public int InitialBackgrounds;
    public float DestroyDistance;
    public float BackgroundSpeed;
    public float BackgroundHeight;

    [HideInInspector]
    public Vector3 CurrentBackgroundVelocity;

    private List<GameObject> Backgrounds;
    private SpriteRenderer BackgroundSprite;
    private CameraController CameraControllerComponent;

	// Use this for initialization
	void Start ()
    {
        CameraControllerComponent = GetComponent<CameraController>();
        BackgroundSprite = BackgroundPrefab.GetComponent<SpriteRenderer>();

        Backgrounds = new List<GameObject>();

        for (int i = 1; i <= InitialBackgrounds; i++)
        {
            float floor = Mathf.FloorToInt(InitialBackgrounds / 2);
            float ceil = floor + 1;

            Vector3 Pos;
            if (i <= floor)
            {
                Pos = new Vector3(transform.position.x - (BackgroundSprite.bounds.size.x * (ceil - i)), BackgroundHeight);
            }
            else if (i > ceil)
            {
                Pos = new Vector3(transform.position.x + (BackgroundSprite.bounds.size.x * (i - ceil)), BackgroundHeight);
            }
            else
            {
                Pos = new Vector3(transform.position.x, BackgroundHeight);
            }

            Backgrounds.Add(Instantiate(BackgroundPrefab, Pos, transform.rotation) as GameObject);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        float CameraVelocity = CameraControllerComponent.CurrentVelocity.x;

        if (CameraVelocity != 0f)
        {
            foreach (GameObject Background in Backgrounds)
            {
                Vector3 NewPos = new Vector3(Background.transform.position.x + BackgroundSpeed * CameraVelocity, BackgroundHeight);
                Background.transform.position = NewPos;
            }
        }

        float CharacterVelocity = CameraControllerComponent.Target.GetComponent<Rigidbody2D>().velocity.x;
        float BackDistance;

        BackDistance = Backgrounds[0].transform.position.x - transform.position.x;
        if ((Mathf.Abs(BackDistance) > DestroyDistance) && (BackDistance < 0) && (CharacterVelocity > 0))
        {
            Vector3 NewPos = new Vector3(Backgrounds[Backgrounds.Count - 1].transform.position.x + BackgroundSprite.bounds.size.x, BackgroundHeight);
            Backgrounds.Add(Instantiate(BackgroundPrefab, NewPos, transform.rotation) as GameObject);

            GameObject TempBack = Backgrounds[0];

            Backgrounds.Remove(Backgrounds[0]);

            Destroy(TempBack);
        }
        else 
        {
            BackDistance = Backgrounds[Backgrounds.Count - 1].transform.position.x - transform.position.x;
            if ((Mathf.Abs(BackDistance) > DestroyDistance) && (BackDistance > 0) && (CharacterVelocity < 0))
            {
                Vector3 NewPos = new Vector3(Backgrounds[0].transform.position.x - BackgroundSprite.bounds.size.x, 10);
                Backgrounds.Insert(0, Instantiate(BackgroundPrefab, NewPos, transform.rotation) as GameObject);

                GameObject TempBack = Backgrounds[Backgrounds.Count - 1];

                Backgrounds.Remove(Backgrounds[Backgrounds.Count - 1]);

                Destroy(TempBack);
            }
        }
	}
}
