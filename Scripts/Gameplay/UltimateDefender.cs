using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateDefender : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosotionEffect;
    [SerializeField] private Slider _overheatingSlider;
    private GameManager _gameManager;
    private AudioManager _audioManager;

    private void OnEnable()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _audioManager = FindObjectOfType<AudioManager>();

        _gameManager.OnStartGame += EnableSlider;
    }
    private void Start()
    {
        _overheatingSlider.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (_overheatingSlider.value > 0)
            _overheatingSlider.value -= Mathf.Abs(DefendersController._currentSpeed / 1.5f) * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SolarFlare") && _overheatingSlider.value <= 0)
        {
            _overheatingSlider.value = _overheatingSlider.maxValue;
            _audioManager.PlayHitUltimateDefenderSound();
            DefendersController.enginePower += 10f;
            DefendersController.enginePower = (DefendersController.enginePower > 100) ? 100f : DefendersController.enginePower;
            SpawnManager.currentSolarFlare--;
            Destroy(other.gameObject);
            _explosotionEffect.Play();
        }
    }
    private void EnableSlider(bool value)
    {
        _overheatingSlider.gameObject.SetActive(value);
    }
}
