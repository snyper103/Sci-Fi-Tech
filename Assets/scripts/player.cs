using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3.5f, _jumpSpeed = 8.0f, _gravity = 9.81f, _sprayDistance = 10.0f, _fireRate = 0.05f;
    [SerializeField] private GameObject _fire, _weapon, _hitMarker;
    private AudioManager _audioManager;
    private UImanager _UImg;
    private Vector3 _movement = Vector3.zero;
    private CharacterController _controller;
    private float _jumpHeight = 0.0f;
    private int _currentAmo, _maxAmo = 50, _numberCoins = 0;
    private bool _isReloading = false, _haveWeapon = false, _canFire =  true;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _audioManager = GameObject.FindWithTag("audioManager").GetComponent<AudioManager>();
        _UImg = GameObject.FindWithTag("UI_Manager").GetComponent<UImanager>();

        logError();

        _currentAmo = _maxAmo;
        _UImg.updateAmo(0, 0);
        _weapon.SetActive(false);
    }
    void logError()
    {
        if ( !_controller )
            Debug.LogError("player::_controller is NULL");

        if ( !_fire )
            Debug.LogError("player::_fire is NULL");

        if ( !_weapon )
            Debug.LogError("player::_weapon is NULL");

        if ( !_hitMarker )
            Debug.LogError("player::_hitMarker is NULL");

        if ( !_audioManager )
            Debug.LogError("player::_audioManager is NULL");

        if ( !_UImg )
            Debug.LogError("player::_UImg is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        playerFall();

        if ( _haveWeapon )
            weaponBehaviour();
    }

#region movement
    void calculateMovement()
    {
        NavMeshHit hit;

        if ( _controller.isGrounded )
        {
            _jumpHeight = Input.GetAxis("Jump")*Mathf.Sqrt(_jumpSpeed*2.0f*_gravity);

            if ( _jumpHeight >= 0.3f )
            {
                _movement = new Vector3(0.0f, _jumpHeight, 0.0f)*Time.deltaTime;
                _movement = transform.transform.TransformDirection(_movement);

                _controller.Move(_movement);
            }
        }

        else
            _jumpHeight -= _gravity*Time.deltaTime;

        _movement = new Vector3(Input.GetAxis("Horizontal")*_moveSpeed, _jumpHeight, Input.GetAxis("Vertical")*_moveSpeed)*Time.deltaTime;
        _movement = transform.transform.TransformDirection(_movement);

        bool isValid = NavMesh.SamplePosition(_movement+transform.position, out hit, 0.3f, NavMesh.AllAreas);

        if ( isValid )
            _controller.Move(hit.position-transform.position);
    }

    void playerFall()
    {
        if ( transform.position.y <= -4.0f )
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }
#endregion

#region weapon
    void weaponBehaviour()
    {
        if ( Input.GetMouseButton(0) && _currentAmo > 0 && !_isReloading && _canFire )
        {
            fireUp();
            StartCoroutine(fireRate());
        }

        else
            ceasefire();

        if ( Input.GetKeyDown(KeyCode.R) && !_isReloading )
            StartCoroutine(reloadingAmo());
    }
    void fireUp()
    {
        RaycastHit hitInfo;

        Ray rayOrigin = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2.0f, Screen.height/2.0f, 0.0f));

        if ( Physics.Raycast(rayOrigin, out hitInfo, _sprayDistance) )
        {
            hitBehaviour(hitInfo);

            GameObject newHitMarker = Instantiate(_hitMarker, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(newHitMarker, 0.1f);
        }

        _fire.SetActive(true);
        _audioManager.Play("shoot");

        --_currentAmo;

        _UImg.updateAmo(_currentAmo, _maxAmo);
    }
    void ceasefire()
    {
        _fire.SetActive(false);
        _audioManager.stopPlaying("shoot");
    }
    IEnumerator reloadingAmo()
    {
        _isReloading = true;
        yield return new WaitForSeconds(1.5f);

        _currentAmo = _maxAmo;
        _isReloading = false;
        _UImg.updateAmo(_currentAmo, _maxAmo);
    }
    IEnumerator fireRate()
    {
        _canFire = false;
        yield return new WaitForSeconds(_fireRate);

        _canFire = true;
    }

    public void activeWeapon()
    {
        _UImg.updateAmo(_currentAmo, _maxAmo);
        _weapon.SetActive(true);
        _haveWeapon = true;
    }

    void hitBehaviour(RaycastHit hitInfo)
    {
        if ( hitInfo.transform.name == "Wooden_Crate" )
        {
            woodenBox box = hitInfo.transform.GetComponent<woodenBox>();

            if ( !box )
            {
                Debug.LogError("player::box is NULL");
                return;
            }

            box.destroyBox();
            return;
        }

        if ( hitInfo.transform.name == "visibilityCube" )
        {
            invBox box = hitInfo.transform.GetComponent<invBox>();

            if ( !box )
            {
                Debug.LogError("player::box is NULL");
                return;
            }

            box.changeTransparency();
            return;
        }

        if ( hitInfo.transform.name == "reflectCube" )
        {
            reflectBox box = hitInfo.transform.GetComponent<reflectBox>();

            if ( !box )
            {
                Debug.LogError("player::box is NULL");
                return;
            }

            box.changeReflection();
            return;
        }

        if ( hitInfo.transform.name == "textureCube" )
        {
            textureBox box = hitInfo.transform.GetComponent<textureBox>();

            if ( !box )
            {
                Debug.LogError("player::box is NULL");
                return;
            }

            box.changeTexture();
            return;
        }
    }
#endregion

    public void incrementCoin()
    {
        ++_numberCoins;

        _UImg.updateCoin(_numberCoins);
    }
    public void decrementCoin()
    {
        --_numberCoins;

        _UImg.updateCoin(_numberCoins);
    }
    public int returnCoin()
    {
        return _numberCoins;
    }
}
