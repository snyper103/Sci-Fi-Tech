using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] private float _lookSpeedY = 30.0f, _lookSpeedX = 70.0f;
    [SerializeField] private Transform _playerBody;
    private Vector3 _lookAt = Vector3.zero;
    private float _xRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localEulerAngles = Vector3.zero;

        logError();
    }
    void logError()
    {
        if ( !_playerBody )
            Debug.LogError("camera::_playerBody is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        turnAround();
    }

    void turnAround()
    {
        _lookAt = new Vector3(Input.GetAxis("Mouse Y")*_lookSpeedY, Input.GetAxis("Mouse X")*_lookSpeedX, 0.0f)*Time.deltaTime;

        _xRotation -= _lookAt.x;
        _xRotation = Mathf.Clamp(_xRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0.0f, 0.0f);
        _playerBody.Rotate(Vector3.up*_lookAt.y);
    }
}
