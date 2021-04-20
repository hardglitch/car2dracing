//This must be on CarPrefab in Unity

using System;
using UnityEngine;
using UnityEngine.UI;

public class AngleTimer : MonoBehaviour
{
    // ReSharper disable once InconsistentNaming
    private const float AngleTIMER = 5;
    private float _angleTime;

    internal Car CarObj { get; set; }
    internal Hud HudObj { get; set; }
    internal bool CompetitorMode { get; set; }

    
    private void FixedUpdate()
    {
        AngleChecker();
    }


    // ReSharper disable Unity.PerformanceAnalysis
    public void AngleChecker()
    {
        if (Mathf.Abs(transform.rotation.z) > 0.5f)
        {
            if (!CompetitorMode)
            {
                if ((int)_angleTime == (int)AngleTIMER)
                {
                    HudObj.TurnOverCounter(true);
                }

                _angleTime -= Time.fixedDeltaTime;

                if (_angleTime <= 0)
                {
                    var position = transform.position;
                    position = new Vector3(position.x, position.y + 2f, position.z);
                    transform.position = position;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    CarObj.RecountHealth(-1);

                    HudObj.TurnOverCounter(false);
                    _angleTime = AngleTIMER;
                }
                else
                {
                    var obj = HudObj.AngleTimerObj;
                    if (obj is { }) obj.GetComponent<Image>().fillAmount = _angleTime / AngleTIMER;
                }
            }
            else
            {
                _angleTime -= Time.fixedDeltaTime;

                if (!(_angleTime <= 0)) return;
                if (CarObj.Health > 1)
                {
                    var position = transform.position;
                    position = new Vector3(position.x, position.y + 2f, position.z);
                    transform.position = position;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    _angleTime = AngleTIMER;
                }
                CarObj.RecountHealth(-1);
            }
        }
        else
        if ((int)_angleTime != (int)AngleTIMER)
        {
            if (!CompetitorMode) HudObj.TurnOverCounter(false);
            _angleTime = AngleTIMER;
        }
    }
}