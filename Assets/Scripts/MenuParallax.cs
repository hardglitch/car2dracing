using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    private float startPositionX;
    [SerializeField] private GameObject cam;

    [Range(0, 1)]
    [SerializeField] private float parallaxEffect = 0.5f;


    void Start()
    {
        startPositionX = transform.position.x;
    }


    void FixedUpdate()
    {
        float distanceX = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPositionX + distanceX, transform.position.y, transform.position.z);
    }
}
