using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public RectTransform window;

    public Animator startAnimator;
    public Animator levelAnimator;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LevelSelect()
    {
        levelAnimator.SetTrigger("Pressed");

        StartCoroutine(FadeAnimation("LevelSelect"));
    }

    public void StartGame()
    {
        startAnimator.SetTrigger("Pressed");

        GameManager.currentStage = GameManager.instance.levelsCleared + 1;
        if (GameManager.currentStage > 10)
            GameManager.currentStage = 1;

        StartCoroutine(FadeAnimation("Stage" + GameManager.currentStage));
    }

    IEnumerator FadeAnimation(string sceneName)
    {
        AudioManager.instance.PlaySFX("bubblePop");
        
        yield return new WaitForSecondsRealtime(0.5f);

        SceneManager.LoadScene(sceneName);

        yield return new WaitForSecondsRealtime(0.5f);

        for (float t = 0f; t < 1f; t += Time.unscaledDeltaTime)
        {
            yield return null;
            window.anchoredPosition = new Vector2(0f, 1200f * t);
        }

        Destroy(gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
