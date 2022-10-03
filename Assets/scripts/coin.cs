using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    private AudioManager _audioManager;
    private UImanager _UImg;

     void Start()
    {
        _audioManager = GameObject.FindWithTag("audioManager").GetComponent<AudioManager>();
        _UImg = GameObject.FindWithTag("UI_Manager").GetComponent<UImanager>();

        logError();
    }
    void logError()
    {
        if ( !_audioManager )
            Debug.LogError("coin::_audioManager is NULL");

        if ( !_UImg )
            Debug.LogError("coin::_UImg is NULL");
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player" )
            _UImg.displayInteraction("collect coin");
    }

    private void OnTriggerStay(Collider other)
    {
        if ( other.tag == "Player" && Input.GetKeyDown(KeyCode.E) )
        {
            player myPlayer = other.transform.GetComponent<player>();

            if ( !myPlayer )
            {
                Debug.LogError("coin::myPlayer is NULL");
                return;
            }

            else
            {
                myPlayer.incrementCoin();
                _audioManager.Play("pickUp");
                _UImg.setDisplayOff();
                Destroy(gameObject);
            }
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.tag == "Player" )
            _UImg.setDisplayOff();
    }
}
