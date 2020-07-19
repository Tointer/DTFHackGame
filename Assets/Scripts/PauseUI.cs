using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public GameObject replayButton;
    public GameObject menuButton;
    public GameObject nextLevelButton;
    public Image darkPanel;
    public GameObject startPauseText;

    private Image replayButtonImage;
    
    public enum MenuStates{None, Pause, Lose, Win, StartPause}

    public void NewMenuState(MenuStates state)
    {
        replayButton.SetActive(false);
        menuButton.SetActive(false);
        nextLevelButton.SetActive(false);
        startPauseText.SetActive(false);

        if (state == MenuStates.None)
        {
            var c = darkPanel.color;
            c.a = 0f;
            darkPanel.color = c;
            return;
        } 
        
        var color = darkPanel.color;
        color.a = 0.5f;
        darkPanel.color = color;
        
        switch (state)
        {
            case MenuStates.Lose:
                replayButton.SetActive(true);
                var image = replayButton.GetComponent<Image>();
                HideGraphic(image);
                StartCoroutine(FadeInImage(image, 1f));
                //menuButton.SetActive(true);
                break;
            case MenuStates.Win:
                nextLevelButton.SetActive(true);
                //menuButton.SetActive(true);
                break;
            case MenuStates.Pause:
                replayButton.SetActive(true);
                //menuButton.SetActive(true);
                break;
            case MenuStates.None:
                break;
            case MenuStates.StartPause:
                startPauseText.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    IEnumerator FadeInImage(Graphic image, float duration, float targetAlpha = 1f)
    {
        if (duration <= 0 || image == null) throw new ArgumentException();

        while (image.color.a <= targetAlpha)
        {
            var color = image.color;
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / duration);
            image.color = color;
            yield return null;
        }
    }

    public void HideGraphic(Graphic graphic)
    {
        var color = graphic.color;
        color.a = 0;
        graphic.color = color;
    }

}
