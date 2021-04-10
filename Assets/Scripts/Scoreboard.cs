using System;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private Sprite[] carIcons;
    private readonly string[] _winPlaces = { "", "", "" };


    public void PushPlaceholderValue(string carName)
    {
        var carNumber = Convert.ToInt32(carName.Substring(carName.Length-1));

        for (var i=0; i<_winPlaces.Length; i++)
            if (_winPlaces[i].Equals("") && !_winPlaces[0].Equals(carName) && !_winPlaces[1].Equals(carName))
            {
                _winPlaces[i] = carName;
                SetPlaceholderValue(i + 1, carNumber);
                break;
            }
    }

    private void SetPlaceholderValue(int placeholderNumber, int carNumber)
    {
        if (placeholderNumber < 1) placeholderNumber = 1;
        if (placeholderNumber > 3) placeholderNumber = 3;
        if (carNumber < 1) carNumber = 1;
        if (carNumber > 6) carNumber = 6;

        GameObject.Find("Placeholder " + placeholderNumber.ToString()).GetComponent<SpriteRenderer>().sprite = carIcons[carNumber - 1];
    }
}
