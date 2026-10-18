using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }

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
    }

    //create event(s)
    public event Action onBrickBroken;
    public void BrickBroken()
    {
        //trigger event only if has listeners/subscribers
            // onBrickBroken?.Invoke(); DOES THE SAME THING. "?" checks if listeners != null
        if (onBrickBroken != null)
        {
            onBrickBroken();
        }
    }

    #region Elemental Reaction System Events
    //create event(s)

    //3x3 explosion (or flower shape). Either break w/1 shot or make any brick 1 shottable
    public event Action onOverloaded;
        
    public void Overloaded()
    {
        onOverloaded?.Invoke(); //REPLACED null check with onOverloaded?.Invoke(); to invoke directly.
                                //"?" already checks for listeners / if listeners = null
    }


    //4 brick-breaking chain (random direction, can pierce thru obstacles)
    public event Action onElectrocharged;
    public void Electrocharged()
    {
        onElectrocharged?.Invoke();
    }

    //breaks hit cube and applies Weakened in 3x3 area. Weakened cubes take only one hit to be broken (or break chains if there)
    public event Action onSuperconducted;
    public void Superconducted()
    {
        onSuperconducted?.Invoke();
    }

    //freeze 3x2. Hitting one later will break all
    public event Action onFrozen;
    public void Frozen()
    {
        onFrozen?.Invoke();
    }

    public event Action onMelted;
    public void Melted()
    {
        onMelted?.Invoke();
    }

    public event Action onVaporized;
    public void Vaporized()
    {
        onVaporized?.Invoke();
    }
    #endregion

}
