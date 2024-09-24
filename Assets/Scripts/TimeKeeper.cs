using UnityEngine;
using System.Collections;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeKeeper : MonoBehaviour
{

    public float remainingTime = 61.0f;
    public float timeElapsed = 0.0f;
    public Boolean isCounting = true;

    public float textFadeSpeed = 0.01f;

    public TMP_Text timerText;

    void Start()
    {
        timerText.text = remainingTime.ToString("F0");
        InvokeRepeating("TickTime", 1.0f, 1.0f);
    }

    void TickTime()
    {
        if (isCounting)
        {
            remainingTime -= 1.0f;
            timeElapsed += 1.0f;
        }

        if (remainingTime <= 10.0f)
        {
            timerText.color = new Color(1, 0, 0, 1);
            if (!GameObject.Find("CriticalTime").GetComponent<AudioSource>().isPlaying)
            {
                GameObject.Find("CriticalTime").GetComponent<AudioSource>().Play();
                GameObject.Find("CriticalTime").GetComponent<AudioSource>().time = 1.4f;
            }

            // make BgLoop faster when time is critical
            GameObject.Find("BgLoop").GetComponent<AudioSource>().pitch = 1.2f;
        }
        else
        {
            GameObject.Find("CriticalTime").GetComponent<AudioSource>().Stop();
            GameObject.Find("BgLoop").GetComponent<AudioSource>().pitch = 1.0f;
        }

        if (remainingTime <= 1.0f)
        {
            timerEnded();
            return;
        }

        timerText.text = remainingTime.ToString("F0");
    }

    void FixedUpdate()
    {
        timerText.color = Color.Lerp(timerText.color, Color.white, textFadeSpeed);

        RawImage timePickupPreview = GameObject.Find("TimePickupPreview").GetComponent<RawImage>();
        // reduce size of time pickup preview until it reaches 1
        if (timePickupPreview.rectTransform.localScale.x > 1)
        {
            timePickupPreview.rectTransform.localScale -= new Vector3(textFadeSpeed, textFadeSpeed, textFadeSpeed);
        }
    }

    void timerEnded()
    {
        CrossSceneData.score = GameObject.Find("Score").GetComponent<ScoreKeeper>().GetScore();
        CrossSceneData.timeElapsed = (int)timeElapsed;
        SceneManager.LoadScene("GameOver");
    }

    public void AddTime(float time)
    {
        remainingTime += time;

        if (time > 0)
        {
            // set text color to yellow
            timerText.color = new Color(1, 1, 0, 1);

            RawImage timePickupPreview = GameObject.Find("TimePickupPreview").GetComponent<RawImage>();
            timePickupPreview.rectTransform.localScale = new Vector3(2, 2, 2);
        }
        else
        {
            // set text color to red
            timerText.color = new Color(1, 0, 0, 1);
        }
    }
}