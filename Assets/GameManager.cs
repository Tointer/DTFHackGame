using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, ICount
{
    private GameObject player;
    public Counter counter;
    public static GameManager Instance;
    public PauseUI pauseUi;
    private bool endGame;
    public AudioManager audioManager;

    public static bool IsFastForwarding;
    public event Action StartFastForward;
    public event Action StopFastForward;


    public bool disableStartPause;

    private void Update()
    {
        if (endGame) return;
        if (Input.GetButtonUp("Jump"))
        {
            if (Time.timeScale == 0) UnpauseGame();
            else PauseGame(PauseUI.MenuStates.Pause);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            StartFastForward?.Invoke();
        }

        if (Input.GetButton("Fire2"))
        {
            IsFastForwarding = true;
        }
        else
        {
            IsFastForwarding = false;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            StopFastForward?.Invoke();
        }
    }

    public void PauseGame(PauseUI.MenuStates state)
    {
        pauseUi.NewMenuState(state);
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        pauseUi.NewMenuState(PauseUI.MenuStates.None);
        Time.timeScale = 1;
    }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (disableStartPause) return;
        PauseGame(PauseUI.MenuStates.StartPause);
        StartCoroutine(InitialPause());
    }

    IEnumerator InitialPause()
    {
        while (!Input.anyKey)
        {
            yield return null;
        }
        audioManager.PlaySound(AudioManager.Sounds.LevelStart);
        UnpauseGame();
    }
    
    public void PlayerWins()
    {
        endGame = true;
        PauseGame(PauseUI.MenuStates.Win);
        audioManager.PlaySound(AudioManager.Sounds.Succes);
    }

    public void PlayerLoose()
    {
        audioManager.MuteSoundtrack();
        endGame = true;
        PauseGame(PauseUI.MenuStates.Lose);
        audioManager.PlaySound(AudioManager.Sounds.GameOver);
    }

    public void OnMenuButton()
    {
        
    }

    public void OnReplayButton()
    {
        StartCoroutine(LoadMyScene(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadMyScene(int buildIndex)
    {
        pauseUi.FadeIn(0.2f);
        yield return new WaitForSecondsRealtime(0.2f);
        SceneManager.LoadScene(buildIndex);
    }

    public void OnNextLevelButton()
    {
        var nextSceneId = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneId < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadMyScene(nextSceneId));
        }
        else
        {
            Debug.LogError("No scene with ID " + nextSceneId);
        }
    }
    
    public void Tick()
    {
        
    }

    public void EndOfCount()
    {
        PlayerLoose();
    }
}
