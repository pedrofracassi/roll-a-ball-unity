using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthKeeper : MonoBehaviour
{
    public int health = 5;

    public void AddHealth(int deltaHealth)
    {
        health += deltaHealth;
        HealthUpdated();
    }

    public void RemoveHealth(int deltaHealth)
    {
        health -= deltaHealth;
        HealthUpdated();
    }

    public void ResetHealth()
    {
        health = 0;
        HealthUpdated();
    }

    public int GetHealth()
    {
        return health;
    }

    void HealthUpdated()
    {
        if (health <= 0)
        {
            CrossSceneData.score = GameObject.Find("Score").GetComponent<ScoreKeeper>().GetScore();
            CrossSceneData.timeElapsed = (int)GameObject.Find("Timer").GetComponent<TimeKeeper>().timeElapsed;
            SceneManager.LoadScene("GameOver");
        }

        // update health display
        GameObject.Find("Health").GetComponent<HealthDisplay>().UpdateHealth(health);

        if (health == 1)
        {
            GameObject.Find("CriticalHealth").GetComponent<AudioSource>().Play();
        }
        else
        {
            GameObject.Find("CriticalHealth").GetComponent<AudioSource>().Stop();
        }
    }
}
