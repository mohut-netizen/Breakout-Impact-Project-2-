using System;
using UnityEngine;

/// <summary>
/// Breakcasts when certain things happen in the game by firing an event.
/// Doesn't know or care about who listens.
/// 
/// Other scripts can access these game events and fire them off via the singleton (GameEvents.Instance.<TriggerEvent()>)
/// </summary>
public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
        
    }
    
    //Setup the Events (the radio)
    public event Action OnBrickBroken; //null when has no listeners
    public event Action OnBallDestroyed;
    public event Action OnLivesDecreased;
    public event Action OnScoreIncreased;

    //Set up Signal Broadcasters (public methods to fire the events in any scripts). Works ONLY if has listeners/subscribers meaning OnBrickBroken != null, using null operator "?")
    public void TriggerBrickBroken() => OnBrickBroken?.Invoke(); //when called, fire off the event
    public void TriggerBallDestroyed() => OnBallDestroyed?.Invoke();
    public void TriggerLivesDecreased() => OnLivesDecreased?.Invoke();
    public void TriggerScoreIncreased() => OnScoreIncreased?.Invoke();

    //Game State Events
    public event Action OnGameStarted;
    public event Action OnGameEnded;
    public event Action OnGamePaused;
    public event Action OnGameResumed;
    public event Action OnGameRestarted;
    public void TriggerGameStarted() => OnGameEnded?.Invoke(); //use to set up hide game starting UI and allow player to start moving/ball to spawn
    public void TriggerGameEnded() => OnGameEnded?.Invoke(); // lambda => is easy way to write one line function like "{ OnGameEnded?.Invoke(); }"
    public void TriggerGamePaused() => OnGamePaused?.Invoke();
    public void TriggerGameResumed() => OnGameResumed?.Invoke();
    public void TriggerGameRestarted() => OnGameRestarted?.Invoke();




    #region Elemental Reaction System Events
    /*
    //create event(s)
    public event Action OnMelted;
    public event Action OnVaporized;
    public event Action OnFrozen;
    public event Action OnSuperconducted;
    public event Action OnElectrocharged;
    public event Action OnOverloaded;

    //create event invokers/callers/triggers
    public void TriggerOverloaded() => OnOverloaded?.Invoke(); //3x3 explosion (or flower shape). Either break w/1 shot or make any brick 1 shottable
    public void TriggerElectrocharged() => OnElectrocharged?.Invoke(); //4 brick-breaking chain (random direction, can pierce thru obstacles)
    public void TriggerSuperconducted() => OnSuperconducted?.Invoke(); //breaks hit cube and applies Weakened in 3x3 area. Weakened cubes take only one hit to be broken (or break chains if there)
    public void TriggerFrozen() => OnFrozen?.Invoke();    // freeze 3x2. Hitting one later will break all
    public void TriggerMelted() => OnMelted?.Invoke();
    public void TriggerVaporized() => OnVaporized?.Invoke();
    */
    #endregion
}
