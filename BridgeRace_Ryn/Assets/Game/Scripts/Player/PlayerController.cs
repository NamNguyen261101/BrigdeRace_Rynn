using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField] private VariableJoystick _joyStick;
    
    [SerializeField] private Canvas _inputCanvas;
    [SerializeField] private bool _isJoyStick;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private Vector3 _moveVector;
    // Start is called before the first frame update
    void Start()
    {
        EnableJoyStickInput();    
    }

    private void EnableJoyStickInput()
    {
        _isJoyStick = true;
        _inputCanvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputJoyStick();
    }

    private void HandleInputJoyStick()
    {
        if (_isJoyStick)
        {
            _moveVector = Vector3.zero;
            _moveVector.x = _joyStick.Horizontal * _moveSpeed * Time.deltaTime;
            _moveVector.y = _joyStick.Vertical * _moveSpeed * Time.deltaTime;

            if (_joyStick.Horizontal != 0 || _joyStick.Vertical != 0)
            {
                Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _rotateSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(direction);


            }
            else if (_joyStick.Horizontal == 0 && _joyStick.Horizontal == 0) 
            { 
            
            }

            Rigidbody.MovePosition(Rigidbody.position + _moveVector);
        }
    }
}
