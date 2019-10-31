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
    public Image knockbackTimer;
    public GameObject knockbackObject;
    SpriteRenderer knockbackSprite;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        knockbackTimer.fillAmount = 0;
        knockbackSprite = knockbackObject.GetComponent<SpriteRenderer>();
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
                Rigidbody2D zombieRb = zombieCollider.GetComponentInParent<Rigidbody2D>();
                zombieRb.AddForce((zombieRb.position - rb.position).normalized * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    //Cooldown for knockback
    IEnumerator KnockbackCooldown()
    {
        float tempTimer = 0;
        float expandTimer = 0;
        float fadeTimer = 1.0f;
        float expandRadius = knockbackRadius * 6;
        knockbackSprite.color = new Color(knockbackSprite.color.r, knockbackSprite.color.g, knockbackSprite.color.b, 1.0f);
        while (tempTimer < cooldownTimer)
        {
            knockbackObject.transform.localScale = new Vector2(Mathf.Lerp(0, expandRadius * 0.08f, expandTimer * 0.08f), Mathf.Lerp(0, expandRadius * 0.08f, expandTimer * 0.08f));
            knockbackTimer.fillAmount = tempTimer / cooldownTimer;
            if (expandTimer<cooldownTimer/2) expandTimer += Time.deltaTime*10f;
            else
            {
                knockbackSprite.color = new Color(knockbackSprite.color.r, knockbackSprite.color.g, knockbackSprite.color.b, Mathf.Lerp(0.0f, 1.0f, fadeTimer));
                fadeTimer -= Time.deltaTime;
            }
            tempTimer += Time.deltaTime;
            yield return null;
        }
        knockbackTimer.fillAmount = 0;
        ready = true;
    }
}
