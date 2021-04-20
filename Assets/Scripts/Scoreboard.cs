//This may be on "Scoreboard" in Unity

using System;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    internal GameObject[] Cars { get; set; }
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
        if (carNumber > Global.MaxCar) carNumber = Global.MaxCar;

        GameObject.Find("Placeholder " + placeholderNumber).GetComponent<SpriteRenderer>().sprite =
            Cars[carNumber - 1].transform.Find("Icon").GetComponent<SpriteRenderer>().sprite;
    }
}
