using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStuff : MonoBehaviour
{
    public AudioSource MusicSource;
    public bool musicFadeOutEnabled = false;

    void Start()
    {
        PlayerMusic();
    }

    public void PlayerMusic()
    {
        musicFadeOutEnabled = false;
        MusicSource.volume = 1f;
        MusicSource.Play();
    }

    public void FadeOutMusic()
    {
        musicFadeOutEnabled = true;
    }

    void Update()
    {
        if (musicFadeOutEnabled)
        {
            if (MusicSource.volume <= 0.1f)
            {
                MusicSource.Stop();
                musicFadeOutEnabled = false;
            }
            else
            {
                float newVolume = MusicSource.volume - (0.1f * Time.deltaTime);  //change 0.01f to something else to adjust the rate of the volume dropping
                if (newVolume < 0f)
                {
                    newVolume = 0f;
                }
                MusicSource.volume = newVolume;
            }
        }
    }

}
