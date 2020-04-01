using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController3 : MonoBehaviour {

    [SerializeField] GameObject startWall;

	// Use this for initialization
	void Start ()
    {
        Messenger.AddListener(GameEvent.RELEASE_THE_BALL, ReleaseBall);
	}

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.RELEASE_THE_BALL, ReleaseBall);
    }

    private void ReleaseBall()
    {
        Destroy(startWall);
    }
}
