using System;
using Unity.VisualScripting;
using UnityEngine;

//Any object with Elements will have ElementManager to avoid repeating element related code
//in every individual script (like adding "public Elements element;" in every single one
public class ElementManager : MonoBehaviour
{
    [SerializeField] private Sprite[] elementSprites;
    [SerializeField] public SpriteRenderer refSprite; //sprite is NOT a component. Its part of sprite renderer


    public Elements ElementRef;

    private void Awake()
    {
        refSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //SetSprite() depends on elementRef being !null, so check for null b4 running

        if (ElementRef != null)
        {
            SetSprite();
        }
        else
        {
            Debug.LogWarning("elementRef is not assigned for " + gameObject.name);
        }
    }



    private void SetSprite()
    {
        //assign ball sprite based on element

        int index = ElementRef.elementType switch //switch expression shortens swith statment. Assigns directly
        {
            ElementType.Pyro => 1,
            ElementType.Hydro => 2,
            ElementType.Electro => 3,
            ElementType.Cryo => 4,
            _ => 0 //default case
        };

        //exit method if assigned value is higher than array index. Keeps sprite as is
        if (elementSprites.Length - 1 < index) //-1 because length can be 2, but indexes are 0, 1
        {
            //Debug.Log("ElementSprites is shorten than picked index!");
            return;
        }

        //assign sprite
        refSprite.sprite = elementSprites[index];
    }


    
}
