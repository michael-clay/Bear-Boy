using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectedKey : MonoBehaviour {

    [SerializeField] GameObject[] keyText;
    [SerializeField] GameObject helperPopUp;
    [SerializeField] Text helperMessage;
    [SerializeField] GameObject startLevelPopUp;
    [SerializeField] GameObject pausePopUp;

    private int keyNum;
    private bool flip;

	// Use this for initialization
	void Start () {

        Messenger.AddListener<string>(GameEvent.PICKED_UP_KEY, ToggleUI);
        Messenger.AddListener(GameEvent.TALKED_TO_HELPER, EnableHelperPU);

        foreach (GameObject obj in keyText) {
            obj.SetActive(false);
        }

        helperPopUp.SetActive(false);
        pausePopUp.SetActive(false);

        Cursor.visible = true;
        flip = false;
        
        keyNum = 1;
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !flip)
        {
            Messenger.Broadcast(GameEvent.LOCK_CONTROLS);

            pausePopUp.SetActive(true);

            Cursor.visible = true;

            Time.timeScale = 0;

            flip = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && flip)
        {
            Time.timeScale = 1;

            Messenger.Broadcast(GameEvent.UNLOCK_CONTROLS);

            Cursor.visible = false;

            pausePopUp.SetActive(false);

            flip = false;
        }

    }

    private void OnDestroy()
    {
        Messenger.RemoveListener<string>(GameEvent.PICKED_UP_KEY, ToggleUI);
        Messenger.RemoveListener(GameEvent.TALKED_TO_HELPER, EnableHelperPU);
    }

    public void Resume()
    {
        Time.timeScale = 1;

        Messenger.Broadcast(GameEvent.UNLOCK_CONTROLS);

        Cursor.visible = false;

        pausePopUp.SetActive(false);

        flip = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ToggleUI(string key)
    {
        switch (key)
        {
            case ("Key 1"):
                {
                    keyText[0].gameObject.SetActive(true);
                    keyNum++;
                    Messenger.Broadcast(GameEvent.UPDATE_KEY_LOCATION, 1);
                    break;
                }
            case ("Key 2"):
                {
                    keyText[1].gameObject.SetActive(true);
                    keyNum++;
                    Messenger.Broadcast(GameEvent.UPDATE_KEY_LOCATION, 2);
                    break;
                }
            case ("Key 3"):
                {
                    keyText[2].gameObject.SetActive(true);
                    keyNum++;
                    Messenger.Broadcast(GameEvent.UPDATE_KEY_LOCATION, 3);
                    break;
                }
            case ("Key 4"):
                {
                    keyText[3].gameObject.SetActive(true);
                    keyNum++;
                    Messenger.Broadcast(GameEvent.UPDATE_KEY_LOCATION, 4);
                    break;
                }
            case ("Key 5"):
                {
                    keyText[4].gameObject.SetActive(true);
                    keyNum++;
                    Messenger.Broadcast(GameEvent.UPDATE_KEY_LOCATION, 5);
                    break;
                }
            case ("Key 6"):
                {
                    keyText[5].gameObject.SetActive(true);
                    keyNum++;
                    Messenger.Broadcast(GameEvent.UPDATE_KEY_LOCATION, 6);
                    break;
                }
            case ("Key 7"):
                {
                    keyText[6].gameObject.SetActive(true);
                    keyNum++;
                    Messenger.Broadcast(GameEvent.UPDATE_KEY_LOCATION, 7);
                    break;
                }
        }
    }

    public void EnableHelperPU()
    {
        helperPopUp.SetActive(true);

        Cursor.visible = true;

        Time.timeScale = 0;
    }

    public void DisableHelperPU()
    {
        helperPopUp.SetActive(false);

        Cursor.visible = false;

        Messenger.Broadcast(GameEvent.FINISHED_TALKING);

        Time.timeScale = 1;

        if (keyNum >= 7)
        {
            helperMessage.text = "Here is the last key, you must get to the exit on your own.";
        }
        else
        {
            helperMessage.text = "Here is key " + keyNum;
        }
    }

    public void DisableStartPopUp ()
    {
        startLevelPopUp.SetActive(false);

        Cursor.visible = false;
    }
}
