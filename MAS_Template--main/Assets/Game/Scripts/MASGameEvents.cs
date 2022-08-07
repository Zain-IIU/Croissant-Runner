using System.Collections;
using System.Collections.Generic;
using ElephantSDK;
using Facebook.Unity;
using GameAnalyticsSDK;
using UnityEngine;
using MoreMountains.NiceVibrations;


public class MASGameEvents : MonoSingleton<MASGameEvents>
{
    public enum LevelEvents
    {
        LevelStarted,
        LevelFailed,
        LevelCompleted
    }
    void Start()
    {
        GameAnalytics.Initialize();
        FB.Init(OnFBInitComplete);
        MMNViOS.iOSInitializeHaptics();


    }


    public void Haptic(HapticTypes type)
    {
        MMVibrationManager.Haptic(type, false,true, this);
    }

    private void OnFBInitComplete()
    {
        Debug.Log("HappySDK: Facebook Initialize Complete!");
    }


    public void LevelEvent(LevelEvents type, int levelNumber)
    {
        LevelEvent_Elephant(type, levelNumber);
        LevelEvent_GameAnalytics(type, levelNumber);
    }


    void LevelEvent_Elephant(LevelEvents type, int levelNumber)
    {
        switch (type)
        {
            case LevelEvents.LevelStarted:
                Elephant.LevelStarted(levelNumber);
                break;
            case LevelEvents.LevelFailed:
                Elephant.LevelFailed(levelNumber);
                break;
            case LevelEvents.LevelCompleted:
                Elephant.LevelCompleted(levelNumber);
                break;
            default:
                break;
        }

        // Debug.Log($"LevelEvent_Elephant: {type} Level: {levelNumber}");
    }




    void LevelEvent_GameAnalytics(LevelEvents type, int levelNumber)
    {
        switch (type)
        {
            case LevelEvents.LevelStarted:
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level_" + levelNumber);
                break;
            case LevelEvents.LevelFailed:
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level_" + levelNumber);
                break;
            case LevelEvents.LevelCompleted:
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level_" + levelNumber);
                break;
            default:
                break;
        }

        // Debug.Log($"LevelEvent_GameAnalytics: {type} Level: {levelNumber}");
    }
}