using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//GOAL: choose cooresponding image for lives-bar based on # of lives
public class HealthUI : MonoBehaviour, IUserInterface
{
    private int _currentHP = GameManager.Instance.Lives; //references the starting value of lives in GameManager to set the UI to the correct value at start and compare HP changes.
                                                        //NOTE: this is a value variable, so _hp will not update when the value of lives in GameManager changes. Must change manually
    [SerializeField] private Sprite _fullHeartIMG;
    [SerializeField] private Sprite _emptyHeartIMG;
    [SerializeField] private Image[] _hearts = new Image[GameManager.Instance.MAX_LIVES];

    private void OnEnable()
    {
        GameEvents.Instance.OnGameStarted += InitializeUI; //initialize UI to current value when game starts
    }

    void OnDisable()
    {
        GameEvents.Instance.OnGameStarted -= InitializeUI;
    }


    public void InitilizeUI()
    {
        //set heart image placeholders
        foreach (Image heart in _hearts)
        {
            heart.sprite = _emptyHeartIMG;
        }

        //fill hearts based on starting number of lives 
        for (int i = 0; i < _currentHP; i++)
        {
            _hearts[i].sprite = _fullHeartIMG;
        }
    }
    /// <summary>
    /// when a relevant event is fired (like OnLivesChanged), update the UI to reflect the current value of the variable it is displaying
    /// dont worry about having to update multiple hearts at once, because every single time lives change, a new
    /// TriggerLivesChange() will be invoked and UpdatesUI per each instance of lives changing
    /// </summary>
    public void UpdateUI()
    {
        //if lost a life, update the heart that corresponds to the lost life to empty.
        if (GameManager.Instance.Lives < _currentHP)
        {
            _hearts[_currentHP].sprite = _emptyHeartIMG; //set the heart that corresponds to the lost life to empty
        }
        else //If gained a life, update the heart that corresponds to the gained life to full (NOTE: dont worry about going out of bounds because Lives will not go above the max number of lives is 3 and the array has 3 elements)
        {
            _hearts[GameManager.Instance.Lives].sprite = _fullHeartIMG;
        }

        _currentHP = GameManager.Instance.Lives; //updates the local variable to the current value of lives in GameManager

    }
}

/*private RawImage currentImage;

    private int lives;


    private void Awake()
    {
        currentImage = GetComponent<RawImage>();

    }
    private void Update()
    {
        lives = GameManager.Instance.lives;
        Debug.Log(lives);
        //currentImage = Texture[lives]; 

        switch (lives)
        {
            case 3:
                currentImage.texture = livesImages[0].texture;
                Debug.Log("set health image to 3 HEARTS");
                break;
            case 2:
                currentImage.texture = livesImages[1].texture;
                Debug.Log("set health image to 2 HEARTS");
                break;
            case 1:
                currentImage.texture = livesImages[2].texture;
                Debug.Log("set health image to 1 HEARTS");
                break;
            case 0:
                currentImage.texture = livesImages[3].texture;
                Debug.Log("set health image to 0 HEARTS");
                break;
        }
    }

    //visible database
    [System.Serializable]
    public class LiveCounters
    {
        public string imageName; //names the item in list. Should be chosen based on lives
        public Texture texture;
    }

    public List<LiveCounters> livesImages; //creates the list
*/