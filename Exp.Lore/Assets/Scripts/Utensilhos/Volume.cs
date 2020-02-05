using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetarVolume(float valorSlider)
    {
        mixer.SetFloat("VolumeMusica", Mathf.Log10(valorSlider) * 20);
    } 
}
