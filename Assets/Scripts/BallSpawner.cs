using Unity.VisualScripting;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance { get; private set; } //"singleton" make it a single global instance
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Rigidbody2D _paddleRB; //used to get paddle position to spawn ball above paddle
    [SerializeField] private Vector2 _spawnPosition; 
    [SerializeField] private Vector2 _spawnVelocity;

    private void Awake()
    {
        _paddleRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _spawnPosition = _paddleRB.position + new Vector2(0, 1); //make ball spawn slightly above paddle

        //Singleton instantiation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Destroyed Extra Spawner");
            return; //to avoid going further and reassigning Instance
        }

        Instance = this; //reached only when once Instance is null
    }

    private void Start()
    {
        SpawnBall();

    }

    // TODO makes ball spawn !!!ABOVE paddle!!!
    public void SpawnBall()
    {
        //make sure ballPrefab exists before instantiating a null object
        if(_ballPrefab == null){
            Debug.Log("ball prefab is NULL");
            return;
        }

        Instantiate(_ballPrefab, _spawnPosition, Quaternion.identity);
        Debug.Log("BALL SPAWNED");

    }
}
