using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaddle : BasePaddle
{

    [SerializeField]
    [Range(5.0f, 25.0f)]
    private float _speed = 8f;

    // Start is called before the first frame update
    private void Start()
    {
       
    }

    // Update is called once per frame
    private void Update()
    {

        if (Touchscreen.current.primaryTouch.press.IsPressed())
        {
            _direction = Camera.main.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());
        }
        else if (Keyboard.current.wKey.IsActuated(0))
        {
            _direction = Vector2.up;

        }
        else if (Keyboard.current.sKey.IsActuated(0))
       // else if (Input.GetKey(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else
        {
            _direction = Vector2.zero;
        }

        //paddleX.text = rb.velocity.y.ToString();
    }

protected void FixedUpdate()
    {
        if(_direction.sqrMagnitude != 0)
        {
           //rb.AddForce(_direction * _speed);
            rb.velocity = _direction * _speed;
        }

        //up to certain speed has no effect on ball direction
         if(rb.velocity.y < 1 && rb.velocity.y > -1)
        {
                surfaceEffector.enabled = false;
        }
        else{
            surfaceEffector.enabled = true;
            surfaceEffector.speed =  rb.velocity.y < 0 ? 5 : -5; 
        }
        
    }
}
