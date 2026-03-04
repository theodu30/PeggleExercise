using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameEvents : MonoBehaviour
{
    private static bool fireLocked = false;
    private static bool endOfGame = false;

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && !fireLocked)
        {
            CallFireEvent();
        }
    }

    public static event EventHandler FireEvent;

    public static void CallFireEvent()
    {
        if (endOfGame) return;

        fireLocked = true;
        FireEvent?.Invoke(null, EventArgs.Empty);
    }

    public static event EventHandler<bool> EndOfTurnEvent;

    public static void CallEndOfTurnEvent(bool arg)
    {
        if (endOfGame) return;

        fireLocked = false;
        EndOfTurnEvent?.Invoke(null, arg);
    }

    public static event EventHandler<bool> EndOfGameEvent;

    public static void CallEndOfGameEvent(bool arg)
    {
        if (endOfGame) return;

        endOfGame = true;
        EndOfGameEvent?.Invoke(null, arg);
    }

    public static event EventHandler<float> ScoreCalculatedEvent;

    public static void CallScoreCalculatedEvent(float arg)
    {
        ScoreCalculatedEvent?.Invoke(null, arg);
    }

    public static void GameReset()
    {
        fireLocked = false;
        endOfGame = false;
    }

    public static event EventHandler<int> BallNumberChangedEvent;

    public static void CallBallNumberChangedEvent(int arg)
    {
        if (endOfGame) return;

        BallNumberChangedEvent?.Invoke(null, arg);
    }
}
