using Unity.VisualScripting;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance { get; private set; } //"singleton" makes it a single global instance to access info and modify about its state using BallSpawner.Instance.<properties or methods> from other scripts. Public static makes it easy to access all fields and be reference its contents in many scripts using GameManager.Instance.<fields or methods>
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Rigidbody2D _paddleRB; //used to get paddle position to spawn ball above paddle
    [SerializeField] private Vector2 _spawnPosition; //should be slightly above paddle at time of spawning
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

    private void OnEnable()
    {
        GameEvents.Instance.OnGameStarted += SpawnBall; //spawn ball when game starts
        GameEvents.Instance.OnBallDestroyed += SpawnBall; //subscribe to event and call SpawnBall() when event is fired
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnBallDestroyed -= SpawnBall; //unsubscribe to avoid memory leaks
    }


    /// <summary>
    /// Makes ball spawn above paddle
    /// </summary>
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
