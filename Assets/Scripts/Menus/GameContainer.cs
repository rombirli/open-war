using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameContainer : MonoBehaviour
{
    private Game _game;
    public TextMeshProUGUI gameName;
    public TextMeshProUGUI gameStats;

    public Game Game
    {
        get { return _game; }
        set
        {
            _game = value;
            gameName.text = Game.Name;
        }
    }

    public void Play()
    {
        GameManager.CurrentGame = Game;
        SceneManager.LoadScene("Game");
    }

    public void Delete()
    {
        _game.Delete();
        Menu.Instance.RefreshGames();
    }
}