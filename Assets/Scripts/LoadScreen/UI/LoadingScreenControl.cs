using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenControl : MonoBehaviour {

    [SerializeField] Slider slider;
    [SerializeField] Text loadingText;

    private static int sceneToLoad;

    AsyncOperation async;

    public static void LoadLoadScreen(int lvlNum)
    {
        sceneToLoad = lvlNum;
        SceneManager.LoadScene(4);
    }

    public void Start()
    {
        LoadScreen(sceneToLoad);
    }

    private void LoadScreen(int level)
    {
        if (level == 1)
        {
            loadingText.text = "Loading Level 1";
        }
        else if (level == 2)
        {
            loadingText.text = "Loading Level 2";
        }
        else if (level == 3)
        {
            loadingText.text = "Loading Level 3";
        }
        else
        {
            loadingText.text = "Loading...";
        }

        StartCoroutine(LoadingScreen(level));
    }

    private IEnumerator LoadingScreen(int level)
    {
        async = SceneManager.LoadSceneAsync(level);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                slider.value = 1;

                //yield return new WaitForSeconds(2);
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
