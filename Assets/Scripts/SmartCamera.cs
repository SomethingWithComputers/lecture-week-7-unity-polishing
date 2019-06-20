using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartCamera : MonoBehaviour
{
    [SerializeField] private Transform[] _transformsToFocusOn = null;
    [SerializeField] private bool _isAnimatingMovement = true;

    private int _currentTransformToFocusOnIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Move one over to the left, start at the end when we're at the beginning (3 -> 2 -> 1 -> 0 -> 3, etc.)
            if (_currentTransformToFocusOnIndex == 0)
            {
                _currentTransformToFocusOnIndex = _transformsToFocusOn.Length - 1;
            }
            else
            {
                _currentTransformToFocusOnIndex = _currentTransformToFocusOnIndex - 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // Move one over to the right, start at the beginning when we're at the end (0 -> 1 -> 2 -> 3 -> 0, etc.)
            _currentTransformToFocusOnIndex = (_currentTransformToFocusOnIndex + 1) % _transformsToFocusOn.Length;
        }

        Vector3 position = transform.position;
        float targetX = _transformsToFocusOn[_currentTransformToFocusOnIndex].position.x;

        // If we're animating movement, move on over slowly
        if (_isAnimatingMovement)
        {
            position.x = position.x + (targetX - position.x) * Time.deltaTime * 4.0f; // The * 4 speeds it up a bit
        }
        else
        {
            position.x = targetX;
        }

        transform.position = position;
    }
}