using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lengthX, startPositionX, startPositionY;
    [SerializeField] private GameObject cam;

    [Range(0, 1)]
    [SerializeField] private float parallaxEffect = 0.5f;


    void Start()
    {
        startPositionX = transform.position.x;
        startPositionY = transform.position.y;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
    }


    void FixedUpdate()
    {
        float camX = cam.transform.position.x * (1 - parallaxEffect);
        float distanceX = cam.transform.position.x * parallaxEffect;
        float distanceY = cam.transform.position.y * parallaxEffect;

        transform.position = new Vector3(startPositionX + distanceX, startPositionY + distanceY, transform.position.z);

        if (camX >= startPositionX + lengthX * 0.9)
            startPositionX += lengthX;
        else if (camX <= startPositionX - lengthX * 0.9)
            startPositionX -= lengthX;
    }
}
