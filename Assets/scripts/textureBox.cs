using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textureBox : MonoBehaviour
{
    private int _count = 0;
    [SerializeField] private Texture[] _cubeTexture;
    private Renderer _cubeRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _cubeRenderer = GetComponent<Renderer>(); 

        logError();
    }
    void logError()
    {
        for ( int i = 0; i < _cubeTexture.Length; ++i )
            if ( !_cubeTexture[i] )
                Debug.LogError("textureBox::_cubeTexture is NULL on instance " + i);

        if ( !_cubeRenderer )
            Debug.LogError("textureBox::_cubeRenderer is NULL");
    }

    public void changeTexture()
    {
        ++_count;
        _count %= 4;

        switch ( _count )
        {
            case 0:
                _cubeRenderer.material.mainTexture = _cubeTexture[0];
                break;

            case 1:
                _cubeRenderer.material.mainTexture = _cubeTexture[1];
                break;

            case 2:
                _cubeRenderer.material.mainTexture = _cubeTexture[2];
                break;

            case 3:
                _cubeRenderer.material.mainTexture = _cubeTexture[3];
                break;
        }
    }
}
