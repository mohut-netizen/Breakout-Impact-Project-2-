using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _ballRB;
    [SerializeField] private Rigidbody2D _paddleRB; //used to get paddle position to hover ball over paddle when in spawn
    [SerializeField] private const float MAX_LAUNCH_ANGLE = 45f; //"f" ensure a floating decimal is assignment, and 45 is included. Otherwise, int are used
    [SerializeField] private const float MAX_BOUNCE_ANGLE = 75f;
    [SerializeField] private float _launchAngle;
    [SerializeField] private float _speed = 12;
    [SerializeField] private bool _inSpawn = true; //true when instantiated, changed when launched
    [SerializeField] private float _moveVelocity;


    private void Awake()
    {
        //ElementManager = GetComponent<ElementManager>();
        _paddleRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _ballRB = GetComponent<Rigidbody2D>(); //reference the component <T> that is attached to this game object

    }


    void Update()
    {
        if (_inSpawn == true && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
    }

    /// <summary>
    /// FixedUpdate updates at a constant rate of 50. Good for physics stuff (independent of FPS???)
    /// </summary>
    private void FixedUpdate()
    {
        if (_inSpawn)
        {
            HoverOverPaddle();
        }
    }

    /// <summary>
    /// make ball hover over paddle, such as when preparing to launch (still in spawn)
    /// </summary>
    private void HoverOverPaddle()
    {
        _ballRB.position = _paddleRB.position + new Vector2(0, 2); //make the ball's position slightly above the paddle

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

    /// <summary>
    /// Bounce ball off of paddle dynamically based on collision point
    /// </summary>
    /// <param name="collision"></param>
    private void BounceOffPaddle(Collision2D collision)
    {
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


        //TODO NOT ENOUGH to change rotation. "Bounce" material just launches in opp direction
        //Need to also change movement to match direction (velocity)
        //ball.transform.rotation = Quaternion.Euler(0, 0, bounceAngle);        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //paddle bounce collision
        if (collision.gameObject.CompareTag("Player"))
        {
            BounceOffPaddle(collision);
        }
        {
            
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

    }

    ///destroy ball when crossing deadzone
    ///Fire BallDestroyed event to notify GameManager to decrease lives and spawn new ball
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadzone"))
        {
            Destroy(gameObject);
            Debug.Log("Ball passed deadzone. Destroyed ball");
            GameEvents.Instance.TriggerBallDestroyed(); //fire event to notify other scripts decrease lives and spawn new ball while avoiding dependencies
        }
    }

    //NOTE: this was an attempt to manually make the ball bounce instead of relying on Unity physics
    //CAN JUST ADD PHYSICS MATERIAL & BOUNCE = 1 TO BOUNCE
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
}

