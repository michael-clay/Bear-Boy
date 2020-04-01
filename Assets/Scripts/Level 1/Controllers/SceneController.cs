using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    [SerializeField] GameObject hiddenStairs1;
    [SerializeField] GameObject hiddenStairs2;
    [SerializeField] GameObject table1;
    [SerializeField] GameObject table2;
    [SerializeField] GameObject hiddenTable;

    private void Start()
    {
        Messenger.AddListener<int>(GameEvent.REVEAL_HIDDEN, RevealStairs);
        Messenger.AddListener<int, bool>(GameEvent.WRONG_ANSWER, RevealTable);
        Messenger.AddListener(GameEvent.APOLOGY_ACCEPTED, HideTable);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener<int>(GameEvent.REVEAL_HIDDEN, RevealStairs);
        Messenger.RemoveListener<int, bool>(GameEvent.WRONG_ANSWER, RevealTable);
        Messenger.RemoveListener(GameEvent.APOLOGY_ACCEPTED, HideTable);
    }

    void RevealStairs(int tableNum)
    {
        if (tableNum == 1)
        {
            hiddenStairs1.GetComponent<HStairs>().RevealStairs();
        }
        else
        {
            hiddenStairs2.GetComponent<HStairs>().RevealStairs();
        }
    }

    void RevealTable(int tableNum, bool itsWrong)
    {
        hiddenTable.GetComponent<HTable>().RevealTable();
    }

    void HideTable()
    {
        hiddenTable.GetComponent<HTable>().HideTable();

        if (table1.GetComponent<Table>().Used && !table2.GetComponent<Table>().Used)
        {
            table1.GetComponent<Table>().Used = false;
        }
        else if (table1.GetComponent<Table>().Used && table2.GetComponent<Table>().Used)
        {
            table2.GetComponent<Table>().Used = false;
        }
    }
}
