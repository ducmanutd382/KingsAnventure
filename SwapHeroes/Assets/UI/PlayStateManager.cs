using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStateManager : MonoBehaviour
{
    public static PlayStateManager instance;
    public static System.Action PlayStateChanged;

    public static GameState CurrentPlayState
    {
        get => instance._currentPlayState;
        set
        {
            if (instance._lastPlayState != value)
            {
                instance._lastPlayState = instance._currentPlayState;
                instance._currentPlayState = value;
                if (PlayStateChanged != null)
                    PlayStateChanged.Invoke();
            }
        }
    }

    public static GameState LastPlayState
    {
        get => instance._lastPlayState;
        set => instance._lastPlayState = value;
    }

    [SerializeField] GameState _currentPlayState = GameState.None;
    [SerializeField] GameState _lastPlayState = GameState.None;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        CurrentPlayState = GameState.Play;
    }

    public enum GameState
    {
        None,
        Play,
        Pause
    }

}
