using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Awake()
    {
        GameStateManager.OnPause += Open;
        gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        GameStateManager.OnPause -= Open;
    }

    private void Open()
    {
        gameObject.SetActive(true);
    }

    public void Resume()
    {
        GameStateManager.TogglePause();
        gameObject.SetActive(false);
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
