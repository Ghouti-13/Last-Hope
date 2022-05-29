using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public Action<bool> OnStartGame;

    public static bool gameLaunched = false;
    public static bool isGameover = false;

    private SpawnManager _spawnManager;
    private UIManager _UImanager;

    private void OnEnable()
    {
        OnStartGame += LaunchGame;
    }
    private void Start()
    {
        gameLaunched = false;
        isGameover = false;
        _spawnManager = FindObjectOfType<SpawnManager>();
        _UImanager = FindObjectOfType<UIManager>();
    }
    private void Update()
    {
        if (gameLaunched && Camera.main.orthographicSize < 6.5f)
        {
            Camera.main.orthographicSize += Time.deltaTime;
        }
        else if (isGameover && Camera.main.orthographicSize > 3f)
        {
            Camera.main.orthographicSize -= Time.deltaTime;
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        _UImanager.PauseUI(true);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        _UImanager.PauseUI(false);
    }
    public void StartGame()
    {
        OnStartGame?.Invoke(true);
    }
    public void WinGame()
    {
        _UImanager.OnWin?.Invoke();
        OnStartGame?.Invoke(false);

        isGameover = true;
        gameLaunched = false;
    }
    public void GameOver()
    {
        _UImanager.OnLose?.Invoke();
        OnStartGame?.Invoke(false);

        isGameover = true;
        gameLaunched = false;
    }
    private void LaunchGame(bool value)
    {
        if (value)
        {
            _spawnManager.StartSpawn();
            gameLaunched = true;
            isGameover = false;
            _UImanager.EnableGameplayUI();
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
