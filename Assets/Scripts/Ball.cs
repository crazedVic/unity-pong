using System;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{

    public static Action ComputerScoresHandler;
    public static Action PlayerScoresHandler;
    Rigidbody2D rb;

    [SerializeField]
    TextMeshProUGUI  ballSpeedLabel;

    [SerializeField]
    [Range(1.0f, 20.0f)]
    private float baseSpeed = 5f;
    private float currentSpeed;

    [SerializeField]
    [Range(15.0f, 30.0f)]
    private float maxSpeed = 17.0f;

    [SerializeField]
    [Range(1.0f, 2.0f)]
    private float speedMultiplier = 1.1f;
    private bool waitingForFirstCollision = true;

    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.NewRoundHandler += launch;
    }

    private void Start(){
        launch();
        lastPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        ballSpeedLabel.text = rb.velocity.magnitude.ToString();
        lastPosition = transform.position;
    }

    private void FixedUpdate() {
        // keep ball speed constant
         rb.velocity = currentSpeed * (rb.velocity.normalized);
    }

    private void launch(){
        //this should be triggered by an invoked action start game button
        // called on game start
        // to set ball moving in a random direction
    
        currentSpeed = baseSpeed;

        float x = UnityEngine.Random.value > 0.5f ? -1.0f : 1.0f;
        float y = UnityEngine.Random.value < 0.5f ? UnityEngine.Random.Range(-1f, -0.5f) : UnityEngine.Random.Range(0.5f, 1.0f);
        rb.velocity = new Vector2(x,y)*currentSpeed;

    }

    private void OnCollisionEnter2D(Collision2D collided) {
        // first collision will double speed
        if(
            collided.gameObject.name == "Player Paddle"){
            currentSpeed = 
                Mathf.Clamp(currentSpeed * speedMultiplier, baseSpeed, maxSpeed);
        }
            
       if(collided.gameObject.name =="Computer"){
            PlayerScoresHandler.Invoke();
       }
       if(collided.gameObject.name == "Player"){
            ComputerScoresHandler.Invoke();
       }
    }

}
