using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roundabout : MonoBehaviour
{
    [SerializeField] private float _duration = 16.0f;
    [SerializeField] private AudioSource _audioSource = null;

    private bool _hasStarted = false;
    private float _timePassed = 0.0f;

    // Update is called once per frame
    private void Update()
    {
        // If we haven't started yet, and space is pressed, we start!
        if (!_hasStarted && Input.GetKeyDown(KeyCode.Space))
        {
            _hasStarted = true;
            // Notice the spatial blend! It should be on 3d
            _audioSource.Play();
        }

        if (_hasStarted)
        {
            // Move along, until we've passed our duration. Then start back at 0
            if (_timePassed + Time.deltaTime >= _duration)
            {
                _hasStarted = false;
                _timePassed = 0.0f;
            }
            else
            {
                _timePassed += Time.deltaTime;
            }

            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.y = (_timePassed / _duration) * 360.0f; // Check how far we are in total, and rotate that much
            transform.eulerAngles = eulerAngles;
            
            // Hobble around a bit
            Vector3 position = transform.position;
            position.y = Random.Range(0.0f, 0.2f);
            transform.position = position;
        }
    }
}