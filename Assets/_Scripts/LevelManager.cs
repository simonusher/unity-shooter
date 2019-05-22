using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int targetsToShootDown;
    public int targetsHit;

    [SerializeField] private Transform targetPrefab;
    [SerializeField] private Vector3[] targetPositions;

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvents.TARGET_DESTROYED, OnTargetDestroyed);
        InitTargets();
        Cursor.visible = false;
    }

    private void InitTargets()
    {
        PlayerData playerData = GameManager.manager.loggedInPlayer;
        targetsToShootDown = 0;
        targetsHit = 0;
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
        Messenger<int>.RemoveListener(GameEvents.TARGET_DESTROYED, OnTargetDestroyed);
    }

    private void OnTargetDestroyed(int index)
    {
        GameManager.manager.SetObjectShotDown(index);
        targetsHit++;
        CheckIfGameFinished();
    }

    private void CheckIfGameFinished()
    {
        if(targetsHit >= targetsToShootDown)
        {
            GameManager.manager.ResetObjectsShotDown();
            GameManager.manager.SaveState();
            SceneManager.LoadScene("Finish");
        }
    }
}
