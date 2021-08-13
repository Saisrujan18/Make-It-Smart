using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FinalScore : MonoBehaviour
{
    private Text finalText;
    private int scoreFinal=0;
    private int increment = 5;
    private int scoreDisplayed = 0;
    void Start()
    {
        finalText = GetComponent<Text>();
    }
    void Update()
    {
        if (scoreDisplayed < scoreFinal)
        {
            scoreDisplayed += increment;
            finalText.text = "" + scoreDisplayed;
        }
    }
    private void OnEnable()
    {
        scoreFinal = Upgrade.score;
    }
}
