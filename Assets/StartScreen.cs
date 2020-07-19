using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public Button buttonB;
    public Image button;
    public TextMeshProUGUI pressText;
    public TextMeshProUGUI nameText;
    public Image clock;
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StartFading()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.5f);
        var color = button.color;
        while (button.color.a > 0)
        {
            color = button.color;
            color.a = Mathf.Clamp01(color.a - Time.deltaTime/6);
            button.color = color;
            pressText.color = color;
            nameText.color = color;
            clock.color = color;
            yield return null;
        }

        buttonB.enabled = false;

    }
    
    
}
