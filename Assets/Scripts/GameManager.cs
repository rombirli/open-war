using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static string CurrentGame { get; set; }
    public static void Pause() => Time.timeScale = 0;
    public static void Resume() => Time.timeScale = 1;
}