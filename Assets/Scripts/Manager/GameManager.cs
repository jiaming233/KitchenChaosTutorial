using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }
    public static GameManager Instance {  get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameResume;

    [SerializeField] private Player player;
    private State state;

    private float waitingToStartTimer = 1;
    private float countDownToStartTimer = 3;
    [SerializeField] private float gamePlayingTimer = 10;
    private float gamePlayingTimeTotal;

    private bool isGamePause = false;

    private void Awake()
    {
        Instance = this;
        gamePlayingTimeTotal = gamePlayingTimer;
        TurnToWaitingToStart();
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        if (SettingUI.Instance.IsShow())
            return;

        ToggleGame();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                //waitingToStartTimer -= Time.deltaTime;
                //if (waitingToStartTimer <= 0)
                //{
                //    TurnToCountDownToStart();
                //}
                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer <= 0)
                {
                    TurnToGamePlaying();
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0)
                {
                    TurnToGameOver();
                }
                break;
            case State.GameOver:
                break;
        }
    }
    private void TurnToWaitingToStart()
    {
        state = State.WaitingToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TurnToCountDownToStart()
    {
        state = State.CountDownToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToGamePlaying()
    {
        state = State.GamePlaying;
        EnablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToGameOver()
    {
        state = State.GameOver;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void EnablePlayer()
    {
        player.enabled = true;
    }

    public void DisablePlayer() 
    {
        player.enabled = false; 
    }

    public bool IsWaitingToStart()
    {
        return state == State.WaitingToStart;
    }

    public bool IsCountingDown()
    {
        return state == State.CountDownToStart;
    }

    public float GetCountDownTimer()
    {
        return countDownToStartTimer;
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public void ToggleGame()
    {
        isGamePause = !isGamePause;
        if (isGamePause)
        {
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;
            OnGameResume?.Invoke(this, EventArgs.Empty);
        }
    }

    public float GetGamePlayingTimer()
    {
        return gamePlayingTimer;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return gamePlayingTimer / gamePlayingTimeTotal;
    }
}
