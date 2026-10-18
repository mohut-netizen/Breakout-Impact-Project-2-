using UnityEngine;

public class paddleScript : MonoBehaviour
{
    //references
    public Elements Element;

    private Rigidbody2D _paddleRB;

    [SerializeField] private float _moveSpeed = 10f; //just a multiplier to the movement
    private float _moveVelocity; //actual movement

    private void Start()
    {
        _paddleRB = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        MovePlayer(); //not in FixedUpdate() because movement is better if updated frequently. Not like object physics
    }

    private void MovePlayer()
    {
        float moveDirec = Input.GetAxis("Horizontal"); //checks if player is moving right
        _moveVelocity = moveDirec * _moveSpeed;                           //or left, then sets X to +1 or -1

        _paddleRB.linearVelocityX = _moveVelocity;
        
        //paddle_rb.MovePosition(paddle_rb.position + moveVelocity * Time.deltaTime); //adds displacement to current position
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.paddleCollision);
    }

}
