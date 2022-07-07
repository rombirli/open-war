using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI gameNameTMP;
    public GameObject gameContainerPrefab;

    public GameObject gameContainersContainer;

    // Start is called before the first frame update
    public static Menu Instance { get; set; }

    void Start()
    {
        RefreshGames();
        Instance = this;
    }

    public void RefreshGames()
    {
        int i;
        for (i = 0; i < gameContainersContainer.transform.childCount; i++)
            Destroy(gameContainersContainer.transform.GetChild(i).gameObject);

        var games = Game.LoadGames().ToArray();
        var gameContainersRectTransform = gameContainersContainer.GetComponent<RectTransform>();
        gameContainersRectTransform.sizeDelta =
            new Vector2(gameContainersRectTransform.sizeDelta.x, games.Length * 100);
        i = 0;
        foreach (var game in games)
        {
            var gameContainer = Instantiate(gameContainerPrefab, gameContainersContainer.transform, false);
            gameContainer.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
            gameContainer.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(20, -100 * ++i);
            gameContainer.GetComponent<GameContainer>().Game = game;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NewGame()
    {
        if (Game.NewGame(gameNameTMP.text, out var game))
        {
            GameManager.CurrentGame = game;
            SceneManager.LoadScene("Game");
        }
    }
}