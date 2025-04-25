using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using MiniScript.MSGS.Time;

public class TimeKeeperTestScript : MonoBehaviour
{
    void Start()
    {
        TimeKeeper.Initialize();

        TimeKeeper.SetTickDuration(2f);

        TimeKeeper.tickEvent.AddListener(new UnityEngine.Events.UnityAction(handleTickEvent));
    }
        
    void Update()
    {
        
    }

    void handleTickEvent()
    {
        Debug.Log("tick" + TimeKeeper.tickCounter);
    }

    [Button]
    void StartClock() { TimeKeeper.Start(); }
    
    [Button]
    void StopClock() { TimeKeeper.Stop(); }

    [Button]
    void PauseClock() { TimeKeeper.Pause(); }

    [Button]
    void ResumeClock() { TimeKeeper.Resume(); }
}
