using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject HPDsiplay1;
    [SerializeField]
    private GameObject HPDsiplay2;
    [SerializeField]
    private GameObject HPDsiplay3;

    [SerializeField]
    private TextMeshProUGUI pointsDiplay;
    [SerializeField]
    private TextMeshProUGUI LivesDisplay;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        UpdateHP();
        pointsDiplay.text = playerStats.getPoints() + " pts";
        LivesDisplay.text = "x " + playerStats.getLives();
    }

    private void UpdateHP()
    {
        switch (playerStats.getHP())
        {
            case 3:
                HPDsiplay1.SetActive(true);
                HPDsiplay2.SetActive(true);
                HPDsiplay3.SetActive(true);
                break;
            case 2:
                HPDsiplay1.SetActive(true);
                HPDsiplay2.SetActive(true);
                HPDsiplay3.SetActive(false);
                break;
            case 1:
                HPDsiplay1.SetActive(true);
                HPDsiplay2.SetActive(false);
                HPDsiplay3.SetActive(false);
                break;
            default:
                HPDsiplay1.SetActive(false);
                HPDsiplay2.SetActive(false);
                HPDsiplay3.SetActive(false);
                break;
        }
    }
}
