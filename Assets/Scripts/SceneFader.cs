using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;

    public Image fadeImage;

    Coroutine currentPhase;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        fadeImage.gameObject.SetActive(false);
    }

    public void FadeToScene(string scene)
    {
        if (currentPhase != null)
            return;

        currentPhase = StartCoroutine(FadeToSceneC(scene));
    }

    IEnumerator FadeToSceneC(string scene)
    {
        fadeImage.gameObject.SetActive(true);

        float f = 0f;

        while (f < 1f)
        {
            yield return null;
            f += 5f * Time.unscaledDeltaTime;
            f = Mathf.Clamp01(f);

            fadeImage.color = new Color(1, 1, 1, f);
        }

        SceneManager.LoadScene(scene);

        yield return new WaitForSecondsRealtime(0.5f);

        while (f > 0f)
        {
            yield return null;
            f -= 5f * Time.unscaledDeltaTime;
            f = Mathf.Clamp01(f);

            fadeImage.color = new Color(1, 1, 1, f);
        }

        fadeImage.gameObject.SetActive(false);
        currentPhase = null;
    }
}
