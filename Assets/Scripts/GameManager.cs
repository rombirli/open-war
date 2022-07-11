using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    private static bool _paused = false;

    public static bool Paused
    {
        get => _paused;

        set
        {
            _paused = value;
            Time.timeScale = _paused ? 0 : 1;
        }
    }

    public static Game CurrentGame { get; set; }
}