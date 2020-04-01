// Michael Clay

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour {

    public const string RIDDLE_1 = "RIDDLE_1";
    public const string RIDDLE_2 = "RIDDLE_2";

    public const string REVEAL_HIDDEN = "REVEAL_HIDDEN";

    public const string PLACE_MOVEABLE = "PLACE_MOVEABLE";

    public const string CORRECT_ANSWER = "CORRECT_ANSWER";
    public const string WRONG_ANSWER = "WRONG_ANSWER";

    public const string APOLOGIES = "APOLOGIES";
    public const string APOLOGY_ACCEPTED = "APOLOGY_ACCEPTED";

    public const string PICKED_UP_OBJECT = "PICKED_UP_OBJECT";
    public const string PLACE_OBJECT = "PLACE_OBJECT";

    public const string PICKED_UP_KEY = "PICKED_UP_KEY";

    public const string TALKED_TO_HELPER = "TALKED_TO_HELPER";
    public const string FINISHED_TALKING = "FINISHED_TALKING";
    public const string UPDATE_KEY_LOCATION = "UPDATE_KEY_LOCATION";

    public const string RELEASE_THE_BALL = "RELEASE_THE_BALL";
    public const string HIT_PLAYER = "HIT_PLAYER";
    public const string PLAYER_VICTORY = "PLAYER_VICTORY";
    public const string COLLECTED_COINS = "COLLECTED_COINS";

    public const string LOCK_CONTROLS = "LOCK_CONTROLS";
    public const string UNLOCK_CONTROLS = "UNLOCK_CONTROLS";
}
