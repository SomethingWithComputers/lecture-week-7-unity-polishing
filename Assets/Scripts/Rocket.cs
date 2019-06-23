using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public event Action OnDone = delegate { };

    [SerializeField] private float _duration = 4.0f;
    [SerializeField] private Vector3 _lift = new Vector3(0.0f, 20.0f, 0.0f);
    [SerializeField] private AudioSource _audioSourceExplosion = null;
    [SerializeField] private AudioSource _audioSourceThruster = null;
    [SerializeField] private Transform _modelContainer = null;
    [SerializeField] private ParticleSystem _particleSystemExplosion = null;
    [SerializeField] private ParticleSystem _particleSystemThruster = null;


    private Boolean _hasExploded = false;
    private Rigidbody _rigidbody = null;

    private void FixedUpdate()
    {
        // Fly upwards as long as we can
        if (!_hasExploded)
        {
            _rigidbody.AddForce(_lift, ForceMode.Acceleration);
        }
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        // Destroy ourselves after the particles are all gone (our own flight time + how long the explosion takes + a little extra to be sure)
        Destroy(gameObject, _duration + _particleSystemExplosion.main.duration + 1.0f);
    }

    private void OnDestroy()
    {
        // Let others know we're done
        OnDone();
    }

    private void Update()
    {
        if (_hasExploded)
        {
            return;
        }

        // Count down to 0 (or lower)
        _duration = _duration - Time.deltaTime;
        if (_duration <= 0.0f)
        {
            // Stop thrusting!
            _audioSourceThruster.Stop();
            _particleSystemThruster.Stop();

            // Start exploding!
            _audioSourceExplosion.Play();
            _particleSystemExplosion.Play();

            // Hide the "model"
            _modelContainer.gameObject.SetActive(false);

            // Make sure we stop moving?
            _rigidbody.isKinematic = true;

            // We've done our job
            _hasExploded = true;
        }
    }
}