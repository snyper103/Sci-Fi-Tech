using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField] private Text _Amo, _coin, _collectable, _warning;
    private bool _isDisplayOn = false;

    // Start is called before the first frame update
    void Start()
    {
        logError();

        updateCoin(0);
        _collectable.gameObject.SetActive(false);
        _warning.gameObject.SetActive(false);
    }

    void logError()
    {
        if ( !_Amo )
            Debug.LogError("UImanager::_Amo is NULL");

        if ( !_coin )
            Debug.LogError("UImanager::_coin is NULL");

        if ( !_collectable )
            Debug.LogError("UImanager::_collectable is NULL");

        if ( !_warning )
            Debug.LogError("UImanager::_warning is NULL");
    }

#region display
    public void displayInteraction(string name)
    {
        _collectable.text = "Press 'E' to " + name;
        _isDisplayOn = true;
        StartCoroutine(flickeringDisplay());
    }
    private IEnumerator flickeringDisplay()
    {
        while ( _isDisplayOn )
        {
            _collectable.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);

            _collectable.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void setDisplayOff()
    {
        StopCoroutine(flickeringDisplay());
        _isDisplayOn = false;
        _collectable.gameObject.SetActive(false);
    }

    public void DisplayWarning(string warningText)
    {
        _warning.text = warningText;
        StartCoroutine(makeWarning());
    }
    private IEnumerator makeWarning()
    {
        _warning.gameObject.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        _warning.gameObject.SetActive(false);
    }

#endregion

#region update
    public void updateAmo(int currentAmo, int maxAmo)
    {
        _Amo.text = currentAmo + "/" + maxAmo;
    }

    public void updateCoin(int numberCoins)
    {
        _coin.text = "R$ " + 10*numberCoins + ",00";
    }
#endregion
}
