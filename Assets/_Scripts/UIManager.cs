using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private string scoreTextTemplate = "Score: {0}/{1}";
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private LevelManager levelManager;
    private int targetsHit;
    private int targetsToShootDown;

    void Start()
    {
        Assert.IsNotNull(scoreText, "UIManager: scoreText is null");
        UpdateValues();
        UpdateScoreText();
    }

    private void UpdateValues()
    {
        targetsHit = levelManager.targetsHit;
        targetsToShootDown = levelManager.targetsToShootDown;
    }

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvents.TARGET_DESTROYED, OnHit);
    }


    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvents.TARGET_DESTROYED, OnHit);
    }

    private void OnHit(int index)
    {
        targetsHit++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = String.Format(scoreTextTemplate, targetsHit, targetsToShootDown);
    }
}
