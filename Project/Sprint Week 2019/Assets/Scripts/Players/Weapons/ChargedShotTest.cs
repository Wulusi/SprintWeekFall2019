using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;

public class ChargedShotTest : MonoBehaviour
{
    public GamePad.Index playerIndex;
    public Vector2 triggerIndex;
    public GameObject shot, firedShot;
    public bool hasShot, autoFire, isCountDown;
    public Element currentElement;
    public Color elementColour;
    public sObj_WeaponParams weaponParams;

    [HideInInspector]
    public float localScale, maxShotSize, minShotSize, minShotThreshold, chargeSpeed, shotCoolDown, shotSpeed;

    public float triggerFloatRight, triggerFloatLeft;
    public int numShotsInBurst;
    public int burstShotsLeft;
    public float timeBetweenBursts;
    public float burstTimer, minBurstTime, maxBurstTime;
    public Transform barrel;

    public Image BurstCoolDown;
    // Start is called before the first frame update
    void Start()
    {
        GetData();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        InflateObject();
    }

    void GetData()
    {
        hasShot = false;
        localScale = 0;
        maxShotSize = weaponParams._maxShotSize;
        minShotSize = weaponParams._minShotSize;
        chargeSpeed = weaponParams._chargeSpeed;
        shotCoolDown = weaponParams._shotCoolDown;
        shotSpeed = weaponParams._shotSpeed;
        minShotThreshold = weaponParams._minShotThreshold;
        shot = weaponParams.playerShot;
        elementColour = Color.yellow;
        maxBurstTime = timeBetweenBursts;
        BurstCoolDown = transform.GetChild(0).GetComponentInChildren<Image>();
        isCountDown = false;
    }


    void CheckInput()
    {
        triggerFloatRight = GamePad.GetTrigger(GamePad.Trigger.RightTrigger, playerIndex);
        triggerFloatLeft = GamePad.GetTrigger(GamePad.Trigger.LeftTrigger, playerIndex);
        //Debug.Log("LeftTriggerFloat is " + triggerFloatLeft);
    }

    void InflateObject()

    {
        if (triggerFloatRight != 0 && triggerFloatLeft <= 0)
        {
            if (!hasShot)
            {
                firedShot = PoolManager.Instance.SpawnFromPool(shot.name, barrel.transform.position + barrel.transform.up, Quaternion.identity);
                firedShot.GetComponent<BulletDmg>().elementType = currentElement;
                firedShot.GetComponent<SpriteRenderer>().color = elementColour;
                firedShot.GetComponentInChildren<SpriteRenderer>().color = elementColour;
                hasShot = true;
                firedShot.transform.position = transform.position + transform.up;
            }
            else
            {
                if (firedShot != null)
                {
                    firedShot.transform.position = barrel.transform.position + barrel.transform.up;
                    localScale += (Time.deltaTime * triggerFloatRight * chargeSpeed);
                    localScale = Mathf.Clamp(localScale, minShotSize, maxShotSize);
                    firedShot.transform.localScale = new Vector3(localScale, localScale, localScale);
                    ShootChargedShot(localScale, firedShot);
                }

            }
        }
        else
        {
            if (firedShot != null)
            {
                firedShot.transform.position = barrel.transform.position + barrel.transform.up;
                localScale -= (Time.deltaTime);
                localScale = Mathf.Clamp(localScale, minShotSize, maxShotSize);
                firedShot.transform.localScale = new Vector3(localScale, localScale, localScale);
                ShootShot(localScale, firedShot);

            }
        }

        if (triggerFloatLeft != 0 && triggerFloatRight <= 0 && burstShotsLeft > 0)
        {
            if (!hasShot)
            {
                firedShot = PoolManager.Instance.SpawnFromPool(shot.name, barrel.transform.position + barrel.transform.up, Quaternion.identity);
                firedShot.GetComponent<BulletDmg>().elementType = currentElement;
                firedShot.GetComponent<SpriteRenderer>().color = elementColour;
                firedShot.GetComponentInChildren<SpriteRenderer>().color = elementColour;
                hasShot = true;
                firedShot.transform.position = barrel.transform.position + barrel.transform.up;
            }
            else
            {
                if (firedShot != null)
                {
                    firedShot.transform.position = barrel.transform.position + barrel.transform.up;
                    localScale += (Time.deltaTime * triggerFloatLeft * chargeSpeed);
                    localScale = Mathf.Clamp(localScale, minShotSize, maxShotSize);
                    firedShot.transform.localScale = new Vector3(localScale, localScale, localScale);
                    StartCoroutine(CommenceAutoFire(0.1f));
                    ShootShot(localScale, firedShot);
                }
            }
        }
        else
        {
            if (!isCountDown)
            {
                isCountDown = true;
                StartCoroutine(BurstCoolDownMeter(timeBetweenBursts));
            }
            //burstTimer += Time.deltaTime;

            //if (burstTimer >= timeBetweenBursts)
            //{

            //    burstTimer = 0;

            //    autoFire = false;
            //}
        }

        if (triggerFloatLeft <= 0)
        {
            autoFire = false;
            StopCoroutine(CommenceAutoFire(0f));
        }
    }

