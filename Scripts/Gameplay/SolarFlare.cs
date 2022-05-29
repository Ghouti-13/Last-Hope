using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarFlare : MonoBehaviour
{
    public int damage = 20;
    public float minSpeed = 0.5f, maxSpeed = 1.2f;

    private float _speed;
    private List<Transform> _soloarFlareTragets = new List<Transform>();
    private Vector3 _target;
    private GameObject _solarFlareTargetParent;

    void Start()
    {
        _solarFlareTargetParent = GameObject.Find("SolarFlaresTarget");
        SetTarget();
        _speed = Random.Range(minSpeed, maxSpeed);
        _target = GetRandomTarget().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameover) CancelInvoke();

        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }
    private void SetTarget()
    {
        for (int i = 0; i < _solarFlareTargetParent.transform.childCount; i++)
        {
            _soloarFlareTragets.Add(_solarFlareTargetParent.transform.GetChild(i).transform);
        }
    }
    private Transform GetRandomTarget() 
    {
        return _soloarFlareTragets[Random.Range(0, _soloarFlareTragets.Count)];
    }
}
