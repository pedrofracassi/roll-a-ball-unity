using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGameOverTexts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // get from CrossSceneData
        int score = CrossSceneData.score;
        int timeElapsed = CrossSceneData.timeElapsed;

        // update the Game Over Texts
        GameObject.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + score;
        GameObject.Find("TimeSurvivedText").GetComponent<TMPro.TextMeshProUGUI>().text = "Time Survived: " + timeElapsed + "s";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
