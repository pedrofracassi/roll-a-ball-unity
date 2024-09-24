using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score + "";

        RawImage timePickupPreview = GameObject.Find("PickupPreview").GetComponent<RawImage>();
        timePickupPreview.rectTransform.localScale = new Vector3(2, 2, 2);
    }

    public void FixedUpdate()
    {
        RawImage timePickupPreview = GameObject.Find("PickupPreview").GetComponent<RawImage>();
        // reduce size of time pickup preview until it reaches 1
        if (timePickupPreview.rectTransform.localScale.x > 1)
        {
            timePickupPreview.rectTransform.localScale -= new Vector3(0.13f, 0.13f, 0.13f);
        }
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score + "";
    }

    public int GetScore()
    {
        return score;
    }
}
