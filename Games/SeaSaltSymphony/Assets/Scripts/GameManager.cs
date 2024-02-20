using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float playerAxisX = -7.5f;
    public float minScreenPosY = -3;
    public int screenHeight = 6;
    public float laneOffset;

    public int nbLanes = 4;
    public int startLane = 1;
    public SongScriptable songData;
    public AudioSource music;
    public float startDelay = 3;
    public float fadeOutDuration = 2;
    
    public float laneWidth { get; private set; }
    
    public float GetLaneYPos(int lane)
    {
        return minScreenPosY + (lane - 1) * laneWidth + laneOffset;
    }

    private void Awake()
    {
        laneWidth = (float)screenHeight / nbLanes;
        Debug.Log(laneWidth);
    }
    
    void Start()
    {
        float delay = songData.beatLength * startDelay;
        StartCoroutine(PlaySong(music, songData.bgm, delay, songData.songDuration));
    }

    public IEnumerator PlaySong(AudioSource audioSource, AudioClip bgm, float delay, float duration, bool fadeout = true)
    {
        yield return new WaitForSeconds(delay);

        audioSource.clip = bgm;
        audioSource.Play();
        
        yield return new WaitForSeconds(duration);

        if (fadeout) StartCoroutine(Fade(music, fadeOutDuration, 0));
        else audioSource.Stop();
    }
    
    public static IEnumerator Fade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        audioSource.Stop();
        // call the you win screen!! :D But...it doesn't exist yet :c
    }
}
