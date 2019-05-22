using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int targetsToShootDown;
    private int targetsHit;

    [SerializeField] private Transform targetPrefab;
    [SerializeField] private Vector3[] targetPositions;

    private void Awake()
    {
        Messenger.AddListener(GameEvents.TARGET_DESTROYED, OnTargetDestroyed);
        InitTargets();
        Cursor.visible = false;
    }

    void Start()
    {

    }

    private void InitTargets()
    {
        PlayerData playerData = GameManager.manager.loggedInPlayer;
        int targetsToShootDown = 0;
        int targetsHit = 0;
        bool[] targetsTakenDown = playerData.ObjectsShotDown;
        for (int i = 0; i < targetsTakenDown.Length; i++)
        {
            if (targetsTakenDown[i])
            {
                targetsHit++;
            }
            else
            {
                Transform target = Instantiate(targetPrefab, targetPositions[i], Quaternion.identity);
                target.GetComponent<IdComponent>().id = i;
            }
            targetsToShootDown++;
        }
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
