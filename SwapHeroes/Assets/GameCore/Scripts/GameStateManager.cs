using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public static System.Action GameStateChanged;
    public static GameState CurrentState
    {
        get => instance._currentState;
        set
        {
            if (instance._currentState != value)
            {
                instance._lastState = instance._currentState;
                instance._currentState = value;
                if (GameStateChanged != null)
                    GameStateChanged.Invoke();
            }
        }
    }
    public static GameState LastState
    {
        get => instance._lastState;
        set => instance._lastState = value;
    }

    [SerializeField] GameState _currentState = GameState.None;
    [SerializeField] GameState _lastState = GameState.None;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CurrentState = GameState.Idle;
    }
}


public enum GameState
{
    None,
    Idle,
    Exit,
    Stage,
    Play,
    Win,
    Lose,
    Pause
}
