using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;

public class DefenseKnockback : MonoBehaviour
{

    public GamePad.Index playerIndex;
    public float knockbackForce;
    bool buttonPressA;
    bool ready = true;
    public float cooldownTimer;
    public float knockbackRadius = 3;
    public bool debugLines;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckInput();
        KnockbackEnemy();
    }

    void CheckInput()
    {
        buttonPressA = GamePad.GetButtonDown(GamePad.Button.A, playerIndex);
    }

    void KnockbackEnemy()
    {
        //If cooldown has been met and the button is pressed, commence knockback
        if (buttonPressA && ready)
        {

            //Start cooldown
            ready = false;
            StartCoroutine(KnockbackCooldown());

            //Visual testing for radius
            if (debugLines)
            {
                for (int i = 0; i < 360; i++)
                {
                    Debug.DrawLine(rb.position, new Vector3(rb.position.x + Mathf.Cos(i * Mathf.Deg2Rad) * knockbackRadius,
                                                            rb.position.y + Mathf.Sin(i * Mathf.Deg2Rad) * knockbackRadius,
                                                            0), Color.blue, cooldownTimer);
                }
            }
            

            //Check for collision on zombie layer
            Collider2D[] zombieColliders = Physics2D.OverlapCircleAll(rb.position, knockbackRadius,
                                                                      LayerMask.GetMask("Zombies"));

            //Knockback zombies
            foreach (Collider2D zombieCollider in zombieColliders)
            {
                Rigidbody2D zombieRb = zombieCollider.GetComponent<Rigidbody2D>();
                zombieRb.AddForce((zombieRb.position - rb.position).normalized * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    //Cooldown for knockback
    IEnumerator KnockbackCooldown()
    {
        float tempTimer = 0;
        while (tempTimer < cooldownTimer)
        {
            tempTimer += Time.deltaTime;
            yield return null;
        }
        ready = true;
    }
}