    void ShootChargedShot(float shotSize, GameObject shot)
    {
        if (shotSize >= maxShotSize)
        {
            shot.transform.localScale = new Vector3(localScale, localScale, localScale);

            //If the trigger was released
            if (triggerFloatRight <= 0)
            {
                hasShot = true;
                shot.GetComponent<Rigidbody2D>().velocity = barrel.transform.up * shotSpeed;
                firedShot = null;
                autoFire = false;
                StartCoroutine(CountDown(shotCoolDown));
            }

            if (firedShot == null)
            {
                //Do nothing here
            }
        }
    }

    void ShootShot(float shotSize, GameObject shot)
    {
        if (shotSize >= minShotThreshold && !autoFire && burstShotsLeft > 0)
        {
            hasShot = true;
            shot.transform.localScale = new Vector3(localScale, localScale, localScale);
            shot.GetComponent<Rigidbody2D>().velocity = barrel.transform.up * shotSpeed;
            firedShot = null;

            if (firedShot == null)
            {
                //Do nothing here
            }

            StartCoroutine(CountDown(shotCoolDown));
        }
        else if (shotSize >= minShotThreshold * 0.5f && autoFire)
        {
            if (burstShotsLeft > 0)
            {
                hasShot = true;
                shot.transform.localScale = new Vector3(localScale, localScale, localScale);
                shot.GetComponent<Rigidbody2D>().velocity = barrel.transform.up * shotSpeed;
                firedShot = null;
                StartCoroutine(CountDown(shotCoolDown * 0.1f));
                burstShotsLeft--;
            }
        }

        if (shotSize >= minShotThreshold && !autoFire)
        {
            hasShot = true;
            shot.transform.localScale = new Vector3(localScale, localScale, localScale);
            shot.GetComponent<Rigidbody2D>().velocity = barrel.transform.up * shotSpeed;
            firedShot = null;

            if (firedShot == null)
            {
                //Do nothing here
            }

            StartCoroutine(CountDown(shotCoolDown));
        }
    }

    private IEnumerator CountDown(float coolDown)
    {
        float duration = coolDown;
        float time = 0;

        while (time <= duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        localScale = 0;
        hasShot = false;
    }

    private IEnumerator CommenceAutoFire(float countDown)
    {
        float duration = countDown;
        float time = 0;

        while (time <= duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        autoFire = true;
    }

    private IEnumerator BurstCoolDownMeter(float coolDown)
    {
        float duration = coolDown;
        float time = 0;

        while (time <= duration)
        {
            time += Time.deltaTime;
            //if (isCountDown)
            //{
            //    BurstCoolDown.fillAmount = time / duration;
            //} else
            //{
            //    BurstCoolDown.fillAmount = 1f;
            //}
            yield return null;
        }
        burstShotsLeft = numShotsInBurst;
        isCountDown = false;
        autoFire = false;
    }
}