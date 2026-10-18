using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    //public ElementManager ElementManager;
    [SerializeField] private Rigidbody2D _ballRB;
    [SerializeField] private Rigidbody2D _paddleRB;
    [SerializeField] private const float MAX_LAUNCH_ANGLE = 45f; //"f" ensure a floating decimal is assignment, and 45 is included. Otherwise, int are used
    [SerializeField] private const float MAX_BOUNCE_ANGLE = 75f;
    [SerializeField] private float _launchAngle;
    [SerializeField] private float _speed = 12;
    [SerializeField] private bool _inSpawn;


    private void Awake()
    {
        //ElementManager = GetComponent<ElementManager>();
        _paddleRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _ballRB = GetComponent<Rigidbody2D>();
        _inSpawn = true;
        
        //makes ball spawn above paddle
        _ballRB.transform.position = new Vector2(_paddleRB.transform.position.x, transform.position.y);
    }

    void Update()
    {
        if (_inSpawn == true && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
    }

    private void FixedUpdate()
    {
        //make ball follow paddle when player moves
        if (_inSpawn)
        {
            _ballRB.linearVelocityX = _paddleRB.linearVelocityX;
        }
    }

    private void LaunchBall()
    {
        SetLaunchAngle();
        _ballRB.linearVelocity = transform.up * _speed;
        _inSpawn = false;
    }
    private void SetLaunchAngle()
    {
        _launchAngle = UnityEngine.Random.Range(-MAX_LAUNCH_ANGLE, MAX_LAUNCH_ANGLE);
        _ballRB.transform.rotation = Quaternion.Euler(0, 0, _launchAngle); //cannot assign rotation directly, must use Quaternion
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //paddle bounce collision
        if (collision.gameObject.CompareTag("Player"))
        {
            #region bounce ball off of paddle dynamically based on collision point

            //references
            Rigidbody2D ball = _ballRB;
            Collider2D paddle = collision.collider; //not BoxCollider2D because arg passed in as "(*Collision2D* collision)"


            //collision information
            Vector2 contactDistance = paddle.bounds.center - ball.transform.position; //e.g. if ball.x = 10 and paddle.center's x = 10, then distance = 0
            Vector2 ballDirection = ball.linearVelocity.normalized; //gets just the direction, no spd. Velocity = direction * speed


            float paddleHalfWidth = paddle.bounds.size.x / 2; //from center to edge. Enables to normalize bounceAngle to be -1 to 1 instead of -0.5 to 0.5

            //rotate ball based on angle
            float bounceAngle = (contactDistance.x / paddleHalfWidth) * MAX_BOUNCE_ANGLE; //contactDistance will always be capped by paddle size. If width = 50 & x = 0, if you hit the edge.x (-25), then you hit max halfWidth (25). -25/25 = -1
            ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection; //rotates around ball *current rotation* (& axis and NOT world's xyz)


            //apply new direction to ball
            ball.linearVelocity = ballDirection * ball.linearVelocity.magnitude; //mag = speed


            //NOT ENOUGH to change rotation. "Bounce" material just launches in opp direction
            //Need to also change movement to match direction (velocity)
            //ball.transform.rotation = Quaternion.Euler(0, 0, bounceAngle);
            #endregion
        }


        //element collision stuff
        #region elemental collision detector
        /*
        ElementManager otherElementManager = collision.collider.GetComponent<ElementManager>();

        if (otherElementManager != null || otherElementManager.ElementRef.elementType != ElementType.NoElement)
        {

            Elements thisElement = this.ElementManager.ElementRef; //ref EM so ball element is synced
            Elements otherElement = otherElementManager.ElementRef;

            Debug.Log($"Elemental Collision occurred! Elements involved: {thisElement} [ {this.name} ] + " +
                $"{otherElement} [ {collision.collider.name} ]");

            ElementManager.ElementRef.CheckReaction(thisElement, otherElement);

        }
        //else
        //{
        //    Debug.LogWarning($"No ElementManager found on the other collider ({collision.collider.name})");
        //}
        */
        #endregion
    }



    //destroy ball when crossing deadzone
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.CompareTag("Deadzone"))
        {
            Destroy(gameObject);
            

            GameManager.Instance.DecreaseLives();

            BallSpawner.Instance.SpawnBall();
            
        }
    }

}

//JUST ADD PHYSICS MATERIAL + BOUNCE = 1 TO BOUNCE
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
    Debug.Log("Ball collided with object!");
    //flips ball direction upon collisions
    //ballRB.transform.rotation = Quaternion.Euler(0, 0, -launchAngle);

    Vector2 normal = collision.GetContact(0).normal; //normal is perpendicular from hit surface
    Vector2 reflectedVelocity = Vector2.Reflect(ballRB.linearVelocity.normalized, normal);

    ballRB.linearVelocity = reflectedVelocity * speed;
    }
    */