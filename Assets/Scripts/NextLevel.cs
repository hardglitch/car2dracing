using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private void Start()
    {
        if (Global.Level == Global.MaxLevel) gameObject.SetActive(false);
    }
}
