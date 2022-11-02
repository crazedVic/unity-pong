using System;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{

    public static Action ComputerScoresHandler;
    public static Action PlayerScoresHandler;
    Rigidbody2D rb;

    [SerializeField]
    TextMeshProUGUI  ballSpeed;

    [SerializeField]
    [Range(1.0f, 20.0f)]
    private float speed = 5f;
    
    [SerializeField]
    [Range(1.0f, 2.0f)]
    private float openingHandicap = 1.6f;
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
        float speed = Vector3.Distance(lastPosition, transform.position) * 100f;
       // float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        ballSpeed.text = rb.velocity.magnitude.ToString();
        lastPosition = transform.position;
    }

    private void FixedUpdate() {
        // keep ball speed constant
         rb.velocity = speed * (rb.velocity.normalized);
    }

    private void launch(){
        //this should be triggered by an invoked action start game button
        // called on game start
        // to set ball moving in a random direction
    
        if(!waitingForFirstCollision) {
            // means game round ended
            speed /= openingHandicap;
            waitingForFirstCollision = true;
        }

        float x = UnityEngine.Random.value > 0.5f ? -1.0f : 1.0f;
        float y = UnityEngine.Random.value < 0.5f ? UnityEngine.Random.Range(-1f, -0.5f) : UnityEngine.Random.Range(0.5f, 1.0f);
        rb.velocity = new Vector2(x,y)*(speed/openingHandicap);

    }

    private void OnCollisionEnter2D(Collision2D collided) {
        // first collision will double speed
        if(waitingForFirstCollision && 
            (collided.gameObject.name == "Computer" ||
             collided.gameObject.name == "Player")){
            speed = speed * openingHandicap;
            waitingForFirstCollision = false;
        }
            
       if(collided.gameObject.name =="Computer"){
            PlayerScoresHandler.Invoke();
       }
       if(collided.gameObject.name == "Player"){
            ComputerScoresHandler.Invoke();
       }
    }

}
