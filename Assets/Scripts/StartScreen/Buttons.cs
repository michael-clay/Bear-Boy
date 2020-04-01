using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    [SerializeField] Text placeHolder;

	public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame(int levelNumber)
    {
        LoadingScreenControl.LoadLoadScreen(levelNumber);
    }

    public void ChangeGameName(string name)
    {
        PlayerPrefs.SetString("Game Name", name);
        placeHolder.text = PlayerPrefs.GetString("Game Name");
    }
}
