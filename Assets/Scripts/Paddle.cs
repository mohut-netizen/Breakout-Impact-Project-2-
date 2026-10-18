using UnityEngine;

public class Paddle : MonoBehaviour
{
    //references
    //public Elements Element;
    [SerializeField] private Rigidbody2D _paddleRB;
    [SerializeField] private float _moveSpeed = 10f; //just a multiplier to the movement
    [SerializeField] private float _moveVelocity; //actual movement; spd * direction

    private void Start()
    {
        _paddleRB = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        MovePlayer(); //not in FixedUpdate() because movement is better if updated frequently. Not like object physics
    }

    /// <summary>
    /// Move player based on user's horizontal input
    /// </summary>
    private void MovePlayer()
    {
        float moveDirection = Input.GetAxis("Horizontal"); //set X= +1 or -1 based on direction
        _moveVelocity = moveDirection * _moveSpeed;                         
        _paddleRB.linearVelocityX = _moveVelocity;
        
        //paddle_rb.MovePosition(paddle_rb.position + moveVelocity * Time.deltaTime); //adds displacement to current position
    }

    /// <summary>
    /// Do something on collision with other objects
    /// </summary>
    /// <param name="collision"></param> thing the paddle collided with
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.paddleCollision); //play appropriate sound
    }

}
