using System;
using UnityEngine;

public class Competitor : MonoBehaviour
{
    private WheelJoint2D[] _wheelJoints;
    private JointMotor2D _wheels;

    private const float _acceleration = 200;
    private const float _maxSpeed = 1500;

    private const float _angleTimer = 5;
    private float _angleTime;

    private const int _maxHealth = 3;
    private int _currentHealth;
    private int _coins = 0;

    private bool _isFinished = false;


    private void Start()
    {
        _wheelJoints = GetComponents<WheelJoint2D>();
        _wheels = _wheelJoints[0].motor;
        _currentHealth = _maxHealth;
        _angleTime = _angleTimer;
    }


    private void FixedUpdate()
    {
        if (_currentHealth > 0 && !_isFinished)
        {
            try
            {
                // Forward Move (Right)
                if (_wheels.motorSpeed >= -_maxSpeed &&
                    transform.rotation.z < 0.5f)
                {
                    if (_wheels.motorSpeed >= -_maxSpeed)
                        _wheels.motorSpeed = -_maxSpeed;
                    else
                    if (_wheels.motorSpeed > 0)
                        _wheels.motorSpeed = 0;
                    else
                        _wheels.motorSpeed -= _acceleration * Time.fixedDeltaTime;
                }
                else
                // Back Move (Left)
                {
                    if (_wheels.motorSpeed >= _maxSpeed)
                        _wheels.motorSpeed = _maxSpeed;
                    else
                    if (_wheels.motorSpeed < 0)
                        _wheels.motorSpeed = 0;
                    else
                        _wheels.motorSpeed += _acceleration * Time.fixedDeltaTime;
                }

                _wheelJoints[1].motor = _wheels; // backWheel = frontWheel
                _wheelJoints[0].motor = _wheels;
            }
            catch (Exception error)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                Debug.LogError(error);
            }

            AngleChecker();
        }
        else
        {
            _wheels.motorSpeed = 0;
            _wheelJoints[1].motor = _wheels; // backWheel = frontWheel
            _wheelJoints[0].motor = _wheels;
        }
    }


    private void AngleChecker()
    {
        if (Mathf.Abs(transform.rotation.z) > 0.5f)
        {
            _angleTime -= Time.fixedDeltaTime;

            if (!(_angleTime <= 0)) return;
            if (_currentHealth > 1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _angleTime = _angleTimer;
            }
            RecountHealth(-1);
        }
        else
            _angleTime = _angleTimer;
    }


    private void RecountHealth(int deltaHealth)
    {
        _currentHealth += deltaHealth;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
        }
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }


    public void SetIsFinished(bool isFinish)
    {
        _isFinished = isFinish;
    }

    public bool GetIsFinished()
    {
        return _isFinished;
    }

    public int GetCoins()
    {
        return _coins;
    }

    public void SetCoins(int coins)
    {
        this._coins += coins;
    }


    public int GetHealth()
    {
        return _currentHealth;
    }
}