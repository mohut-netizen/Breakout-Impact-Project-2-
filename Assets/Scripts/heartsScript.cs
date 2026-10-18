using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class heartsScript : MonoBehaviour
{
    //GOAL: choose cooresponding image for lives-bar based on # of lives

    private int _lives;

    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;

    [SerializeField]
    private Image[] hearts;

    private void Update()
    {
        _lives = GameManager.Instance.Lives;

        foreach(Image heart in hearts)
        {
            heart.sprite = _emptyHeart;
        }

        for (int i = 0; i < _lives; i++) //counts i till = lives
        {
            hearts[i].sprite = _fullHeart; //sets each heart's sprite 2 full for each life cuz
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