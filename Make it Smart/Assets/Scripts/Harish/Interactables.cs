using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    [SerializeField] private Sprite[] interactiveIcons;//0 - Crime 1 - Accident/Medical 2 - Repair
    [SerializeField] private float visibleTime = 25f;
    [SerializeField] AudioSource btnclick;
    private Setup setup;
    private int buildingIndex;
    private int iconIndex;
    private IEnumerator currentCoroutine;
    private IEnumerator masterCoroutine;
    void Start()
    {
        setup = FindObjectOfType<Setup>();
        masterCoroutine = initiate();
        StartCoroutine(masterCoroutine);
    }
    private IEnumerator fade(char c, float fadeTime)
    {
        SpriteRenderer icon = gameObject.GetComponent<SpriteRenderer>();
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, (c == '+') ? 0 : 1);
        while (icon.color.a <= 1 && icon.color.a >= 0) 
        {
            if (c == '+') 
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, icon.color.a + Time.deltaTime / fadeTime);
            else
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, icon.color.a - Time.deltaTime / fadeTime);
            yield return null;
        }
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, (c == '+') ? 1 : 0);
    }
    private IEnumerator toggleIcon()
    {
        buildingIndex = Random.Range(0, setup.Buildings.Length);
        gameObject.transform.position = new Vector3(setup.Buildings[buildingIndex].position.x, setup.Buildings[buildingIndex].position.y, -4f);
        iconIndex = Random.Range(0, interactiveIcons.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = interactiveIcons[iconIndex];
        yield return StartCoroutine(fade('+', 1f));
        yield return new WaitForSeconds(visibleTime);
        yield return StartCoroutine(fade('-', 1f));
    }
    private IEnumerator initiate()
    {
        while (true)
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            currentCoroutine = toggleIcon();
            yield return StartCoroutine(currentCoroutine);
        }
    }
    public IEnumerator OnClick()
    {
        if (masterCoroutine != null)
            StopCoroutine(masterCoroutine);
        Upgrade.score += 25;
        string msg = "";
        if (iconIndex == 0)
            msg = "Crime Scene Reported!!";
        else if (iconIndex == 1)
            msg = "Medical Emergency... Patients have reached the Hospital safely\nThe patients are doing well now";
        else if (iconIndex == 2)
            msg = "Repairs Done!!";
        FindObjectOfType<Upgrade>().DisplayMessage(msg);
        btnclick.Play();
        yield return StartCoroutine(fade('-', 1f));
        masterCoroutine = initiate();
        StartCoroutine(masterCoroutine);
    }
}
