using System;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject coinBot;
    private Transform _player;
    private const float _minX = 0;
    private float _maxX = 0;
    private const float _minY = 0;
    private const float _maxY = 10f;

    private void Start()
    {
        _maxX = Global.GroundPrefabSizeX * Global.LevelSize - 2f;
        _player = player.transform.Find("Car " + Global.Car.ToString());
    }

    private void Update()
    {
        CameraPosition();
    }

    private void FixedUpdate()
    {
        if (coinBot.transform.position.x > _maxX + 10) Destroy(coinBot);

    }


    // ReSharper disable Unity.PerformanceAnalysis
    private void CameraPosition()
    {
        try
        {
            var camX = _player.position.x;
            var camY = _player.position.y;

            transform.position = new Vector3(
                Mathf.Clamp(camX, _minX, _maxX),
                Mathf.Clamp(camY, _minY, _maxY),
                transform.position.z
            );
        }
        catch (Exception error)
        {
            Debug.LogError(error);
        }
    }
}