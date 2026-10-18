using Unity.VisualScripting;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance { get; private set; } //"singleton" makes publically accessible
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Vector3 _spawnPosition = new(0, -7.2f, 0);
    

    //private Vector2 spawnVelocity = new Vector2(0, 0);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("Spawner Instance Set");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Destroyed Extra Spawner");
        }
    }

    private void Start()
    {
        SpawnBall();
    }


    public void SpawnBall()
    {
            Instantiate(_ballPrefab, _spawnPosition, Quaternion.identity);
            //transform.position = spawnPosition;
            //inSpawn = true;
        
    }
}
