using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    // script is attached to the HealthDisplay UI panel
    // should create HealthHeartPrefabs (images) for each maxHealth

    public GameObject healthHeartPrefab;
    public int maxHealth = 5;
    public int currentHealth = 5;

    private List<GameObject> healthHearts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        RectTransform panelRectTransform = GetComponent<RectTransform>();
        float panelWidth = panelRectTransform.rect.width;
        float heartWidth = healthHeartPrefab.GetComponent<RectTransform>().sizeDelta.x;
        float totalHeartsWidth = heartWidth * maxHealth;
        float spacing = (panelWidth - totalHeartsWidth) / (maxHealth + 1);

        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heart = Instantiate(healthHeartPrefab, transform);
            RectTransform rt = heart.GetComponent<RectTransform>();
            float xPos = (spacing * (i + 1)) + (heartWidth * i) - (panelWidth / 2) + (heartWidth / 2);
            rt.anchoredPosition = new Vector2(xPos, 0);
            healthHearts.Add(heart);
        }
    }

    // update heart positioning just like in Start
    public void Update()
    {
        RectTransform panelRectTransform = GetComponent<RectTransform>();
        float panelWidth = panelRectTransform.rect.width;
        float heartWidth = healthHeartPrefab.GetComponent<RectTransform>().sizeDelta.x;
        float totalHeartsWidth = heartWidth * maxHealth;
        float spacing = (panelWidth - totalHeartsWidth) / (maxHealth + 1);

        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heart = healthHearts[i];
            RectTransform rt = heart.GetComponent<RectTransform>();
            float xPos = (spacing * (i + 1)) + (heartWidth * i) - (panelWidth / 2) + (heartWidth / 2);
            rt.anchoredPosition = new Vector2(xPos, 0);
        }
    }

    // blink last heart when health is 1
    // stop blinking when health is greater than 1
    public void BlinkLastHeart()
    {
        if (currentHealth == 1)
        {
            StartCoroutine(Blink(healthHearts[0]));
        }
        else
        {
            StopCoroutine(Blink(healthHearts[0]));
            healthHearts[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    // coroutine to blink the last heart
    IEnumerator Blink(GameObject heart)
    {
        while (true)
        {
            heart.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
            yield return new WaitForSeconds(0.3f);
            heart.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void UpdateHealth(int newHealth)
    {
        currentHealth = newHealth;

        for (int i = 0; i < healthHearts.Count; i++)
        {
            if (i < currentHealth)
            {
                // set opacity to 1 for health hearts that are active
                healthHearts[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                healthHearts[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
            }
        }

        BlinkLastHeart();
    }
}
