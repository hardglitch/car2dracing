//This must be on CarPrefab in Unity

using System;
using UnityEngine;

public class Car: MonoBehaviour
{
    [Header("Car Parameters")]
    [SerializeField] private float acceleration = 200;
    [SerializeField] private float maxSpeed = 1500;
    [SerializeField] private float inertia = 2;
    
    [Header("Audio")]
    [SerializeField] private float maxPitch = 3;
    private AudioSource _audioSource;

    internal GameObject CarPrefab => gameObject;
    internal GameObject CarIcon => CarPrefab.transform.Find("Icon").gameObject;

    private const int MAXHealth = 3;
    internal int Health { get; private set; } = MAXHealth;
    internal int Coins { get; set; }
    internal bool IsFinished { get; private set; } = false;
    
    private WheelJoint2D[] _wheelJoints = new WheelJoint2D[2];
    private JointMotor2D _wheels;
    private AngleTimer _angleTimer;
    
    internal Controllers ControllersObj { get; set; }
    internal Hud HudObj { get; set; }
    internal bool CompetitorMode { get; set; } = false;

    private void Start()
    {
        _wheelJoints = CarPrefab.GetComponents<WheelJoint2D>();
        _wheels = _wheelJoints[0].motor;
        _angleTimer = GetComponent<AngleTimer>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch = 0;
    }

    private void FixedUpdate()
    {
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        CarMovement();
    }

    
    public void RecountHealth(int deltaHealth)
    {
        Health += deltaHealth;

        if (Health <= 0)
        {
            Health = 0;
            if (!CompetitorMode) HudObj.MakeRestartScreen();
        }

        if (Health > MAXHealth)
            Health = MAXHealth;

        if (!CompetitorMode) HudObj.MakeHp();
    }


    // ReSharper disable Unity.PerformanceAnalysis
    private void CarMovement()
    {
        if (!CompetitorMode)
        {
            try
            {
                // Forward Move (Right)
                if (ControllersObj.IsClickedRight() && _wheels.motorSpeed >= -maxSpeed)
                {
                    if (_wheels.motorSpeed > 0)
                        _wheels.motorSpeed = 0;
                    else
                        _wheels.motorSpeed -= acceleration * Time.fixedDeltaTime;
                }
                else if (!ControllersObj.IsClickedLeft() && _wheels.motorSpeed < 0)
                    _wheels.motorSpeed += acceleration * inertia * Time.fixedDeltaTime;


                // Back Move (Left)
                if (ControllersObj.IsClickedLeft() && _wheels.motorSpeed <= maxSpeed)
                {
                    if (_wheels.motorSpeed < 0)
                        _wheels.motorSpeed = 0;
                    else
                        _wheels.motorSpeed += acceleration * Time.fixedDeltaTime;
                }
                else if (!ControllersObj.IsClickedRight() && _wheels.motorSpeed > 0)
                    _wheels.motorSpeed -= acceleration * inertia * Time.fixedDeltaTime;

                _wheelJoints[1].motor = _wheels; // backWheel = frontWheel
                _wheelJoints[0].motor = _wheels;

                _audioSource.pitch = maxPitch * Mathf.Abs(_wheels.motorSpeed / maxSpeed);
                
                _angleTimer.AngleChecker();
            }
            catch (Exception error)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                Debug.LogError(error);
            }
        }

        else
        
        if (Health > 0 && !IsFinished)
        {
            try
            {
                // Forward Move (Right)
                if (_wheels.motorSpeed >= -maxSpeed &&
                    transform.rotation.z < 0.5f)
                {
                    if (_wheels.motorSpeed >= -maxSpeed)
                        _wheels.motorSpeed = -maxSpeed;
                    else
                    if (_wheels.motorSpeed > 0)
                        _wheels.motorSpeed = 0;
                    else
                        _wheels.motorSpeed -= acceleration * Time.fixedDeltaTime;
                }
                else
                // Back Move (Left)
                {
                    if (_wheels.motorSpeed >= maxSpeed)
                        _wheels.motorSpeed = maxSpeed;
                    else
                    if (_wheels.motorSpeed < 0)
                        _wheels.motorSpeed = 0;
                    else
                        _wheels.motorSpeed += acceleration * Time.fixedDeltaTime;
                }

                _wheelJoints[1].motor = _wheels; // backWheel = frontWheel
                _wheelJoints[0].motor = _wheels;
            }
            catch (Exception error)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                Debug.LogError(error);
            }

            GetComponent<AngleTimer>().AngleChecker();
        }
        else
        {
            _wheels.motorSpeed = 0;
            _wheelJoints[1].motor = _wheels; // backWheel = frontWheel
            _wheelJoints[0].motor = _wheels;
        }
    }
}