using UnityEngine;

public class BrickScript : MonoBehaviour
{
    //public Elements element; //assign element as scriptable object element in inspector

    [SerializeField]
    private int _health = 1;

    private void Update()
    {
        //when brick is broken
        if (_health <= 0)
        {
            Destroy(gameObject);
            GameEvents.Instance.BrickBroken(); //fire event, trigger subscribers (increase score)
                                               //in GameManager
            
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _health--;

            SoundManager.Instance.PlaySFX(SoundManager.Instance.brickCollision1);
        }
    }
}
