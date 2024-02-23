using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float playerAxisX = -7.5f;
    public float noteAxisX = -6.5f;
    public LineRenderer noteLine;
    public int shootAxisX = 7;
    public float minScreenPosY = -3;
    public int screenHeight = 6;
    public float laneOffset;

    public static GameManager Instance;
    public event Action onPlayerLifeUpdate;
    public event Action onPlayerScoreUpdate;
    public int lives = 6;
    public int score = 0;
    private int scorePenalty = 10;
    
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
        Instance = this;
        laneWidth = (float)screenHeight / nbLanes;
        Debug.Log(laneWidth);
        if (noteLine != null)
        {
            for(int i = 0; i < noteLine.positionCount; i++)
            {
                Vector3 pos = noteLine.GetPosition(i);
                pos.x = noteAxisX;
                noteLine.SetPosition(i, pos);
            }
        }
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
        SceneManager.LoadScene("YouWin");
    }

    public void UpdateScore(int scoreBonus)
    {
        score += scoreBonus;
        onPlayerScoreUpdate?.Invoke();
    }
    public void UpdateLives(int livesToChange)
    {
        lives+= livesToChange;
        UpdateScore(-scorePenalty);
        if (lives == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        onPlayerLifeUpdate?.Invoke();
    }
}
