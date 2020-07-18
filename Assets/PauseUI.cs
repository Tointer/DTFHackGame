using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public GameObject replayButton;
    public GameObject menuButton;
    public GameObject nextLevelButton;
    public Image darkPanel;
    
    public enum MenuStates{None, Pause, Lose, Win, StartPause}

    public void NewMenuState(MenuStates state)
    {
        replayButton.SetActive(false);
        menuButton.SetActive(false);
        nextLevelButton.SetActive(false);

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
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

}
