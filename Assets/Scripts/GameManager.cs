using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] string currentStage;

    public int stagesCleared;

    public GameObject bubble;
    public GameObject frozenBubble;

    private void Awake()
    {
        instance = this;

        currentStage = "TestStage";
    }

    // Start is called before the first frame update
    void Start()
    {
        stagesCleared = PlayerPrefs.GetInt("S", 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneFader.instance.FadeToScene(currentStage);
        }
    }

    public void TransformBubble(GameObject oBubble, GameObject bPrefab)
    {
        GameObject nBubble = Instantiate(bPrefab, oBubble.transform);
        nBubble.transform.localPosition = Vector3.zero;
        nBubble.transform.localScale = Vector3.one;
        nBubble.transform.SetParent(null);

        Destroy(oBubble);
    }
}
