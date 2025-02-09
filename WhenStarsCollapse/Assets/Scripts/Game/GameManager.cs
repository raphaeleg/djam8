using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Updates the Scores and checks whether the lose condition is met.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] Score score;
    [SerializeField] const int BLACKHOLE_END_CONDITION = 5;
    [SerializeField] const int START_CHAOTIC_GAME_INTENSITY = 3;
    #region EventManager
    private Dictionary<string, Action<int>> SubscribedEvents;

    private void Awake()
    {
        SubscribedEvents = new() {
            { "isSuccessfulSpawn", Event_AddCount_Star },
            { "blackHoleSpawn", Event_AddCount_BlackHole },
            { "whiteDwarfSpawn", Event_AddCount_WhiteDwarf },
            { "TimerText", Event_AddCount_Timer },
        };
    }
    private void OnEnable()
    {
        foreach (var pair in SubscribedEvents)
        {
            EventManager.StartListening(pair.Key, pair.Value);
        }
    }

    private void OnDisable()
    {
        foreach (var pair in SubscribedEvents)
        {
            EventManager.StopListening(pair.Key, pair.Value);
        }
    }
    #endregion

    public void Start() 
    { 
        score.Restart(); 
    }

    public void Event_AddCount_Star(int val)
    {
        score.stars++;
        EventManager.TriggerEvent("StarsText", score.stars);
    }
    public void Event_AddCount_WhiteDwarf(int val) 
    { 
        score.whiteDwarfs++; 
    }
    public void Event_AddCount_BlackHole(int val)
    {
        score.blackHoles++;
        EventManager.TriggerEvent("BlackHoleText", score.blackHoles);
        if (score.blackHoles >= BLACKHOLE_END_CONDITION) 
        { 
            EventManager.TriggerEvent("Lose", (int)Audio_MusicArea.LOST); 
        } 
        else if (score.blackHoles == (int)BLACKHOLE_END_CONDITION/2) 
        { 
            EventManager.TriggerEvent("ChangeMusicArea", (int)Audio_MusicArea.CHAOTIC); 
        }
    }
    public void Event_AddCount_Timer(int val) 
    {
        if (val % 10 is 0 && val is not 0) 
        {
            int currentDifficulty = val / 10;
            if (currentDifficulty is START_CHAOTIC_GAME_INTENSITY) 
            { 
                EventManager.TriggerEvent("ChangeMusicArea", (int)Audio_MusicArea.CHAOTIC); 
            }
            EventManager.TriggerEvent("DifficultyIncrease", currentDifficulty); 
        }
        score.time = val; 
    }
}
