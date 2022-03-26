using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Sausage : MonoBehaviour
{
    private Spawner _spawner = null;
    private AudioSource _emergenceSound = null;

    public void GetSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }

    private void Remove()
    {        
        _spawner.ReduceAmount();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Cat>(out Cat cat))
        {
            Remove();
        }
    }

    private void Awake()
    {
        _emergenceSound = GetComponent<AudioSource>();
        _emergenceSound.Play();
    }
}
