using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.UI;

public class PlaceBarrier : MonoBehaviour
{
    public GamePad.Index playerIndex;
    public Vector2 triggerIndex;
    public bool hasShot;

    public ObstacleSpawner obstacleSpawner;

    public Image buildTimer;

    public GridSystem gridSystem;

    public float triggerFloat, triggerAmt, triggerTimer, barrierCoolDown;
    // Start is called before the first frame update
    void Start()
    {
        obstacleSpawner = GetComponent<ObstacleSpawner>();
        gridSystem = GridSystem.Instance;
        hasShot = false;
        buildTimer.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        SpawnBarrier();
        UpdateUI();
    }

    void CheckInput()
    {
        triggerFloat = GamePad.GetTrigger(GamePad.Trigger.RightTrigger, playerIndex);
    }

    void SpawnBarrier()

    {
        if (triggerFloat != 0)
        {
            DetermineSpawnLocation();
            triggerAmt += (Time.deltaTime * triggerFloat) / triggerTimer;
            triggerAmt = Mathf.Clamp(triggerAmt, 0, 1);

            if (!hasShot && triggerAmt >= 1f)
            {
                obstacleSpawner.SpawnObstacle();
                hasShot = true;
                triggerAmt = 0;
                buildTimer.fillAmount = 0;
                StartCoroutine(CountDown());
            }
        }
        else
        {
            triggerAmt -= (Time.deltaTime) / triggerTimer;
        }
    }

    void UpdateUI()
    {
        buildTimer.fillAmount = triggerAmt;
    }

    void DetermineSpawnLocation()
    {
        if (obstacleSpawner.GetClosestEnemy(gridSystem.gridLocations) != null)
        {
            buildTimer.transform.position = obstacleSpawner.GetClosestEnemy(gridSystem.gridLocations).transform.position; 
        }
    }

    private IEnumerator CountDown()
    {
        float duration = barrierCoolDown;
        float time = 0;

        while (time <= duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        hasShot = false;
    }
}
