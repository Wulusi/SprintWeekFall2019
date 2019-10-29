using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class PlaceBarrier : MonoBehaviour
{

    public GamePad.Index playerIndex;
    public Vector2 triggerIndex;
    public GameObject shot, firedShot;
    public bool hasShot;

    public ObstacleSpawner obstacleSpawner;

    public float triggerFloat;
    // Start is called before the first frame update
    void Start()
    {
        obstacleSpawner = GetComponent<ObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        SpawnBarrier();
    }

    void CheckInput()
    {
        triggerFloat = GamePad.GetTrigger(GamePad.Trigger.RightTrigger, playerIndex);
    }

    void SpawnBarrier()

    {
        if (triggerFloat != 0)
        {
            if (!hasShot)
            {
                obstacleSpawner.SpawnObstacle();
                hasShot = true;
            }
            else
            {

            }
        }
    }
}
