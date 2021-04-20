//This should be on "Main Camera" in Unity

using System;
using UnityEngine;

public class InfoLine : MonoBehaviour
{
    [Serializable]
    public class CarInfo
    {
        [SerializeField] private GameObject carGO;
        [SerializeField] private GameObject carIcon;
        
        protected internal float? PosXLevel => carGO.transform.position.x;
        protected internal float? PosXInfoLine
        {
            // ReSharper disable once Unity.InefficientPropertyAccess
            set
            {
                if (carIcon is null || value == null) return;
                carIcon.transform.localPosition = new Vector3(
                    (float) value,
                    carIcon.transform.localPosition.y,
                    // ReSharper disable once Unity.InefficientPropertyAccess
                    carIcon.transform.localPosition.z);
            }
        }
    }


    [SerializeField] private CarInfo[] cars;
    [SerializeField] private GameObject minX;
    private const float MinXInfoLine = -560,
                        MaxXInfoLine = 566;

    private float? _posXInfoLine;

    private float _minXLevel,
                  _maxXLevel;


    private void Start()
    {
        //cars = new CarInfo[Global.MaxCar];
        _minXLevel = minX.transform.position.x;
        _maxXLevel = Global.GroundPrefabSizeX * Global.LevelSize - 5.6f;
    }


    private void Update()
    {
        for (var i=0; i<Global.MaxCar; i++)
        {
            _posXInfoLine = MinXInfoLine + cars[i].PosXLevel * (MaxXInfoLine - MinXInfoLine) / ((_maxXLevel - _minXLevel));
            cars[i].PosXInfoLine = _posXInfoLine;
        }
    }
}
