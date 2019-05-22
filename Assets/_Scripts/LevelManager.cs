using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int targetsToShootDown;
    private int targetsHit;

    void Start()
    {
        Cursor.visible = false;
        targetsHit = 0;
        targetsToShootDown = GameObject.FindGameObjectsWithTag("Target").Length;
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvents.TARGET_DESTROYED, OnTargetDestroyed);
    }



    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.TARGET_DESTROYED, OnTargetDestroyed);
    }

    private void OnTargetDestroyed()
    {
        targetsHit++;
        CheckIfGameFinished();
    }

    private void CheckIfGameFinished()
    {
        if(targetsHit >= targetsToShootDown)
        {
            SceneManager.LoadScene("Finish");
        }
    }
}
