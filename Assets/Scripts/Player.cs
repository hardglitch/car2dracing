using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private GameObject carPlayer;
    private GameObject[] carCompetitors;

    private WheelJoint2D[] wheelJoints;
    private JointMotor2D wheels;

    [SerializeField] private float acceleration = 200;
    [SerializeField] private float maxSpeed = 1500;
    [SerializeField] private float inertion = 2;

    [SerializeField] private Image angleTimerImage;
    private readonly float angleTimer = 5;
    private float angleTime;

    private readonly int maxHealth = 3;
    private int currentHealth;
    private int coins = 0;

    [SerializeField] private HUD objHUD;
    [SerializeField] private SFXManager sfxManager;
    [SerializeField] private Player player;
    [SerializeField] private CarScript carScript;
    [SerializeField] private Scoreboard scoreBoard;

    private Controllers contr;
    private int[] heroNumbers;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        carCompetitors = new GameObject[Global.GetMaxCar()];
        carPlayer = transform.Find("Car " + Global.GetCar().ToString()).gameObject;

        foreach (Transform trans in carPlayer.GetComponentsInChildren<Transform>())
            trans.gameObject.layer = LayerMask.NameToLayer("Player");

        carPlayer.SetActive(true);

        carPlayer.GetComponent<CarScript>().SetHUD(objHUD);
        carPlayer.GetComponent<CarScript>().SetSfxManager(sfxManager);
        carPlayer.GetComponent<CarScript>().SetPlayer(player);
        carPlayer.GetComponent<CarScript>().SetScoreboard(scoreBoard);
        carPlayer.transform.Find("Left").GetComponent<OnItemCollision>().SetCar(carScript);
        carPlayer.transform.Find("Right").GetComponent<OnItemCollision>().SetCar(carScript);

        Sprite _sr = transform.Find("Hero " + Global.GetHero().ToString()).GetComponent<SpriteRenderer>().sprite;
        carPlayer.transform.Find("Hero").GetComponent<SpriteRenderer>().sprite = _sr;
        carPlayer.layer = LayerMask.NameToLayer("Player");


        heroNumbers = new int[Global.GetMaxCar()];
        for (int i = 1; i <= heroNumbers.Length; i++)
            heroNumbers[i - 1] = (i == Global.GetHero()) ? 0 : i;


        for (int i = 1; i <= Global.GetMaxCar(); i++)
            if (i != Global.GetCar())
            {
                carCompetitors[i - 1] = transform.Find("Car " + i.ToString()).gameObject;
                carCompetitors[i - 1].layer = LayerMask.NameToLayer("Competitor " + i.ToString());

                foreach (Transform trans in carCompetitors[i - 1].GetComponentsInChildren<Transform>())
                    trans.gameObject.layer = LayerMask.NameToLayer("Competitor " + i.ToString());

                carCompetitors[i - 1].GetComponent<Competitor>().enabled = true;
                carCompetitors[i - 1].GetComponent<CarScript>().SetSfxManager(sfxManager);
                carCompetitors[i - 1].GetComponent<CarScript>().SetScoreboard(scoreBoard);

                carCompetitors[i - 1].transform.Find("Left").GetComponent<OnItemCollision>().SetCar(carScript);
                carCompetitors[i - 1].transform.Find("Right").GetComponent<OnItemCollision>().SetCar(carScript);


                int _randomHeroCell = 0;
                int _hero;
                while (true)
                {
                    _randomHeroCell = (int)UnityEngine.Random.Range(0, Global.GetMaxCar());
                    if (heroNumbers[_randomHeroCell] != 0)
                    {
                        _hero = heroNumbers[_randomHeroCell];
                        heroNumbers[_randomHeroCell] = 0;
                        break;
                    }
                }

                _sr = transform.Find("Hero " + _hero.ToString()).GetComponent<SpriteRenderer>().sprite;
                carCompetitors[i - 1].transform.Find("Hero").GetComponent<SpriteRenderer>().sprite = _sr;

                carCompetitors[i - 1].SetActive(true);
                carCompetitors[i - 1].transform.position = new Vector3(
                    carCompetitors[i - 1].transform.position.x,
                    carCompetitors[i - 1].transform.position.y,
                    carPlayer.transform.position.z + i * 3);
            }

        contr = GetComponent<Controllers>();
        wheelJoints = carPlayer.GetComponents<WheelJoint2D>();
        wheels = wheelJoints[0].motor;
        angleTime = angleTimer;
    }


    private void FixedUpdate()
    {
        try
        {
            // Forward Move (Right)
            if (contr.IsClickedRight() && wheels.motorSpeed >= -maxSpeed)
            {
                if (wheels.motorSpeed > 0)
                    wheels.motorSpeed = 0;
                else
                    wheels.motorSpeed -= acceleration * Time.fixedDeltaTime;
            }
            else
            if (!contr.IsClickedLeft() && wheels.motorSpeed < 0)
                wheels.motorSpeed += acceleration * inertion * Time.fixedDeltaTime;


            // Back Move (Left)
            if (contr.IsClickedLeft() && wheels.motorSpeed <= maxSpeed)
            {
                if (wheels.motorSpeed < 0)
                    wheels.motorSpeed = 0;
                else
                    wheels.motorSpeed += acceleration * Time.fixedDeltaTime;
            }
            else
            if (!contr.IsClickedRight() && wheels.motorSpeed > 0)
                wheels.motorSpeed -= acceleration * inertion * Time.fixedDeltaTime;

            wheelJoints[1].motor = wheels; // backWheel = frontWheel
            wheelJoints[0].motor = wheels;
        }
        catch (Exception error)
        {
            Debug.LogError(error);
        }


        AngleChecker();
    }


    private void AngleChecker()
    {
        if (Mathf.Abs(carPlayer.transform.rotation.z) > 0.5f)
        {
            if ((int)angleTime == (int)angleTimer)
                objHUD.GetComponent<HUD>().TurnOverCounter(true);

            angleTime -= Time.fixedDeltaTime;

            if (angleTime <= 0)
            {
                carPlayer.transform.position = new Vector3(carPlayer.transform.position.x, carPlayer.transform.position.y + 2f, carPlayer.transform.position.z);
                carPlayer.transform.rotation = Quaternion.Euler(0, 0, 0);
                RecountHealth(-1);

                objHUD.GetComponent<HUD>().TurnOverCounter(false);
                angleTime = angleTimer;
            }
            else
                angleTimerImage.fillAmount = angleTime / angleTimer;
        }
        else
        if ((int)angleTime != (int)angleTimer)
        {
            objHUD.GetComponent<HUD>().TurnOverCounter(false);
            angleTime = angleTimer;
        }
    }


    public void RecountHealth(int deltaHealth)
    {
        currentHealth += deltaHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            objHUD.GetComponent<HUD>().MakeRestartScreen();
        }
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        objHUD.MakeHP();
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