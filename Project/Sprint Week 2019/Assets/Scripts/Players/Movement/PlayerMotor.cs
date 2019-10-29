using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
public class PlayerMotor : MonoBehaviour
{
    public GamePad.Index playerIndex;
    public Rigidbody2D rb;
    public Vector2 moveDir;
    public Vector2 lastMoveDir;
    public Vector2 aimDir;
    public Vector2 lastAimDir;
    public float maxSpeed;
    public float currentSpeed;
    float accel;
    public float accelTime;
    public float decelTime;
    public bool isStarting;
    public bool isStopping;
    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if (moveDir == Vector2.zero && currentSpeed > 0 && !isStopping)
        {
            CalcAccel(0, decelTime);
            isStopping = true;
            isStarting = false;
        }
        if (moveDir != Vector2.zero && currentSpeed < maxSpeed && !isStarting)
        {
            CalcAccel(maxSpeed, decelTime);
            isStopping = false;
            isStarting = true;
        }
        
        if (isStarting || isStopping)
        {
            Accelerate();
        }

        if (moveDir != Vector2.zero)
        {
            lastMoveDir = moveDir;
        }

        if (aimDir != Vector2.zero)
        {
            lastAimDir = aimDir;
        }

        rb.velocity = lastMoveDir.normalized * currentSpeed;
        transform.LookAt(transform.position + (Vector3)lastAimDir.normalized,Vector3.up);
    }

    public void CheckInput()
    {
        moveDir = GamePad.GetAxis(GamePad.Axis.LeftStick, playerIndex);
        aimDir = GamePad.GetAxis(GamePad.Axis.RightStick, playerIndex);
    }

    public void CalcAccel(float trgtSpd, float timeToAccel)
    {
        accel = (trgtSpd - currentSpeed) / timeToAccel;
    }

    public void Accelerate()
    {
        currentSpeed += accel * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        if (currentSpeed == 0 || currentSpeed == maxSpeed)
        {
            isStopping = false;
            isStarting = false;
        }
    }
}
