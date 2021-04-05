using System;
using UnityEngine;

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
    [SerializeField] private GameObject _minX;
    private float minX_InfoLine = -560,
                  maxX_InfoLine = 566,
                  posX_InfoLine = 0,
                  minX_Level = 0,
                  maxX_Level = 0;


    void Start()
    {
        minX_Level = _minX.transform.position.x;
        maxX_Level = Global.GetGroundPrefabSizeX() * Global.GetLevelSize() - 5.6f;
    }


    void Update()
    {
        for (int i=0; i<Global.GetMaxCar(); i++)
        {
            posX_InfoLine = minX_InfoLine + cars[i].GetPosXLevel() * (maxX_InfoLine - minX_InfoLine) / ((maxX_Level - minX_Level));
            cars[i].SetPosXInfoLine(posX_InfoLine);
        }
    }
}
