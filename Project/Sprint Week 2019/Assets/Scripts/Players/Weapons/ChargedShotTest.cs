using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class ChargedShotTest : MonoBehaviour
{
    public GamePad.Index playerIndex;
    public Vector2 triggerIndex;
    public GameObject shot, firedShot;
    public bool hasShot;

    public sObj_WeaponParams weaponParams;

    [HideInInspector]
    public float localScale, triggerFloat, maxShotSize, minShotSize, chargeSpeed, shotCoolDown, shotSpeed;

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
                firedShot = Instantiate(shot);
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
            }
        }
        else
        {
            if (firedShot != null)
            {
                localScale -= (Time.deltaTime);
                localScale = Mathf.Clamp(localScale, minShotSize, maxShotSize);
                firedShot.transform.localScale = new Vector3(localScale, localScale, localScale);
            }
        }
    }

    void ShootChargedShot(float shotSize, GameObject shot)
    {
        if (shotSize >= maxShotSize)
        {
            hasShot = true;
            shot.transform.localScale = new Vector3(localScale, localScale, localScale);
            shot.GetComponent<Rigidbody2D>().velocity = transform.up * shotSpeed;
            firedShot = null;
            if (firedShot == null)
            {

            }
            StartCoroutine(CountDown());
        }
    }

    private IEnumerator CountDown()
    {
        float duration = shotCoolDown;
        float time = 0;

        while (time <= duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        localScale = 0;
        hasShot = false;
    }
}