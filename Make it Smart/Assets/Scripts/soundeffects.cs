using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundeffects : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private int firstPlayInt;
    public Slider soundEffectsSlider;
    private float soundEffectsFloat;
   public AudioSource[] soundEffectsAudio;
    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        if (firstPlayInt == 0)
        {
            soundEffectsFloat = .75f;
            soundEffectsSlider.value = soundEffectsFloat;
            PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
           soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
            soundEffectsSlider.value = soundEffectsFloat;
           
        }
    }
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
    }
    void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }
    public void UpdateSound()
    {
        for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = soundEffectsSlider.value;
        }
    }


}
