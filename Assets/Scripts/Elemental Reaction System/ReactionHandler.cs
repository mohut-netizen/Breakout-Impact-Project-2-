using UnityEngine;

public class ReactionHandler : MonoBehaviour
{
    public int AreaOfEffect;
    public int Damage;

    private void Awake()
    {
        //subscribe events
        GameEvents.Instance.onOverloaded += Overload; //calls Overload() when onOverloaded() event is fired
    }

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }


    public void Overload()
    {
        Debug.Log("OVERLOADED is triggered!");

        Damage = 2;
        AreaOfEffect = 3;

        transform.GetChild(0).gameObject.SetActive(true);
    }
}