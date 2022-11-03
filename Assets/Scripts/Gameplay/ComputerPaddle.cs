using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ComputerPaddle : BasePaddle
{

    [SerializeField]
    Rigidbody2D ball;

    [SerializeField]
    [Range(3.0f, 13.0f)]
    private float _speed = 8f;

   protected void FixedUpdate()
    {
        /* 
            Does clampmagnitude work in both directions?
        */
        //rb.AddForce(new Vector2(0, ball.position.y - rb.position.y ) * _speed);
        rb.velocity = Vector2.ClampMagnitude(new Vector2(0, ball.position.y - rb.position.y ) * _speed, _speed);
        //rb.velocity =  new Vector2(0, ball.position.y - rb.position.y ) * _speed;

         if(rb.velocity.y == 0)
        {
                surfaceEffector.enabled = false;
        }
        else{
            surfaceEffector.enabled = true;
            surfaceEffector.speed =  rb.velocity.y < 0 ? -3 : 3; 
        }
        
    }
}
