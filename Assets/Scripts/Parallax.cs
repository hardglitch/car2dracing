using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _lengthX, _startPositionX, _startPositionY;
    [SerializeField] private GameObject cam;

    [Range(0, 1)]
    [SerializeField] private float parallaxEffect = 0.5f;


    private void Start()
    {
        _startPositionX = transform.position.x;
        _startPositionY = transform.position.y;
        _lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
    }


    private void FixedUpdate()
    {
        var camX = cam.transform.position.x * (1 - parallaxEffect);
        var distanceX = cam.transform.position.x * parallaxEffect;
        var distanceY = cam.transform.position.y * parallaxEffect;

        transform.position = new Vector3(_startPositionX + distanceX, _startPositionY + distanceY, transform.position.z);

        if (camX >= _startPositionX + _lengthX * 0.9)
            _startPositionX += _lengthX;
        else if (camX <= _startPositionX - _lengthX * 0.9)
            _startPositionX -= _lengthX;
    }
}
