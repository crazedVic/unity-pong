using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePaddle : MonoBehaviour
{

    protected Vector2 _direction = new Vector2();

    protected  Rigidbody2D rb;
    protected SurfaceEffector2D surfaceEffector;

    // Start is called before the first frame update
    void Awake()
    {
         rb = GetComponent<Rigidbody2D>();
         surfaceEffector = GetComponent<SurfaceEffector2D>();
    }

}
