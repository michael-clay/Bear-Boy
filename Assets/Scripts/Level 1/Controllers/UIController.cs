using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class UIController : MonoBehaviour {

    [SerializeField] GameObject TablePopUp1;
    [SerializeField] GameObject TablePopUp2;
    [SerializeField] GameObject TablePopUp3;
    [SerializeField] GameObject CorrectPopUp;
    [SerializeField] GameObject WrongPopUp;
    [SerializeField] GameObject GrabTheTable;
    [SerializeField] GameObject PlaceTheTable;
    [SerializeField] GameObject AnswerRiddle;
    [SerializeField] GameObject AnswerRiddle2;
    [SerializeField] GameObject RetryRiddle;
    [SerializeField] GameObject Apologize;
    [SerializeField] GameObject startLevelPopUp;
    [SerializeField] GameObject pausePopUp;

    bool flip;

    private void OnDestroy()
    {
        Messenger.RemoveListener<int>(GameEvent.RIDDLE_1, EnableRiddlePU);
        Messenger.RemoveListener<int>(GameEvent.RIDDLE_2, EnableRiddlePU);
        Messenger.RemoveListener<int, bool>(GameEvent.CORRECT_ANSWER, Answer);
        Messenger.RemoveListener<int, bool>(GameEvent.WRONG_ANSWER, Answer);
        Messenger.RemoveListener<int>(GameEvent.APOLOGIES, EnableRiddlePU);
        Messenger.RemoveListener<bool>(GameEvent.PICKED_UP_OBJECT, SwitchGrabIndicator);
        Messenger.RemoveListener<bool>(GameEvent.PLACE_OBJECT, SwitchPlaceIndicator);
    }

    // Use this for initialization
    void Start() {

        Messenger.AddListener<int>(GameEvent.RIDDLE_1, EnableRiddlePU);
        Messenger.AddListener<int>(GameEvent.RIDDLE_2, EnableRiddlePU);
        Messenger.AddListener<int, bool>(GameEvent.CORRECT_ANSWER, Answer);
        Messenger.AddListener<int, bool>(GameEvent.WRONG_ANSWER, Answer);
        Messenger.AddListener<int>(GameEvent.APOLOGIES, EnableRiddlePU);
        Messenger.AddListener<bool>(GameEvent.PICKED_UP_OBJECT, SwitchGrabIndicator);
        Messenger.AddListener<bool>(GameEvent.PLACE_OBJECT, SwitchPlaceIndicator);

        TablePopUp1.SetActive(false);
        TablePopUp2.SetActive(false);
        TablePopUp3.SetActive(false);
        pausePopUp.SetActive(false);
        CorrectPopUp.SetActive(false);
        WrongPopUp.SetActive(false);
        Apologize.SetActive(false);
        PlaceTheTable.SetActive(false);
        AnswerRiddle2.SetActive(false);
        RetryRiddle.SetActive(false);
        AnswerRiddle.SetActive(true);
        GrabTheTable.SetActive(true);

        flip = false;
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

    public void GotIt()
    {
        startLevelPopUp.SetActive(false);
        Cursor.visible = false;
    }

    private void SwitchGrabIndicator(bool flip)
    {
        GrabTheTable.SetActive(flip);
    }

    private void SwitchPlaceIndicator(bool flip)
    {
        PlaceTheTable.SetActive(flip);
    }

    void Answer(int riddleNum, bool right)
    {
        Cursor.visible = false;

        Messenger.Broadcast(GameEvent.UNLOCK_CONTROLS);

        AnswerRiddle.SetActive(false);
        AnswerRiddle2.SetActive(false);
        RetryRiddle.SetActive(false);

        if (right == true)
        {
            if (riddleNum == 1)
            {
                TablePopUp1.SetActive(false);
                StartCoroutine(Close(right));

                AnswerRiddle2.SetActive(true);
            }
            else if (riddleNum == 2)
            {
                TablePopUp2.SetActive(false);
                StartCoroutine(Close(right));

                AnswerRiddle2.SetActive(false);
            }
            else
            {
                TablePopUp3.SetActive(false);
                StartCoroutine(Close(right));

                Apologize.SetActive(false);
                RetryRiddle.SetActive(true);
            }
        }
        else
        {
            Apologize.SetActive(true);

            if (riddleNum == 1)
            {
                TablePopUp1.SetActive(false);
                StartCoroutine(Close(right));
            }
            else
            {
                TablePopUp2.SetActive(false);
                StartCoroutine(Close(right));
            }
        }

    }

    void EnableRiddlePU(int riddleNum)
    {
        Cursor.visible = true;

        Messenger.Broadcast(GameEvent.LOCK_CONTROLS);

        if (riddleNum == 1)
        {
            TablePopUp1.SetActive(true);
        }
        else if (riddleNum == 2)
        {
            TablePopUp2.SetActive(true);
        }
        else
        {
            TablePopUp3.SetActive(true);
        }
    }

    private IEnumerator Close(bool right)
    {
        if (right == true)
        {
            Time.timeScale = 1;

            CorrectPopUp.SetActive(true);

            yield return new WaitForSeconds(1);

            CorrectPopUp.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;

            WrongPopUp.SetActive(true);

            yield return new WaitForSeconds(2);

            WrongPopUp.SetActive(false);
        }
    }
}
