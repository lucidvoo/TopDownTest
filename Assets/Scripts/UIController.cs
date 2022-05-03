using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private PlayerStatsSO playerStats;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject objectInfo;
    [SerializeField] private TextMeshProUGUI objInfoText;

    private InteractiveObjectBase currentActiveObj = null;
    private int prevHealth = -1, 
                prevExp = -1;

    private void Start()
    {
        deathScreen.gameObject.SetActive(false);
        objectInfo.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Events.onPlayerDeath.AddListener(ShowDeathScreen);
        Events.onInteractiveObjectClicked.AddListener(ShowObjectInfo);
        Events.onObjectBecomeNotInteractive.AddListener(TryHideObjInfo);
    }


    private void OnDisable()
    {
        Events.onPlayerDeath.RemoveListener(ShowDeathScreen);
        Events.onInteractiveObjectClicked.RemoveListener(ShowObjectInfo);
        Events.onObjectBecomeNotInteractive.RemoveListener(TryHideObjInfo);
    }

    private void ShowDeathScreen(string obj)
    {
        deathScreen.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (prevHealth != playerStats.Health)
        {
            prevHealth = playerStats.Health;
            healthText.text = playerStats.Health.ToString();
        }

        if (prevExp != playerStats.Exp)
        {
            prevExp = playerStats.Exp;
            expText.text = playerStats.Exp.ToString();
        }
    }

    private void ShowObjectInfo(InteractiveObjectBase objClicked)
    {
        if (currentActiveObj == objClicked)
        {
            return;
        }
        else
        {
            currentActiveObj = objClicked;
        }
        
        objectInfo.gameObject.SetActive(true);
        objInfoText.text = objClicked.description;
    }

    private void TryHideObjInfo(InteractiveObjectBase objLostInteractivity)
    {
        if (currentActiveObj != objLostInteractivity)
        {
            return;
        }

        currentActiveObj = null;
        objectInfo.gameObject.SetActive(false);
    }

}
