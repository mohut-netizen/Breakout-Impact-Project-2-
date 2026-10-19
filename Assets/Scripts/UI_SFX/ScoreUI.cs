using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour, IUserInterface
{
    [SerializeField] private TextMeshProUGUI scoreText;
    //private int _currentScore = GameManager.Instance.Score; //references the starting value of lives in GameManager to set the UI to the correct value at start and compare HP changes.

    //REVIEW consider moving OnGameStarted += InitializeUI to IUserInterface since it seems it wants to be repeted 
    private void OnEnable()
    {
        GameEvents.Instance.OnGameStarted += InitializeUI; //initialize UI to current value when game starts
        GameEvents.Instance.OnScoreIncreased += UpdateUI; //update score UI only when score is increased to avoid updating every frame
    }

    void OnDisable()
    {
        GameEvents.Instance.OnGameStarted -= InitializeUI; //initialize UI to current value when game starts
        GameEvents.Instance.OnScoreIncreased -= UpdateUI;
    }

    public void InitializeUI()
    {
        UpdateUI(); //set score UI to current score when game starts
    }

    /// <summary>
    /// Sets the ScoreUI to the current score
    /// </summary>
    public void UpdateUI()
    {
        //REVIEW consider storing an updated score variable later if decided to have different UI effects based on if score increased or decreased like HealthUI (if applicable to project)
        scoreText.SetText(GameManager.Instance.Score.ToString()); //set text UI if 
    }
}
