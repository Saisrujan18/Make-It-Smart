using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    [SerializeField] private GameObject cashPanel;
    [SerializeField] private float ChangeTime = 8f;
    [SerializeField] public int IncomeValue = 650;
    [SerializeField] private AudioSource cashSound;
    private static float changeTime;
    private static int incomeValue;
    private static int totalAmount;
    private static TextMeshProUGUI moneyText;
    private static Image cashPanelImage;
    private static Color org;
    private IEnumerator currentCoroutine;
    private IEnumerator cashCoroutine;
    
    public static Boolean checkCash(int amt)
    {
        return amt <= totalAmount;
    }
    public void updateCash(int amt, char c)
    {
        if (c == '+') totalAmount += amt;
        else if (c == '-') totalAmount -= amt;
        moneyText.text = "" + totalAmount;
        cashSound.Play();
        Color clr;
        if (c == '+')
            clr = Color.green;
        else
            clr = Color.red;
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = colorBlink(2, 0.2f, clr);
        StartCoroutine(currentCoroutine);
    }
    private void resumeIncome()
    {
        moneyText = gameObject.GetComponent<TextMeshProUGUI>();
        moneyText.text = string.Format("{0:0}", totalAmount);
        cashCoroutine = income();
        StartCoroutine(cashCoroutine);
    }
    private IEnumerator colorBlink(float blinkTime, float blinkInterval, Color blinkColor)
    {
        float startTime = Time.time;
        cashPanelImage.color = org;
        int c = 0;
        while (Time.time - startTime <= blinkTime)
        {
            if (c % 2 == 0)
                cashPanelImage.color = blinkColor;
            else
                cashPanelImage.color = org;
            c++;
            yield return new WaitForSeconds(blinkInterval);
        }
        cashPanelImage.color = org;
    }
    public IEnumerator refresh()
    {
        if (cashCoroutine != null)
            StopCoroutine(cashCoroutine);
        cashPanelImage.color = org;
        float t = Timer.timeRemaining % changeTime;
        if (t != 0)
        {
            yield return new WaitForSeconds(t);
            FindObjectOfType<MoneyScript>().updateCash(incomeValue, '+');
        }
        FindObjectOfType<MoneyScript>().resumeIncome();
    }
    void Start()
    {
        cashPanelImage = cashPanel.GetComponent<Image>();
        totalAmount = 0;
        org = cashPanelImage.color;
        changeTime = ChangeTime;
        incomeValue = IncomeValue;
        resumeIncome();   
    }
    private IEnumerator income()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeTime);
            updateCash(incomeValue, '+');
            
        }
    }

    public void setExtra(int x)
    {
        incomeValue += x;
        IncomeValue = incomeValue;
        Debug.Log(incomeValue);
        StartCoroutine(refresh());
    }
    
}
