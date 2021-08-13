using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class setvolume : MonoBehaviour
{
    public AudioMixer mixer;
    public void setlevel(float slidervalue)
    {
        mixer.SetFloat("lvlvol", Mathf.Log10(slidervalue) * 20);
    }
}
