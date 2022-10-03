using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflectBox : MonoBehaviour
{
    private int _count = 0;
    private Renderer _cubeRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _cubeRenderer = GetComponent<Renderer>(); 

        logError();
    }
    void logError()
    {
        if ( !_cubeRenderer )
            Debug.LogError("reflectBox::_cubeRenderer is NULL");
    }

    public void changeReflection()
    {
        ++_count;
        _count %= 3;

        switch ( _count )
        {
            case 0:
                _cubeRenderer.material.SetFloat("_Metallic", 1.0f);
                break;

            case 1:
                _cubeRenderer.material.SetFloat("_Metallic", 0.5f);
                break;

            case 2:
                _cubeRenderer.material.SetFloat("_Metallic", 0.0f);
                break;
        }
    }
}
