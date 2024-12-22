using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseVolume : MonoBehaviour
{
    AudioSource source;
    public Transform target;
    void Start()
    {
         source = GetComponent<AudioSource>();
    }
    
    float Pitch(float value)
    {
        return (float)1.2 - Mathf.Clamp01(value*value / 800);
    }

    void Update()
    {
        source.pitch = Pitch(Vector3.Distance(transform.position, target.position));
    }
}
