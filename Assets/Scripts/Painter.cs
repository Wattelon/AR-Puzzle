using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    private bool _enabled;

    private void Update()
    {
        if (_enabled)
        {
            particle.Play();
        }
        else
        {
            particle.Stop();
        }
    }

    public void TogglePainter(bool enable)
    {
        _enabled = enable;
    }
}
