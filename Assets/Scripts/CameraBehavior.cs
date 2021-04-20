using System;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private GameObject players;
    [SerializeField] private GameObject coinBot;
    private Transform _players;
    private const float MinX = 0;
    private float _maxX;
    private const float MinY = 0;
    private const float MaxY = 10f;

    private void Start()
    {
        _maxX = Global.GroundPrefabSizeX * Global.LevelSize - 2f;
        _players = players.transform.Find("Car " + Global.Car);
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
            var camX = _players.position.x;
            var camY = _players.position.y;

            transform.position = new Vector3(
                Mathf.Clamp(camX, MinX, _maxX),
                Mathf.Clamp(camY, MinY, MaxY),
                transform.position.z
            );
        }
        catch (Exception error)
        {
            Debug.LogError(error);
        }
    }
}