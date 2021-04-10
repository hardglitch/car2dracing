using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private GameObject _carPlayer;
    private GameObject[] _carCompetitors;

    private WheelJoint2D[] _wheelJoints;
    private JointMotor2D _wheels;

    [SerializeField] private float acceleration = 200;
    [SerializeField] private float maxSpeed = 1500;
    [FormerlySerializedAs("inertia")] [SerializeField] private float inertia = 2;

    [SerializeField] private Image angleTimerImage;
    private const float _angleTimer = 5;
    private float _angleTime;

    private const int _maxHealth = 3;
    private int _currentHealth;
    private int _coins = 0;

    [FormerlySerializedAs("objHUD")] [SerializeField] private Hud objHud;
    [SerializeField] private SfxManager sfxManager;
    [SerializeField] private Player player;
    [SerializeField] private CarScript carScript;
    [SerializeField] private Scoreboard scoreBoard;

    private Controllers _controllers;
    private int[] _heroNumbers;


    private void Awake()
    {
        _currentHealth = _maxHealth;
    }


    private void Start()
    {
        _carCompetitors = new GameObject[Global.MaxCar];
        _carPlayer = transform.Find("Car " + Global.Car.ToString()).gameObject;

        foreach (var trans in _carPlayer.GetComponentsInChildren<Transform>())
            trans.gameObject.layer = LayerMask.NameToLayer("Player");

        _carPlayer.SetActive(true);

        _carPlayer.GetComponent<CarScript>().SetHud(objHud);
        _carPlayer.GetComponent<CarScript>().SetSfxManager(sfxManager);
        _carPlayer.GetComponent<CarScript>().SetPlayer(player);
        _carPlayer.GetComponent<CarScript>().SetScoreboard(scoreBoard);

        foreach (var component in _carPlayer.GetComponentsInChildren<OnItemCollision>())
            component.SetCar(carScript);

        var sr = transform.Find("Hero " + Global.Hero.ToString()).GetComponent<SpriteRenderer>().sprite;
        _carPlayer.transform.Find("Hero").GetComponent<SpriteRenderer>().sprite = sr;
        _carPlayer.layer = LayerMask.NameToLayer("Player");


        _heroNumbers = new int[Global.MaxCar];
        for (var i = 1; i <= _heroNumbers.Length; i++)
            _heroNumbers[i - 1] = (i == Global.Hero) ? 0 : i;


        for (var i = 1; i <= Global.MaxCar; i++)
            if (i != Global.Car)
            {
                _carCompetitors[i - 1] = transform.Find("Car " + i.ToString()).gameObject;
                _carCompetitors[i - 1].layer = LayerMask.NameToLayer("Competitor " + i.ToString());

                foreach (var trans in _carCompetitors[i - 1].GetComponentsInChildren<Transform>())
                    trans.gameObject.layer = LayerMask.NameToLayer("Competitor " + i.ToString());

                _carCompetitors[i - 1].GetComponent<Competitor>().enabled = true;
                _carCompetitors[i - 1].GetComponent<CarScript>().SetSfxManager(sfxManager);
                _carCompetitors[i - 1].GetComponent<CarScript>().SetScoreboard(scoreBoard);

                foreach (var component in _carCompetitors[i - 1].GetComponentsInChildren<OnItemCollision>())
                    component.SetCar(carScript);


                int hero;
                while (true)
                {
                    var randomHeroCell = (int)UnityEngine.Random.Range(0, Global.MaxCar);
                    if (_heroNumbers[randomHeroCell] == 0) continue;
                    hero = _heroNumbers[randomHeroCell];
                    _heroNumbers[randomHeroCell] = 0;
                    break;
                }

                sr = transform.Find("Hero " + hero.ToString()).GetComponent<SpriteRenderer>().sprite;
                _carCompetitors[i - 1].transform.Find("Hero").GetComponent<SpriteRenderer>().sprite = sr;

                _carCompetitors[i - 1].SetActive(true);
                _carCompetitors[i - 1].transform.position = new Vector3(
                    _carCompetitors[i - 1].transform.position.x,
                    _carCompetitors[i - 1].transform.position.y,
                    _carPlayer.transform.position.z + i * 3);
            }

        _controllers = GetComponent<Controllers>();
        _wheelJoints = _carPlayer.GetComponents<WheelJoint2D>();
        _wheels = _wheelJoints[0].motor;
        _angleTime = _angleTimer;
    }


    private void FixedUpdate()
    {
        try
        {
            // Forward Move (Right)
            if (_controllers.IsClickedRight() && _wheels.motorSpeed >= -maxSpeed)
            {
                if (_wheels.motorSpeed > 0)
                    _wheels.motorSpeed = 0;
                else
                    _wheels.motorSpeed -= acceleration * Time.fixedDeltaTime;
            }
            else
            if (!_controllers.IsClickedLeft() && _wheels.motorSpeed < 0)
                _wheels.motorSpeed += acceleration * inertia * Time.fixedDeltaTime;


            // Back Move (Left)
            if (_controllers.IsClickedLeft() && _wheels.motorSpeed <= maxSpeed)
            {
                if (_wheels.motorSpeed < 0)
                    _wheels.motorSpeed = 0;
                else
                    _wheels.motorSpeed += acceleration * Time.fixedDeltaTime;
            }
            else
            if (!_controllers.IsClickedRight() && _wheels.motorSpeed > 0)
                _wheels.motorSpeed -= acceleration * inertia * Time.fixedDeltaTime;

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


    // ReSharper disable Unity.PerformanceAnalysis
    private void AngleChecker()
    {
        if (Mathf.Abs(_carPlayer.transform.rotation.z) > 0.5f)
        {
            if ((int)_angleTime == (int)_angleTimer)
                objHud.GetComponent<Hud>().TurnOverCounter(true);

            _angleTime -= Time.fixedDeltaTime;

            if (_angleTime <= 0)
            {
                _carPlayer.transform.position = new Vector3(_carPlayer.transform.position.x, _carPlayer.transform.position.y + 2f, _carPlayer.transform.position.z);
                _carPlayer.transform.rotation = Quaternion.Euler(0, 0, 0);
                RecountHealth(-1);

                objHud.GetComponent<Hud>().TurnOverCounter(false);
                _angleTime = _angleTimer;
            }
            else
                angleTimerImage.fillAmount = _angleTime / _angleTimer;
        }
        else
        if ((int)_angleTime != (int)_angleTimer)
        {
            objHud.GetComponent<Hud>().TurnOverCounter(false);
            _angleTime = _angleTimer;
        }
    }


    public void RecountHealth(int deltaHealth)
    {
        _currentHealth += deltaHealth;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            objHud.GetComponent<Hud>().MakeRestartScreen();
        }
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        objHud.MakeHp();
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