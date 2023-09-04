using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VNEagleEngine.Events.Player;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int _currentHp;
    [SerializeField] private int _currentMp;

    private void OnEnable()
    {
        PlayerEvents.PLAYER_GOT_DAMAGE += PlayerGotDame;
        PlayerEvents.PLAYER_GAME_OVER += PlayerGameOver;
    }

    private void OnDisable()
    {
        PlayerEvents.PLAYER_GOT_DAMAGE -= PlayerGotDame;
        PlayerEvents.PLAYER_GAME_OVER -= PlayerGameOver;
    }

    private void SetCurrentHp(int value)
    {
        this._currentHp = value;
    }

    // PLAYER EVENTS
    public void PlayerGotDame(int value)
    {
        if (value > this._currentHp)
        {
            SetCurrentHp(0);
            PlayerEvents.PLAYER_GAME_OVER.Invoke();
        }
        else
        {
            SetCurrentHp(this._currentHp - value);
        }
    }

    public void PlayerGameOver()
    {
        Debug.Log("[PLAYER STATUS] Game Over ");
    }
}
