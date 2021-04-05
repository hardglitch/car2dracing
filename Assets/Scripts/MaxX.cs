using UnityEngine;

public class MaxX : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(
            Global.GetGroundPrefabSizeX() * Global.GetLevelSize() + 5.6f,
            -4f,
            transform.position.z);
    }
}
