using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicControl : MonoBehaviour
{
    public AudioSource musicSourse;
    public AudioClip musicClip;
    public bool use_music;
    public Button MusicOn;
    public Button MusicOff;
    public bool checkMusic;
    // Start is called before the first frame update
    public void Init()
    {
        musicSourse = GetComponent<AudioSource>();
        musicSourse.clip = musicClip;
        checkMusic = DataStore.instance.music.checkMusic;

        if (checkMusic==true)
        {
            musicOn();
        }
        else
        {
            musicOff();
        }
            
    }

    public void musicOn()
    {
        MusicOn.gameObject.SetActive(true);
        MusicOff.gameObject.SetActive(false);
        musicSourse.Play();
        checkMusic = false;
    }

    public void musicOff()
    {
        MusicOff.gameObject.SetActive(true);
        MusicOn.gameObject.SetActive(false);
        musicSourse.Stop();
        checkMusic = true;
    }
}
