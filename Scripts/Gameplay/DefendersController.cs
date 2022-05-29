using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendersController : MonoBehaviour
{
    public static float enginePower = 100f;
    [SerializeField] private float _orbitGravity = 1f;
    [SerializeField] private GameObject _rightEngines;
    [SerializeField] private GameObject _leftEngines;
    private UIManager _UImanager;

    public static float _currentSpeed = 1f;
    float horizontalInput;

    private void Start()
    {
        _currentSpeed = 1f;
        enginePower = 100;
        _UImanager = FindObjectOfType<UIManager>();
    }
    private void Update()
    {
        if (GameManager.gameLaunched && enginePower > 0)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            switch (horizontalInput)
            {
                case 0:
                    _currentSpeed = (_currentSpeed < _orbitGravity) ? _currentSpeed += _orbitGravity * Time.deltaTime : _currentSpeed;
                    StopEngine();
                    break;
                case 1:
                    _currentSpeed += _orbitGravity * Time.deltaTime;
                    _currentSpeed = Mathf.Clamp(_currentSpeed, 1f, 15f); // 1f => minimum speed, 15f => maximum speed.
                    StartEngine(horizontalInput);
                    break;
                case -1:
                    _currentSpeed -= _orbitGravity * Time.deltaTime;
                    StartEngine(horizontalInput);
                    break;
            }
        }

        if (enginePower <= 0) StopEngine();

        ConsumePower();

        transform.Rotate(Vector3.up, (-_currentSpeed + -_orbitGravity) * 5 * Time.deltaTime);
    }
    private void StartEngine(float direction)
    {
        if (direction > 0)
            _rightEngines.SetActive(true);
        else
            _leftEngines.SetActive(true);
    }
    private void StopEngine()
    {
        _rightEngines.SetActive(false);
        _leftEngines.SetActive(false);
    }
    private void ConsumePower()
    {
        if (enginePower > 0)
        {
            enginePower -= Mathf.Abs(horizontalInput) * 2 * Time.deltaTime;
            _UImanager.UpdateEngineUI(enginePower);
        }
    }
}
