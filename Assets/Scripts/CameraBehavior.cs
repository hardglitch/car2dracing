using System;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject coinBot;
    private readonly float minX = 0;
    private float maxX = 0;
    private readonly float minY = 0;
    private readonly float maxY = 10f;

    private void Start()
    {
        maxX = Global.GetGroundPrefabSizeX() * Global.GetLevelSize() - 2f;
    }

    void Update()
    {
        CameraPosition();
    }

    private void FixedUpdate()
    {
        if (coinBot.transform.position.x > maxX + 10) Destroy(coinBot);

    }


    void CameraPosition()
    {
        try
        {
            float camX = player.transform.Find("Car " + Global.GetCar().ToString()).position.x;
            float camY = player.transform.Find("Car " + Global.GetCar().ToString()).position.y;

            transform.position = new Vector3(
                Mathf.Clamp(camX, minX, maxX),
                Mathf.Clamp(camY, minY, maxY),
                transform.position.z
            );
        }
        catch (Exception error)
        {
            Debug.LogError(error);
        }
    }
}