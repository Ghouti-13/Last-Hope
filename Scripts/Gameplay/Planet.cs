using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _rotationSpeed = 2f;
    private UIManager _UImanager;
    private GameManager _gameManager;
    private AudioManager _audioManager;

    void Start()
    {
        _UImanager = FindObjectOfType<UIManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _audioManager = FindObjectOfType<AudioManager>();
    }
    void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SolarFlare"))
        {
            SpawnManager.currentSolarFlare--;
            _health -= other.GetComponent<SolarFlare>().damage;
            _audioManager.PlayHitEarthSound();
            _UImanager.OnEarthDamaged?.Invoke((int)_health);
            if (_health <= 0)
                _gameManager.GameOver();
            Destroy(other.gameObject);
        }
    }
}
