using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End_game_UI : MonoBehaviour
{
    [Tooltip("Меню выиграной игры")]
    [SerializeField]
    GameObject Canvas_game_wins = null;

    [Tooltip("Меню проигранной игры")]
    [SerializeField]
    GameObject Canvas_game_lose = null;


    private void Start()
    {
        Game_administrator.End_game_event.AddListener(Game_end);
    }

    /// <summary>
    /// Конец игры
    /// </summary>
    /// <param name="_win">Выиграли или проиграли</param>
    void Game_end(bool _win)
    {
        if (_win)
            Invoke("Delay_End_game_win", 0.5f);
        else
            Invoke("Delay_End_game_lose", 0.5f);

        Game_Player.Cursor_player(true);
    }


    /// <summary>
    /// Закончить игру в виде победы (функция для задержки)
    /// </summary>
    void Delay_End_game_win()
    {
        Canvas_game_wins.SetActive(true);
    }

    /// <summary>
    /// Закончить игру в виде поражения (функция для задержки)
    /// </summary>
    void Delay_End_game_lose()
    {
        Canvas_game_lose.SetActive(true);
    }

}
