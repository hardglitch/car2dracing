using System;
using UnityEngine;

public class Competitor : MonoBehaviour
{
    private WheelJoint2D[] wheelJoints;
    private JointMotor2D wheels;

    private readonly float acceleration = 200;
    private readonly float maxSpeed = 1500;

    private readonly float angleTimer = 5;
    private float angleTime;

    private readonly int maxHealth = 3;
    private int currentHealth;
    private int coins = 0;

    private bool isFinished = false;


    void Start()
    {
        wheelJoints = GetComponents<WheelJoint2D>();
        wheels = wheelJoints[0].motor;
        currentHealth = maxHealth;
        angleTime = angleTimer;
    }


    private void FixedUpdate()
    {
        if (currentHealth > 0 && !isFinished)
        {
            try
            {
                // Forward Move (Right)
                if (wheels.motorSpeed >= -maxSpeed &&
                    transform.rotation.z < 0.5f)
                {
                    if (wheels.motorSpeed >= -maxSpeed)
                        wheels.motorSpeed = -maxSpeed;
                    else
                    if (wheels.motorSpeed > 0)
                        wheels.motorSpeed = 0;
                    else
                        wheels.motorSpeed -= acceleration * Time.fixedDeltaTime;
                }
                else
                // Back Move (Left)
                {
                    if (wheels.motorSpeed >= maxSpeed)
                        wheels.motorSpeed = maxSpeed;
                    else
                    if (wheels.motorSpeed < 0)
                        wheels.motorSpeed = 0;
                    else
                        wheels.motorSpeed += acceleration * Time.fixedDeltaTime;
                }

                wheelJoints[1].motor = wheels; // backWheel = frontWheel
                wheelJoints[0].motor = wheels;
            }
            catch (Exception error)
            {
                Debug.LogError(error);
            }

            AngleChecker();
        }
        else
        {
            wheels.motorSpeed = 0;
            wheelJoints[1].motor = wheels; // backWheel = frontWheel
            wheelJoints[0].motor = wheels;
        }
    }


    private void AngleChecker()
    {
        if (Mathf.Abs(transform.rotation.z) > 0.5f)
        {
            angleTime -= Time.fixedDeltaTime;

            if (angleTime <= 0)
            {
                if (currentHealth > 1)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    angleTime = angleTimer;
                }
                RecountHealth(-1);
            }
        }
        else
            angleTime = angleTimer;
    }


    public void RecountHealth(int deltaHealth)
    {
        currentHealth += deltaHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }


    public void SetIsFinished(bool isfin)
    {
        isFinished = isfin;
    }

    public bool GetIsFinished()
    {
        return isFinished;
    }

    public int GetCoins()
    {
        return coins;
    }

    public void SetCoins(int _coins)
    {
        coins += _coins;
    }


    public int GetHealth()
    {
        return currentHealth;
    }
}