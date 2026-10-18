using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;


    private int _score = 0;

    [SerializeField] private int _lives = 3;
    public int Lives => _lives; //=> is shorthand for {get; private set;}
                               //allows it to be read publically without being set accidentally
    private int _level = 1;

    
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
        SceneManager.LoadScene("level" + _level);
    }

    private void Update()
    {
        if (_lives == 0)
        {
            //end game when lives = 0
            //Debug.Log("Game Over!");
        }

        scoreText.SetText (_score.ToString());
    }

    public void IncreaseScore() //called when onBrickBroken event is fired
    {
        _score++;

        //play sfx
        //add score to UI

        //Debug.Log("Brick Broken: Score is increased!");
    }
    public void DecreaseLives()
    {
        _lives = Mathf.Max(_lives - 1, 0); //makes sure lives aren't negative. Chooses the higher #

        /*lives--;
        if (lives <= 0) { lives = 0; }  */
        

        Debug.Log("Lost a life! Lives left:" + _lives);
    }

    
}
