using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStarter : MonoBehaviour
{
    [SerializeField] private GameObject _prefabFirework = null;
    
    private Camera _camera = null;
    private Rocket _currentRocket = null;
    private Quaternion _initialRotation;

    private void Start()
    {
        _camera = GetComponent<Camera>(); // Same as Camera.main in this case :)
        
        _initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (_currentRocket)
        {
            transform.LookAt(_currentRocket.transform);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // If there is already a rocket flying, refocus on the new one and stop listening to this one
                if (_currentRocket)
                {
                    _currentRocket.OnDone -= onRocketDone;
                }
                
                // Create new fireworks and also keep track of it as the last one added
                _currentRocket = Instantiate(_prefabFirework, hit.point, Quaternion.identity).GetComponent<Rocket>();
                _currentRocket.OnDone += onRocketDone;
            }
        }
    }

    private void onRocketDone()
    {
        // Focus ourself back where we started
        transform.rotation = _initialRotation;
    }
}