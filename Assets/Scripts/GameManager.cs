using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int currentStage = -1;

    public int levelsCleared = -1;
    bool levelClear;

    public GameObject bubble;
    public GameObject frozenBubble;

    private void Awake()
    {
        instance = this;

        levelClear = false;

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
        if (currentStage != -1 && !levelClear)
        {
            AudioManager.instance.PlaySFX("winSound");

            levelClear = true;
            levelsCleared = Mathf.Max(levelsCleared, currentStage);
            PlayerPrefs.SetInt("S", levelsCleared);
            currentStage += 1;
            SceneFader.instance.FadeToScene("Stage" + currentStage);
        }
    }

    public void QuitToTitle()
    {
        SceneFader.instance.FadeToScene("Start Menu");
    }
}
