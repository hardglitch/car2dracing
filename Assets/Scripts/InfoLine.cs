using System;
using UnityEngine;
using UnityEngine.Serialization;

public class InfoLine : MonoBehaviour
{

    [Serializable]
    public class CarInfo
    {
        [SerializeField] private GameObject carGO;
        [SerializeField] private GameObject carIcon;

        public CarInfo() { }

        public float GetPosXLevel()
        {
            return carGO.transform.position.x;
        }

        public void SetPosXInfoLine(float newPosX)
        {
            carIcon.transform.localPosition = new Vector2(newPosX, carIcon.transform.localPosition.y);
        }
    }


    [SerializeField] private CarInfo[] cars;
    [FormerlySerializedAs("_minX")] [SerializeField] private GameObject minX;
    private const float _minXInfoLine = -560;
    private const float _maxXInfoLine = 566;

    private float _posXInfoLine = 0,
                  _minXLevel = 0,
                  _maxXLevel = 0;


    private void Start()
    {
        _minXLevel = minX.transform.position.x;
        _maxXLevel = Global.GroundPrefabSizeX * Global.LevelSize - 5.6f;
    }


    private void Update()
    {
        for (var i=0; i<Global.MaxCar; i++)
        {
            _posXInfoLine = _minXInfoLine + cars[i].GetPosXLevel() * (_maxXInfoLine - _minXInfoLine) / ((_maxXLevel - _minXLevel));
            cars[i].SetPosXInfoLine(_posXInfoLine);
        }
    }
}
