using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Action<int, int> OnNextWave;
    public Action<int> OnEarthDamaged;
    public Action OnWin;
    public Action OnLose;

    [Header("Menu UI")]
    [SerializeField] private GameObject _mainmenuUI;
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _gameOverUI;

    [Header("GameplayUI")]
    [SerializeField] private GameObject _gameplayUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private TextMeshProUGUI _wavesText;
    [SerializeField] private Slider _earthStateSlider;
    [SerializeField] private Slider _enginePowerSlider;
    [SerializeField] private TextMeshProUGUI _enginePowerText;

    private void OnEnable()
    {
        OnNextWave += UpdateWaveUI;
        OnEarthDamaged += UpdateEarthStateBar;
        OnWin += WinUI;
        OnLose += GameoverUI;
    }
    private void UpdateWaveUI(int currentWave, int totalWaves)
    {
        _wavesText.text = "Wave " + currentWave + " / " + totalWaves;
    }
    private void UpdateEarthStateBar(int newValue)
    {
        _earthStateSlider.value = newValue;
    }
    public void UpdateEngineUI(float newValue)
    {
        _enginePowerSlider.value = newValue;
        _enginePowerText.text = "ENGINES POWER " + _enginePowerSlider.value.ToString("F1") + "%";
    }
    public void PauseUI(bool value)
    {
        _pauseUI.SetActive(value);
    }
    private void OnDisable()
    {
        OnNextWave -= UpdateWaveUI;
    }
    private void WinUI()
    {
        _winUI.SetActive(true);
        _gameplayUI.SetActive(false);
    }
    private void GameoverUI()
    {
        _gameOverUI.SetActive(true);
        _gameplayUI.SetActive(false);
    }
    public void EnableGameplayUI()
    {
        _gameplayUI.SetActive(true);
        _mainmenuUI.SetActive(false);
    }
}
