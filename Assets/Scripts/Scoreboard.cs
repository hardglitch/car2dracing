using System;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private Sprite[] carIcons;
    private string[] winPlaces = { "", "", "" };


    public void PushPlaceholderValue(string _carName)
    {
        int _carNumber = Convert.ToInt32(_carName.Substring(_carName.Length-1));

        for (int i=0; i<winPlaces.Length; i++)
            if (winPlaces[i].Equals("") && !winPlaces[0].Equals(_carName) && !winPlaces[1].Equals(_carName))
            {
                winPlaces[i] = _carName;
                SetPlaceholderValue(i + 1, _carNumber);
                break;
            }
    }

    public void SetPlaceholderValue(int _placeholderNumber, int _carNumber)
    {
        if (_placeholderNumber < 1) _placeholderNumber = 1;
        if (_placeholderNumber > 3) _placeholderNumber = 3;
        if (_carNumber < 1) _carNumber = 1;
        if (_carNumber > 6) _carNumber = 6;

        GameObject.Find("Placeholder " + _placeholderNumber.ToString()).GetComponent<SpriteRenderer>().sprite = carIcons[_carNumber - 1];
    }
}
