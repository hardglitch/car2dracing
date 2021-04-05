using UnityEngine;

public class Controllers : MonoBehaviour
{
    private bool isClickedLeft = false;
    private bool isClickedRight = false;

    public void ClickedLeft(bool isClicked)
    {
        isClickedLeft = isClicked;
    }

    public void ClickedRight(bool isClicked)
    {
        isClickedRight = isClicked;
    }

    public bool IsClickedLeft()
    {
        return isClickedLeft;
    }

    public bool IsClickedRight()
    {
        return isClickedRight;
    }
}