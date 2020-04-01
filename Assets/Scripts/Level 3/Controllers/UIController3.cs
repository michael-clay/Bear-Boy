using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController3 : MonoBehaviour {

    [SerializeField] private GameObject startPopUp;
    [SerializeField] private GameObject deadPopUp;
    [SerializeField] private GameObject winPopUp;
    [SerializeField] private GameObject pausePopUp;
    [SerializeField] private Text countDown;
    [SerializeField] private Text coinCounter;
    [SerializeField] private Text victoryText;

    private float countDownTimer;
    private int coins;
    private bool stopIt;
    private bool startCountdown;
    private bool flip;

	// Use this for initialization
	void Start ()
    {
        Messenger.AddListener(GameEvent.HIT_PLAYER, EnableDeadPU);
        Messenger.AddListener(GameEvent.PLAYER_VICTORY, EnableWinPU);
        Messenger.AddListener(GameEvent.COLLECTED_COINS, CollectedCoin);

        startPopUp.SetActive(true);
        deadPopUp.SetActive(false);
        winPopUp.SetActive(false);
        pausePopUp.SetActive(false);

        Cursor.visible = true;

        coinCounter.text = coins + " / 5";

        countDownTimer = 4;
        coins = 0;
        stopIt = false;
        startCountdown = false;
        flip = false;
	}

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.HIT_PLAYER, EnableDeadPU);
        Messenger.RemoveListener(GameEvent.PLAYER_VICTORY, EnableWinPU);
        Messenger.RemoveListener(GameEvent.COLLECTED_COINS, CollectedCoin);
    }

    // Update is called once per frame
    void Update ()
    {
        if (startCountdown)
        {
            if (!stopIt)
            {
                if (countDownTimer > 1)
                {
                    countDownTimer = countDownTimer - Time.deltaTime;

                    countDown.text = countDownTimer.ToString();
                }
                else
                {
                    countDown.text = "GO!";
                    StartCoroutine(KillCountdown());

                    Messenger.Broadcast(GameEvent.RELEASE_THE_BALL);
                }
            }
        }

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

    private void EnableWinPU()
    {
        Cursor.visible = true;

        victoryText.text = "You collected " + coins + " / 5 coins!";

        winPopUp.SetActive(true);

        StartCoroutine(DisableControls());
    }

    private void EnableDeadPU()
    {
        Cursor.visible = true;

        deadPopUp.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;

        Messenger.Broadcast(GameEvent.UNLOCK_CONTROLS);

        Cursor.visible = false;

        pausePopUp.SetActive(false);

        flip = false;
    }

    public void CollectedCoin()
    {
        coins++;

        coinCounter.text = coins + " / 5";
    }

    public void Quit()
    {
        Time.timeScale = 1;

        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1;

        LoadingScreenControl.LoadLoadScreen(3);
    }

    public void EndGame()
    {
        LoadingScreenControl.LoadLoadScreen(5);
    }

    public void DisableStartPU()
    {
        startPopUp.SetActive(false);

        Cursor.visible = false;

        startCountdown = true;
    }

    private IEnumerator DisableControls()
    {
        yield return new WaitForSeconds(1);

        Messenger.Broadcast(GameEvent.LOCK_CONTROLS);
    }

    private IEnumerator KillCountdown()
    {
        yield return new WaitForSeconds(2);

        Destroy(countDown);
    }
}
