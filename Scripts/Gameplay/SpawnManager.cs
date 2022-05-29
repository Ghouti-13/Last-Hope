using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static int currentSolarFlare = 20;

    public GameObject solarFlarePrefab;

    [SerializeField] private List<Transform> _startTransforms = new List<Transform>();
    private UIManager _UImanager;
    private GameManager _gameManager;
    private float _delay = 1f, _repateRate = 14f;
    private int _solarFlareToSpawn;
    private int _wave = 1;
    private int _maxWaves = 5;

    private void Start()
    {
        currentSolarFlare = 20;
    }
    public void StartSpawn()
    {
        _UImanager = FindObjectOfType<UIManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _UImanager?.OnNextWave(_wave, _maxWaves);
        SetSolarFlareToSpawn(_wave);
        InvokeRepeating("SpawnSolarFlare", _delay, _repateRate);
    }
    private void Update()
    {
        if(currentSolarFlare <= 0 && !GameManager.isGameover)
        {
            _gameManager.WinGame();
        }
    }
    private Vector3 GetRandomPosition()
    {
        return _startTransforms[Random.Range(0, _startTransforms.Count)].position;
    }
    private void SpawnSolarFlare()
    {
        if (_solarFlareToSpawn > 0)
        {
            var solarFlare = Instantiate(solarFlarePrefab);
            var soloarFlareObject = solarFlare.GetComponent<SolarFlare>();
            soloarFlareObject.minSpeed += 0.5f;
            soloarFlareObject.maxSpeed += 1.2f;
            solarFlare.transform.position = GetRandomPosition();
            _solarFlareToSpawn--;
        }
        else
        {
            CancelInvoke();
            _wave++;
            SetSolarFlareToSpawn(_wave);
            if(_wave <= 5) StartCoroutine(NextWaveRoutine());
        }
    }
    private void SetSolarFlareToSpawn(int wave)
    {
        switch (wave)
        {
            case 1: _solarFlareToSpawn = 2;
                break;
            case 2: _solarFlareToSpawn = 3;
                break;
            case 3: _solarFlareToSpawn = 4;
                break;
            case 4: _solarFlareToSpawn = 5;
                break;
            case 5: _solarFlareToSpawn = 6;
                break; 
        }
    }
    IEnumerator NextWaveRoutine()
    {
        yield return new WaitForSeconds(3f);
        _repateRate -= 2f;
        _UImanager.OnNextWave?.Invoke(_wave, _maxWaves);
        InvokeRepeating("SpawnSolarFlare", _delay, _repateRate); 
    }
}