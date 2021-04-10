using UnityEngine;
using UnityEngine.InputSystem;

public class Controllers : MonoBehaviour
{
    private bool _isClickedLeft = false;
    private bool _isClickedRight = false;
    private PlayerController _playerController;


    private void Awake()
    {
        _playerController = new PlayerController();
        _playerController.PlayerControllers.Move.performed += _ => Move();
    }

    private void OnEnable()
    { _playerController.Enable(); }

    private void OnDisable()
    { _playerController.Disable(); }

    private void Move()
    {
        var moveInput = _playerController.PlayerControllers.Move.ReadValue<float>();

        if (moveInput > 0) ClickedRight(true);
        else
        if (moveInput < 0) ClickedLeft(true);
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

    public void ClickedLeft(bool isClicked)
    { 
        _isClickedLeft = isClicked;
        _isClickedRight = !isClicked;
    }

    public void ClickedRight(bool isClicked)
    {
        _isClickedRight = isClicked;
        _isClickedLeft = !isClicked;
    }

    public bool IsClickedLeft()
    { return _isClickedLeft; }

    public bool IsClickedRight()
    { return _isClickedRight; }


}