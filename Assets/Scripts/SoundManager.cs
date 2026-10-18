using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; private set;}

    //Reference audio component
    [Header("--- Audio Source ---")]
    public AudioSource SFXSource;

    //Audio clips (assign in editor)
    [Header("--- Audio Clip ---")]
    public AudioClip brickCollision1;
    public AudioClip paddleCollision;

    #region Singleton stuff
    private void Awake()
    {
        if (Instance == null) Instance = this;  else  Destroy(Instance);
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null; //unassign Instance to avoid empty/"zombie" reference
    }
    #endregion

    public void PlaySFX(AudioClip clip) //set parameter as clip to play
    {
        SFXSource.PlayOneShot(clip);
    }

}
