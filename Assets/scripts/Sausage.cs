using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Sausage : MonoBehaviour
{
    private Spawner _spawner = null;
    private AudioSource instantiateSound = null;

    public void Remove()
    {        
        _spawner.ReduceAmount();
        Destroy(gameObject);
    }

    public void GetSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }

    private void Awake()
    {
        instantiateSound = GetComponent<AudioSource>();
        instantiateSound.Play();
    }
}
