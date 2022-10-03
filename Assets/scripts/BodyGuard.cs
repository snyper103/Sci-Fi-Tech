using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyGuard : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.0f, _angleSpeed = 25.0f;
    private Vector3 _movement = Vector3.zero;

    // FixedUpdate is called once per frame at the currente framerate
    void FixedUpdate()
    {
        calculateMovement();
    }

#region movement
    void calculateMovement()
    {
        if ( transform.localEulerAngles.y >= 270.0f && transform.position.x > 5.0f )
        {
            _movement = Vector3.left*Time.fixedDeltaTime*_moveSpeed;
            transform.position += _movement;
        }

        else
            if ( transform.localEulerAngles.y <= 90.0f && transform.position.x < 7.0f )
            {
                _movement = Vector3.right*Time.fixedDeltaTime*_moveSpeed;
                transform.position += _movement;
            }


        if ( transform.position.x >= 7.0f && transform.localEulerAngles.y < 270.0f )
            transform.localEulerAngles += Vector3.up*Time.fixedDeltaTime*_angleSpeed;

        else
            if ( transform.position.x <= 5.0f && transform.localEulerAngles.y > 90.0f )
                transform.localEulerAngles += Vector3.down*Time.fixedDeltaTime*_angleSpeed;
    }
#endregion
}
