//This must be on "Players" in Unity

using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Players : MonoBehaviour
{
    [SerializeField] private Hud hudObj;
    [SerializeField] private SfxManager sfxManagerObj;
    [SerializeField] private Controllers controllersObj;
    [SerializeField] private Scoreboard scoreboardObj;
    [SerializeField] private InfoLine infoLineObj;
    private GameObject[] _carGO;
    private GameObject _playerCar;
    private int[] _heroNumbers;
    private int _heroNumber;

    
    private void Awake()
    {
        PrepareCars();
        //throw new NotImplementedException();
    }

    private void PrepareCars()
    {
        _carGO = new GameObject[Global.MaxCar];

        for (var i=1; i<=Global.MaxCar; i++)
        {
            _carGO[i-1] = transform.Find("Car " + i).gameObject;
            _carGO[i-1].GetComponent<Car>().CompetitorMode = true;
            _carGO[i-1].GetComponent<AngleTimer>().CarObj = _carGO[i-1].GetComponent<Car>();
            _carGO[i-1].GetComponent<AngleTimer>().CompetitorMode = true;
            _carGO[i-1].GetComponent<Car>().GetComponent<Collisions>().CarObj = _carGO[i-1].GetComponent<Car>();
            _carGO[i-1].GetComponent<Car>().GetComponent<Collisions>().ScoreboardObj = scoreboardObj;
            _carGO[i-1].GetComponent<Car>().GetComponent<Collisions>().sfxManagerObj = sfxManagerObj;

            _carGO[i-1].GetComponent<Car>().gameObject.layer = LayerMask.NameToLayer("Competitor " + i);
            foreach (var child in _carGO[i-1].GetComponent<Car>().GetComponentsInChildren<Transform>())
                child.gameObject.layer = LayerMask.NameToLayer("Competitor " + i);
        }

        _playerCar = _carGO[Global.Car-1].GetComponent<Car>().CarPrefab;
        _carGO[Global.Car-1].GetComponent<Car>().HudObj = hudObj;
        _carGO[Global.Car-1].GetComponent<Car>().CompetitorMode = false;
        _carGO[Global.Car-1].GetComponent<Car>().GetComponent<Collisions>().HudObj = hudObj;
        _carGO[Global.Car-1].GetComponent<AngleTimer>().CompetitorMode = false;
        _carGO[Global.Car-1].GetComponent<AngleTimer>().HudObj = hudObj;
        _carGO[Global.Car-1].GetComponent<Car>().ControllersObj = controllersObj;

        scoreboardObj.Cars = _carGO;
        hudObj.Player = _carGO[Global.Car-1].GetComponent<Car>();

        foreach (var trans in _playerCar.GetComponentsInChildren<Transform>())
            trans.gameObject.layer = LayerMask.NameToLayer(layerName: "Player");

        _playerCar.SetActive(true);
        
        var sr = transform.Find("Hero " + Global.Hero).GetComponent<SpriteRenderer>().sprite;
        _playerCar.transform.Find("Hero").GetComponent<SpriteRenderer>().sprite = sr;
        _playerCar.layer = LayerMask.NameToLayer("Player");

        _heroNumbers = new int[Global.MaxCar];
        for (var i = 1; i <= _heroNumbers.Length; i++)
            _heroNumbers[i - 1] = (i == Global.Hero) ? 0 : i;
        
        for (var i = 1; i <= Global.MaxCar; i++)
            if (i != Global.Car)
            {
                RandomHero();

                sr = transform.Find("Hero " + _heroNumber).GetComponent<SpriteRenderer>().sprite;
                _carGO[i - 1].transform.Find("Hero").GetComponent<SpriteRenderer>().sprite = sr;

                _carGO[i - 1].gameObject.SetActive(true);
                _carGO[i - 1].transform.position = new Vector3(
                    _carGO[i - 1].transform.position.x,
                    _carGO[i - 1].transform.position.y,
                    _playerCar.transform.position.z + i * 3);
            }
    }

    private void RandomHero()
    {
        while (true)
        {
            var randomHeroCell = Random.Range(0, Global.MaxCar);
            if (_heroNumbers[randomHeroCell] == 0) continue;
            _heroNumber = _heroNumbers[randomHeroCell];
            _heroNumbers[randomHeroCell] = 0;
            break;
        }
    }
}