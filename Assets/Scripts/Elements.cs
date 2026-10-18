using Unity.VisualScripting;
using UnityEngine;
//NO SINGLETON BECAUSE MULTI INSTANCES

public enum ElementType { NoElement, Pyro, Hydro, Electro, Cryo} //enum to organize + readability

[CreateAssetMenu(fileName = "Elements", menuName = "Elements")]
public class Elements : ScriptableObject
{
    public ElementType elementType;

    public void CheckReaction(Elements element1, Elements element2)
    {
        //check elements involving in reaction
        if (element1.elementType == ElementType.Electro && element2.elementType == ElementType.Pyro
            || element1.elementType == ElementType.Pyro && element2.elementType == ElementType.Electro) //checks tag of *attached object
        {
            //Debug.Log("OVERLOAD?!?!?");
            GameEvents.Instance.Overloaded();
        }
    }

//DONT WORK INSIDE ELEMENTS, BECAUSE START() ISNT CALLED IN SCRIPTABLE OBJECTS
    //reactions
    //public void Overload()
    //{
    //    Debug.Log("OVERLOADED is triggered!");
    //}

}
