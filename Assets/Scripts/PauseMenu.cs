using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public GameObject menu;
    [HideInInspector] public bool paused;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        paused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            menu.SetActive(paused);
        }
    }

    public void RetryClicked()
    {
        if (GameManager.currentStage != -1)
        {
            SceneFader.instance.FadeToScene("Stage" + GameManager.currentStage);
        }
    }

    public void QuitClicked()
    {
        SceneFader.instance.FadeToScene("LevelSelect");
    }
}
