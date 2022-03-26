using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPointParent = null;
    [SerializeField] private Sausage _sausage = null;
    [SerializeField] private EatenSausageCounter _countSausage = null;
    [SerializeField] private float _timeBetweenSpawn = 5f;
    [SerializeField] private int _amountSausages = 3;
    private Spawner _spawner = null;
    private int _currentNumberSausages = 0;

    private Transform[] _points;

    public void ReduceAmount()
    {        
        _currentNumberSausages--;
        _countSausage.Increase();
    }

    private void OnEnable()
    {        
        _points = _spawnPointParent.GetComponentsInChildren<Transform>();
        _spawner = GetComponent<Spawner>();
        StartCoroutine(Spawn(_timeBetweenSpawn));
    }

    private IEnumerator Spawn(float timeBetweenSpawn)
    {
        int firstElement = 1;
        var waitSeconds = new WaitForSeconds(timeBetweenSpawn);

        while (true)
        {
            while (_currentNumberSausages < _amountSausages)
            {
                int pointNumber = UnityEngine.Random.Range(firstElement, _points.Length);

                if (_points[pointNumber].transform.childCount == 0)
                {
                    Sausage sausage = Instantiate(_sausage, _points[pointNumber].transform);
                    sausage.GetSpawner(_spawner);
                    _currentNumberSausages++;
                }

                yield return waitSeconds;
            }

            yield return waitSeconds;
        }
    }
}
