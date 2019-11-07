using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // SECTION: VARIABLES
    [Header("UI Variables")]
    [SerializeField] private Canvas[] canvasList;
    [SerializeField] private int currentCanvas;
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration;

    // Use this for initialization
    void Start()
    {
        SwitchCanvas(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // SECTION: UI
    public void SwitchCanvas(int canvas)
    {
        fadeCanvasGroup.blocksRaycasts = true;
        
        StartCoroutine(CanvasFadeIn(canvas));
    }

    public IEnumerator CanvasFadeIn(int canvas)
    {
        float fadeTimer = 0.0f;

        while (fadeCanvasGroup.alpha < 1.0f)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, (fadeTimer / fadeDuration));
            fadeTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        canvasList[currentCanvas].enabled = false;
        canvasList[canvas].enabled = true;
        currentCanvas = canvas;

        StartCoroutine(CanvasFadeOut(canvas)); 
    }

    public IEnumerator CanvasFadeOut(int canvas)
    {
        float fadeTimer = 0.0f;

        while (fadeCanvasGroup.alpha > 0.0f)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, (fadeTimer / fadeDuration));
            fadeTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        fadeCanvasGroup.blocksRaycasts = false;
    }
}
