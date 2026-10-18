using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private ElementType element = ElementType.NoElement; //assign element as scriptable object element in inspector
    [SerializeField] private const int STARTING_HP = 1; //TODO temporary: set manually in inspector for each brick type
    [SerializeField] private int _health = STARTING_HP;

    private void Awake()
    {
        element = GetComponent<ElementType>();
    }

    private void Update()
    {
        //when brick is broken
        if (_health <= 0)
        {
            Destroy(gameObject);
            GameEvents.Instance.BrickBroken(); //fire event, trigger subscribers (increase score)

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
