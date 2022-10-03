using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invBox : MonoBehaviour
{
    private int _count = 0;
    private Vector4 _color = Vector4.zero;
    private Renderer _cubeRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _cubeRenderer = GetComponent<Renderer>(); 
        _color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);

        logError();
    }
    void logError()
    {
        if ( !_cubeRenderer )
            Debug.LogError("invBox::_cubeRenderer is NULL");
    }

    public void changeTransparency()
    {
        ++_count;
        _count %= 5;

        switch ( _count )
        {
            case 0:
                _color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
                _cubeRenderer.material.SetColor("_Color", _color);
                break;

            case 1:
                _color = new Vector4(0.0f, 0.0f, 0.0f, 0.75f);
                _cubeRenderer.material.SetColor("_Color", _color);
                break;

            case 2:
                _color = new Vector4(0.0f, 0.0f, 0.0f, 0.5f);
                _cubeRenderer.material.SetColor("_Color", _color);
                break;

            case 3:
                _color = new Vector4(0.0f, 0.0f, 0.0f, 0.25f);
                _cubeRenderer.material.SetColor("_Color", _color);
                break;

            case 4:
                _color = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
                _cubeRenderer.material.SetColor("_Color", _color);
                break;
        }
    }
}
