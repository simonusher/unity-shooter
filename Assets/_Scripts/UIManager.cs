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

    private int targetsToShootDown;
    private int targetsHit;

    void Start()
    {
        Assert.IsNotNull(scoreText, "UIManager: scoreText is null");
        targetsHit = 0;
        targetsToShootDown = GameObject.FindGameObjectsWithTag("Target").Length;
        UpdateScoreText();
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvents.TARGET_DESTROYED, OnHit);
    }



    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.TARGET_DESTROYED, OnHit);
    }

    private void OnHit()
    {
        targetsHit++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = String.Format(scoreTextTemplate, targetsHit, targetsToShootDown);
    }
}
