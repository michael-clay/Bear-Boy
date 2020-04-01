using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetArea : MonoBehaviour {

    [SerializeField] private int levelNumber;

    private void OnTriggerEnter(Collider other)
    {
        LoadingScreenControl.LoadLoadScreen(levelNumber);
    }
}
