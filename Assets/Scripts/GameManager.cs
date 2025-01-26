using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int currentStage = -1;

    public int levelsCleared = -1;

    public GameObject bubble;
    public GameObject frozenBubble;

    private void Awake()
    {
        instance = this;

        if (levelsCleared < 0)
            levelsCleared = Mathf.Max(0, PlayerPrefs.GetInt("S", 0));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentStage != -1)
        {
            SceneFader.instance.FadeToScene("Stage" + currentStage);
        }

        if (PauseMenu.instance != null && PauseMenu.instance.paused)
            Time.timeScale = 0f;
        else if (Input.GetKey(KeyCode.LeftShift))
            Time.timeScale = 3f;
        else
            Time.timeScale = 1f;
    }

    public void TransformBubble(GameObject oBubble, GameObject bPrefab)
    {
        GameObject nBubble = Instantiate(bPrefab, oBubble.transform);
        nBubble.transform.localPosition = Vector3.zero;
        nBubble.transform.localScale = Vector3.one;
        nBubble.transform.SetParent(null);

        Destroy(oBubble);
    }

    public void StageClear()
    {
        Debug.Log(currentStage);

        if (currentStage != -1)
        {
            levelsCleared = Mathf.Max(levelsCleared, currentStage);
            PlayerPrefs.SetInt("S", levelsCleared);
            currentStage = -1;
            SceneFader.instance.FadeToScene("LevelSelect");
        }
    }
}
