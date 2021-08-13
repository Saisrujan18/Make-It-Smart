using System;
using System.Collections;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class rayCast : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject upgradePanelCanvas;
    [SerializeField] private TextMeshProUGUI buildingname;
    [SerializeField] private Image panelSprite;
    [SerializeField] private GameObject[] buttons;
    private Sprite upgradeSprite;
    private Sprite[] buttonSprites;
    private Setup setup;
    private Boolean isPanelActive;
    private string currentBuilding;
    private IEnumerator currentCoroutine;

    [SerializeField] float padding = 8f;
    private GameObject popup;
    private Button currentButton;
    private Color def;
    [SerializeField] private Color enabled;
    [SerializeField] private Color disabled;
    void Start()
    {
        isPanelActive = false;
        upgradePanelCanvas.SetActive(isPanelActive);
        setup = FindObjectOfType<Setup>();
        currentBuilding = "Building";

        def = buttons[0].transform.GetChild(0).GetComponent<Image>().color;
        enabled = new Color(0, 0.90f, 0.30f, 1);
        disabled = new Color(0.8f, 0.05f, 0.05f, 1);
    }
    void Update()
    {
        if (!PauseMenu.isGamePaused())
        {
            if (Input.GetMouseButtonDown(0))//LeftMouseClick - 0
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, ray.direction);
                if (hitInfo)
                {
                    if (hitInfo.collider.CompareTag("Interactable") && hitInfo.transform.gameObject.GetComponent<SpriteRenderer>().color.a == 1)  
                    {
                        StartCoroutine(hitInfo.transform.gameObject.GetComponent<Interactables>().OnClick());
                        isPanelActive = false;
                    }
                    else if (hitInfo.collider.CompareTag("UpgradeCanvas") && isPanelActive)
                    {
                        isPanelActive = true;
                    }
                    else if (hitInfo.collider.CompareTag("Building"))
                    {
                        currentBuilding = hitInfo.collider.name;
                        Debug.Log(currentBuilding);
                        if (setup.sprite.ContainsKey(currentBuilding))
                        {
                            isPanelActive = true;
                            upgradeSprite = setup.sprite[currentBuilding];
                            buttonSprites = setup.upgradeSprite[currentBuilding];
                        }
                        else
                            isPanelActive = false;
                    }
                    else
                        isPanelActive = false;
                }
                else
                    isPanelActive = false;
                if (isPanelActive)
                {
                    refreshPanel();
                    if (currentCoroutine != null)
                        StopCoroutine(currentCoroutine);
                    currentCoroutine = displayPanel('+');
                    StartCoroutine(currentCoroutine);
                }
                else
                {
                    if (currentCoroutine != null)
                        StopCoroutine(currentCoroutine);
                    currentCoroutine = displayPanel('-');
                    StartCoroutine(currentCoroutine);
                }
            }        
        }
        else
            upgradePanelCanvas.SetActive(false);
    }
    private IEnumerator displayPanel(char c)
    {
        float displayTime = 15f;
        float fadeTime = 0.8f;
        CanvasGroup panel = upgradePanelCanvas.GetComponent<CanvasGroup>();
        panel.alpha = 1;
        if (c == '+')
        {
            setPanel(currentBuilding);
            yield return new WaitForSeconds(displayTime);
        }
        while (panel.alpha > 0)
        {
            panel.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }
        upgradePanelCanvas.SetActive(false);
    }
    public static void refreshPanel()
    {
        FindObjectOfType<rayCast>().setButtons();
    }
    private void setButtons()
    {
        string[] upgrades=setup.upgradeList[currentBuilding];
        for (int i = 0; i < upgrades.Length; i++)
        {
            buttons[i].SetActive(true);
            buttons[i].transform.GetChild(1).gameObject.SetActive(false);
            buttons[i].transform.GetChild(0).GetComponent<Image>().color = def;
        }
        for (int i = upgrades.Length; i < buttons.Length; i++)
            buttons[i].SetActive(false);
        for (int i = 0; i < upgrades.Length; i++)
        {
            Image upgradeImage = buttons[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
            upgradeImage.sprite = buttonSprites[i];
            //reduced opacity of sprite if locked
            if (setup.isButtonDisabled[currentBuilding][i])
                upgradeImage.color = new Color(1, 1, 1, 0.5f);
            else
                upgradeImage.color = new Color(1, 1, 1, 1f);
        }
    }
    private void setPanel(string buildingname)
    {
        this.buildingname.text = buildingname;
        panelSprite.sprite = upgradeSprite;
        upgradePanelCanvas.SetActive(true);   
    }
    public void upgrade(int index)
    {
        if (!setup.isButtonDisabled[currentBuilding][index - 1])
            FindObjectOfType<Upgrade>().setState(index, currentBuilding);
        else
            FindObjectOfType<Upgrade>().DisplayMessage("Completed");
    }
    public void mouseEnter(int i)
    {
        //Debug.Log("Mouse Enter Button:" + i);
        currentButton = buttons[i - 1].transform.GetChild(0).GetComponent<Button>();
        popup = buttons[i - 1].transform.GetChild(1).gameObject;
        RectTransform popupTransform = popup.GetComponent<RectTransform>();
        String upgradeDesc = setup.upgradeList[currentBuilding][i - 1];
        TextMeshProUGUI popupText = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (!setup.isButtonDisabled[currentBuilding][i - 1]) 
        {
            popupText.text = upgradeDesc;
            currentButton.GetComponent<Image>().color = enabled;
        }
        else
        {
            popupText.text = "Completed";
            currentButton.GetComponent<Image>().color = disabled;
        }
        Vector2 trueSize = popupText.GetPreferredValues(popupText.text);
        Vector2 preferredSize = new Vector2(trueSize.x + padding * 2f, trueSize.y + padding * 2f);
        Vector2 anchorPos = new Vector2(preferredSize.x / 4f, preferredSize.y / 2f);
        popupTransform.sizeDelta = preferredSize;
        popupTransform.anchoredPosition = anchorPos;
        popup.SetActive(true);
    }
    public void mouseExit(int i)
    {
        //Debug.Log("Mouse Exit");
        currentButton.GetComponent<Image>().color = def;
        popup.SetActive(false);
    }
}