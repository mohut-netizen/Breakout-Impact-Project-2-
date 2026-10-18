using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//GOAL: choose cooresponding image for lives-bar based on # of lives
public class HealthUI : MonoBehaviour
{
    private int _lives = GameManager.Instance.Lives; //REVIEW to make sure updates as lives change
    [SerializeField] private Sprite _fullHeartIMG;
    [SerializeField] private Sprite _emptyHeartIMG;
    [SerializeField] private Image[] _hearts;

    private void Update()
    {
        //TODO initialize hearth UI only once at beginning of program
        //set heart image placeholders
        foreach(Image heart in _hearts)
        {
            heart.sprite = _emptyHeartIMG;
        }

        //TODO change so sprites are only updated when lives change
        //fill hearts based on number of lives 
        for (int i = 0; i < _lives; i++){
            _hearts[i].sprite = _fullHeartIMG;
        }

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