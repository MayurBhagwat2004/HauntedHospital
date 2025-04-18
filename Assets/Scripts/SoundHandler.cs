using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundHandler : MonoBehaviour
{
    public Slider slider;
    public AudioClip audioClip;
    [SerializeField]
    private AudioSource audioSource;
    public static SoundHandler instance;
    private void Awake()
    {
        instance = this;
        if (instance != this)
        {
            Destroy(instance);
        }

    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioClip!=null)
        {
            Debug.Log("inside audioclip");
            audioSource.clip = audioClip;
            audioSource.Play();

        }


    }

    private void Update()
    {

        if (slider!=null)
        {
            if (audioSource.volume != slider.value && audioSource != null)
            {
                VolumeAdjustment(slider.value);
            }
            else
            {
                if (GameObject.Find("volumeSlider"))
                {
                    slider = GameObject.Find("volumeSlider").GetComponent<Slider>();

                }
            }
        }

    }

    public void UpdateMusic(AudioClip newAudioClip)
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.clip = newAudioClip;
        audioSource.Play();
    }




    private bool VolumeAdjustment(float volumeAmount)
    {
        if (audioSource.volume!=volumeAmount)
        {
            audioSource.volume = Mathf.Clamp(volumeAmount,0f,1f);
            return true;
        }
        else
        {
            return false;
        }
    }

}
