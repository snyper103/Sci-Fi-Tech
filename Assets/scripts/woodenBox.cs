using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodenBox : MonoBehaviour
{
    [SerializeField] private GameObject _woodenCracked;

    // Start is called before the first frame update
    void Start()
    {
        logError();
    }
    void logError()
    {
        if ( !_woodenCracked )
            Debug.LogError("woodenBox::_woodenCracked is NULL");
    }

    public void destroyBox()
    {
        Instantiate(_woodenCracked, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
