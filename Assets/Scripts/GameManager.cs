using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum Levels
{
    Level1 = 1,
    Level2 = 2,
    Level3 = 3,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private const int STARTING_LIVES = 3;
    [SerializeField] private int _score = 0;
    [SerializeField] private int _lives = STARTING_LIVES;
    [SerializeField] private Levels _currentLevel; //TODO set this manually in editor per stage

    public int Lives => _lives; //creates a public getter for _lives. => is shorthand for {get; private set;} to avoid setting accidentally


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        DontDestroyOnLoad(this.gameObject);
    }
   

    private void Start()
    {
        _score = 0;
        _lives = 3;

        GameEvents.Instance.onBrickBroken += IncreaseScore; //subscribes to event. Triggers when
                                                            //event is fired
        SceneManager.LoadScene("level" + _currentLevel);
    }

    private void Update()
    {
        if (_lives == 0)
        {
            //TODO end game when lives = 0
            //Debug.Log("Game Over!");
        }

        scoreText.SetText (_score.ToString()); //set text UI
    }

    /// <summary>
    /// Increase score when certain conditions are met, like when certain events (eg. onBrickBroken) are fired
    /// </summary>
    public void IncreaseScore() 
    {
        _score++;

        //TODO play sfx
        //TODO add score to UI

        Debug.Log("Score is increased!");
    }
    public void DecreaseLives()
    {
        _lives = Mathf.Max(_lives - 1, 0); //decrease lives, then return the bigger of the two args (0 or lives, to make sure lives isn't <0. "Clamps" it at 0)
        Debug.Log("Lost a life! Lives left:" + _lives);
    }

    
}
