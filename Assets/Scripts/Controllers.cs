//This must be on "Players" in Unity

using UnityEngine;

public class Controllers : MonoBehaviour
{
    private bool _isClickedLeft, _isTouchLeft;
    private bool _isClickedRight, _isTouchRight;
    private PlayerController _playerController;


    private void Awake()
    {
        _playerController = new PlayerController();
        _playerController.PlayerControllers.Move.performed += _ => Move();
    }

    private void OnEnable() => _playerController.Enable();

    private void OnDisable() => _playerController.Disable();


    private void Move()
    {
        var moveInput = _playerController.PlayerControllers.Move.ReadValue<float>();

        if (moveInput > 0 || _isTouchRight)
        {
            _isClickedRight = true;
            _isClickedLeft = false;
        }
        else
        if (moveInput < 0 || _isTouchLeft)
        {
            _isClickedRight = false;
            _isClickedLeft = true;
        }
        else
        {
            _isClickedLeft = false;
            _isClickedRight = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public bool IsClickedLeft()
    {
        return _isClickedLeft;
    }

    public bool IsClickedRight()
    {
        return _isClickedRight;
    }
    
    public void ClickedLeft(bool state)
    {
        _isTouchLeft = state;
    }

    public void ClickedRight(bool state)
    {
        _isTouchRight = state;
    }
}