using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerButton : MonoBehaviour {

    [SerializeField] GameObject assignedTable;

    public bool correct;

    private int tableNum;

    private void Start()
    {
        tableNum = assignedTable.GetComponent<Table>().tableNum;
    }

    public void Clicked() 
    {
        if (tableNum == 1)
        {
            if (correct == true)
            {
                Messenger.Broadcast(GameEvent.CORRECT_ANSWER, tableNum, true);
                Messenger.Broadcast(GameEvent.REVEAL_HIDDEN, tableNum);
            }
            else
            {
                Messenger.Broadcast(GameEvent.WRONG_ANSWER, tableNum, false);
            }
        }
        else if (tableNum == 2)
        {
            if (correct == true)
            {
                Messenger.Broadcast(GameEvent.CORRECT_ANSWER, tableNum, true);
                Messenger.Broadcast(GameEvent.REVEAL_HIDDEN, tableNum);
            }
            else
            {
                Messenger.Broadcast(GameEvent.WRONG_ANSWER, tableNum, false);
            }
        }
        else
        {
            Messenger.Broadcast(GameEvent.CORRECT_ANSWER, tableNum, true);
            Messenger.Broadcast(GameEvent.APOLOGY_ACCEPTED);
        }
    }
   
}
