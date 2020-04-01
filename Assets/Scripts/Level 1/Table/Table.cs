using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

    public bool Used { get; set; }
    public int tableNum;

    private void Start()
    {
        Used = false;
    }

    public void Operate()
    {
        if (Used == false)
        {
            Time.timeScale = 0;

            if (tableNum == 1)
            {
                Messenger.Broadcast(GameEvent.RIDDLE_1, 1);
            }
            else if (tableNum == 2)
            {
                Messenger.Broadcast(GameEvent.RIDDLE_2, 2);
            }
            else
            {
                Messenger.Broadcast(GameEvent.APOLOGIES, 3);
            }
            
            Used = true;
        }
    }
}

