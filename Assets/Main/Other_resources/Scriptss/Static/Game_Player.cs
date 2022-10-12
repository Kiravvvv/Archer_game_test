//Статический скрипт для функционала игрока
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game_Player
{

    /// <summary>
    /// Активация или выключение курсора игрока
    /// </summary>
    /// <param name="_active"></param>
    public static void Cursor_player(bool _active)
    {
        if(_active)
        Cursor.lockState = CursorLockMode.None;
        else
        Cursor.lockState = CursorLockMode.Locked;
    }

}
