using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class ChargedShotTest : MonoBehaviour
{
    public GamePad.Index playerIndex;
    public Vector2 triggerIndex;
    public GameObject shot, firedShot;
    public bool hasShot, autoFire;

    public sObj_WeaponParams weaponParams;

    [HideInInspector]
    public float localScale, triggerFloat, maxShotSize, minShotSize, minShotThreshold, chargeSpeed, shotCoolDown, shotSpeed;

    public Transform barrel;

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
    }


    void CheckInput()
    {
        triggerFloat = GamePad.GetTrigger(GamePad.Trigger.RightTrigger, playerIndex);
    }

    void InflateObject()

    {
        if (triggerFloat != 0)
        {
            if (!hasShot)
            {
                firedShot = PoolManager.Instance.SpawnFromPool(shot.name, transform.position + transform.up, Quaternion.identity);
                hasShot = true;
                firedShot.transform.position = transform.position + transform.up;
            }
            else
            {
                if (firedShot != null)
                {
                    firedShot.transform.position = transform.position + transform.up;
                    localScale += (Time.deltaTime * triggerFloat * chargeSpeed);
                    localScale = Mathf.Clamp(localScale, minShotSize, maxShotSize);
                    firedShot.transform.localScale = new Vector3(localScale, localScale, localScale);
                    ShootChargedShot(localScale, firedShot);
                }


                if (firedShot != null && autoFire)
                {
                    firedShot.transform.position = transform.position + transform.up;
                    localScale += (Time.deltaTime * triggerFloat * chargeSpeed);
                    localScale = Mathf.Clamp(localScale, minShotSize, maxShotSize);
                    firedShot.transform.localScale = new Vector3(localScale, localScale, localScale);
                    ShootShot(localScale, firedShot);
                }
            }
        }
        else
        {
            if (firedShot != null)
            {
                firedShot.transform.position = transform.position + transform.up;
                localScale -= (Time.deltaTime);
                localScale = Mathf.Clamp(localScale, minShotSize, maxShotSize);
                firedShot.transform.localScale = new Vector3(localScale, localScale, localScale);
                ShootShot(localScale, firedShot);
            
            }
        }

        if (triggerFloat <= 0)
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
            StartCoroutine(CommenceAutoFire(5f));

            //If the trigger was released
            if (triggerFloat <= 0)
            {
                hasShot = true;
                shot.GetComponent<Rigidbody2D>().velocity = transform.up * shotSpeed;
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
        if (shotSize >= minShotThreshold && !autoFire)
        {
            hasShot = true;
            shot.transform.localScale = new Vector3(localScale, localScale, localScale);
            shot.GetComponent<Rigidbody2D>().velocity = transform.up * shotSpeed;
            firedShot = null;

            if (firedShot == null)
            {
                //Do nothing here
            }

            StartCoroutine(CountDown(shotCoolDown));
        }
        else if (shotSize >= minShotThreshold * 0.5f && autoFire)
        {
            hasShot = true;
            shot.transform.localScale = new Vector3(localScale, localScale, localScale);
            shot.GetComponent<Rigidbody2D>().velocity = transform.up * shotSpeed;
            firedShot = null;
            StartCoroutine(CountDown(shotCoolDown * 0.1f));
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
}