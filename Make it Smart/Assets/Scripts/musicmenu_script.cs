using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class musicmenu_script : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer mixer;
    public void setlevel(float sliderValue)
    {
        mixer.SetFloat("musicvol", Mathf.Log10(sliderValue) * 20);
    }
    
}
