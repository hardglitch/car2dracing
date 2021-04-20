using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    private float _startPositionX;
    [SerializeField] private GameObject heroScreen, carScreen, levelScreen;
    [SerializeField] private GameObject heroContent, carContent, levelContent;

    [Range(0, 1)]
    [SerializeField] private float parallaxEffect = 0.5f;


    private void Start()
    {
        _startPositionX = transform.position.x;
    }


    private void FixedUpdate()
    {
        var distanceX = 0f;
        if (heroScreen.activeSelf)
        {
            distanceX = heroContent.transform.position.x * parallaxEffect;
            print("OK");
        }
        else if (carScreen.activeSelf)
        {
            distanceX = carContent.transform.position.x * parallaxEffect;
            print("OK222");
        }
        else if (levelScreen.activeSelf)
        {
            distanceX = levelContent.transform.position.x * parallaxEffect;
            print("OK---");
        }

        transform.position = new Vector3(_startPositionX + distanceX, transform.position.y, transform.position.z);
    }
}
