using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Highscore, LastScore;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Highscore.text = $"High Score: {SaveSystem.LoadPlayerHighScore().highScore}";
            LastScore.text = $"Last Run Score: {SaveSystem.LoadPlayerHighScore().lastScore}";
        } catch
        {
            SaveSystem.SavePlayerHighScore(new PlayerData());
            Highscore.text = $"High Score: {0}";
            LastScore.text = $"Last Run Score: {0}";
        }
    }

    
    public void PlayGame ()
    {
        GameStateManager.ResetGameState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


   public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
