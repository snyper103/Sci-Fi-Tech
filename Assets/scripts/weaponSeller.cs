using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSeller : MonoBehaviour
{
    private AudioManager _audioManager;
    private UImanager _UImg;
    private bool _canDisplay = true;

    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GameObject.FindWithTag("audioManager").GetComponent<AudioManager>();
        _UImg = GameObject.FindWithTag("UI_Manager").GetComponent<UImanager>();

        logError();
    }
    void logError()
    {
        if ( !_audioManager )
            Debug.LogError("weaponSeller::_audioManager is NULL");

        if ( !_UImg )
            Debug.LogError("weaponSeller::_UImg is NULL");
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player" )
            _UImg.displayInteraction("shop weapon");
    }

    private void OnTriggerStay(Collider other)
    {
        if ( other.tag == "Player" && Input.GetKeyDown(KeyCode.E) )
        {
            player myPlayer = other.transform.GetComponent<player>();

            if ( !myPlayer )
            {
                Debug.LogError("weaponSeller::myPlayer is NULL");
                return;
            }

            else
            {
                if ( myPlayer.returnCoin() >= 1 && _canDisplay )
                {
                    myPlayer.decrementCoin();
                    myPlayer.activeWeapon();
                    _audioManager.Play("won");
                    _UImg.setDisplayOff();

                    StartCoroutine(defineCanDisplay());
                }

                else
                {
                    if ( _canDisplay )
                    {
                        _UImg.DisplayWarning("You can't buy any weapon!");
                        StartCoroutine(defineCanDisplay());
                    }

                }
            }
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.tag == "Player" )
            _UImg.setDisplayOff();
    }

    private IEnumerator defineCanDisplay()
    {
        _canDisplay = false;
        yield return new WaitForSeconds(5.0f);
        _canDisplay = true;
    }
}
