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
/// <summary>
/// Contains information about the game that would need to be referenced by many different scripts.
/// Is a singleton to serve as a single hub to the game's shared state.
/// Listens to events fired by the GameEvents radio and performs some functionality.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } //make manager a SINGLETON (only 1 instance can exist). Public static makes it easy to access all fields and be reference its contents in many scripts using GameManager.Instance.<properties or methods>
    public const int MAX_LIVES = 3; //public constant to be referenced by other scripts (like HealthUI) to set the size of the hearts array and prevent out of bounds errors
    private const int STARTING_LIVES = MAX_LIVES;
    [SerializeField] private int _lives = STARTING_LIVES;
    [SerializeField] private int _score = 0;
    [SerializeField] private Levels _currentLevel = 0; //TODO set this manually in editor per stage

    //Read-only public fields serving as getters for the private fields so they can be accesses by other scripts using GameManager.Instance.Field.
    public int Lives => _lives; // "=> <field>" returns field (lambda calling the getter)
    public int Score => _score;

    private void Awake()
    {
        if (Instance != null && Instance != this) //check if an Instance already exists and is not equal to this object. If so, a duplicate is found
        {
            Destroy(this.gameObject); //Clears duplicate managers that arent the original one. If didnt add "&& Instance != this", og instance would be destroyed too
            return; //to avoid reassigning Instance when loading scene (keeps the og)
        }

        //code is only reached for the first instance since Instance will be null
        Instance = this; 
        DontDestroyOnLoad(this.gameObject); //makes the first instance persist across scenes
    }
   
    //One-time game initialization after initial setup in Awake()
    private void Start()
    {
        SceneManager.LoadScene("level" + _currentLevel);
        GameEvents.Instance.TriggerGameStarted(); //TODO: temporary. trigger game started only after start timer passed
    }

    #region OnEnable and OnDisable Loading Code
    /* Explanation for OnEnable and OnDisable:
     *
     * Run every time script or game object or component is toggled ON/OFF in the unity inspector.
     * Disabling an object subscpends it, (like unchecking the "show" box in the inspector to hide a UI object) not destroy it from memory, prevent it using resources and loading
     * 
     * NOTE: Great for resetting temporary state, like event subscription
     * NOTE: SetActive(true/false) is great for hiding/showing UI elements quickly instead of destroying the UI and recreating it everytime user pauses
     */
    ///Runs every time the script/object is activated/turned on
    ///Subscribes <method> to event and listens. Calls method() when event is fired
    private void OnEnable()
    {
        GameEvents.Instance.OnBrickBroken += IncreaseScore;
        GameEvents.Instance.OnGameEnded += EndGame;
        GameEvents.Instance.OnBallDestroyed += DecreaseLives;
    }

    ///Runs every time script is disabled(like unchecking the "show" box in the inspector to hide or show a UI object and prevent it from running code)
    ///Unsubscribe from events every time object is disabled to avoid memory leaks 
    private void OnDisable()
    {
        GameEvents.Instance.OnBrickBroken -= IncreaseScore;
        GameEvents.Instance.OnGameEnded -= EndGame;
        GameEvents.Instance.OnBallDestroyed -= DecreaseLives;
    }
    #endregion


    //REVIEW how to increase score and end game when certain events are fired
    /// <summary>
    /// NOTE triggered when OnGameEnded is fired
    /// </summary>
    public void EndGame()
    {
        //TODO toggle UI for gameover
        Debug.Log("Game Over!");
    }


    /// <summary>
    /// Increase score when certain conditions are met, like when certain events (eg. onBrickBroken) are fired.
    /// Functions sorta like a public Set() to externally modify private state 
    /// </summary>
    public void IncreaseScore() 
    {
        _score++;

        //TODO play sfx
        //TODO add score to UI

        GameEvents.Instance.TriggerScoreIncreased(); //notify UI to update score display when score is increased

        Debug.Log("Score is increased!");
    }
    
    public void DecreaseLives()
    {
        _lives = Mathf.Max(_lives - 1, 0); //return either 0 or lives-1, whatever is bigger ("Clamps" lives at 0 make sure lives isn't <0. )
        Debug.Log("Lost a life! Lives left: " + _lives);

        if (_lives == 0)
        {
            GameEvents.Instance.TriggerGameEnded();
        }
    }
}
