using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public struct OnGameOverInfo
    {
        private string playerName;

        public OnGameOverInfo(string playerName)
        {
            this.playerName = playerName;
        }
    }
    
    public static readonly UnityEvent<OnGameOverInfo> OnGameOver = new UnityEvent<OnGameOverInfo>();
    public static readonly UnityEvent OnGameStart = new UnityEvent();
}