using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ChargingStation : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _canCharge = true;
    private float _timeSinceCharge = 0.0f;
    private float _timeBetweenCharges = 10.0f;
    private bool _doOnceOn = true;
    private bool _doOnceOff = true;

    private GameObject _lightOneBlue;
    private GameObject _lightOneRed;
    private GameObject _lightTwoBlue;
    private GameObject _lightTwoRed;
    private GameObject _lightThreeBlue;
    private GameObject _lightThreeRed;

    private GameObject _pointLightBlue;
    private GameObject _pointLightRed;

    private GameObject _playerTarget = null;
    private Vector3 _playerPosition;
    private float _senseRadius = 8.0f;

    private SoundManager _soundManager = null;

    private void Start()
    {
        InitializeStationObjects();
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>(); //if exists
        if (player)
        {
            _playerTarget = player.gameObject;
        }
        SoundManager soundManager = FindObjectOfType<SoundManager>(); //if exists
        if (soundManager)
        {
            _soundManager = soundManager;
        }

    }

    private void InitializeStationObjects()
    {
        //gets lights from prefab
        _lightOneBlue = GameObject.Find("Light One Blue");
        _lightOneRed = GameObject.Find("Light One Red");
        _lightTwoBlue = GameObject.Find("Light Two Blue");
        _lightTwoRed = GameObject.Find("Light Two Red");
        _lightThreeBlue = GameObject.Find("Light Three Blue");
        _lightThreeRed = GameObject.Find("Light Three Red");

        _pointLightBlue = GameObject.Find("Point Light Blue");
        _pointLightRed = GameObject.Find("Point Light Red");

        _pointLightBlue.SetActive(false); //lights satrt off
        _pointLightRed.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        _playerPosition = _playerTarget.transform.position; //constantly updates the player positin
        UpdatePlayerPosition();
        if (_canCharge == false) //can it charge?
        {
            _timeSinceCharge += Time.deltaTime; //how long has it been since last charge?
            if (_timeSinceCharge >= _timeBetweenCharges) //has enought time passed?
            {
                _timeSinceCharge = 0.0f; 
                _canCharge = true;
                _doOnceOn = true;
                _doOnceOff = true;
            }
        }
    }

    public void SetChargeMode()
    {
        if (_canCharge == true && _timeSinceCharge == 0.0f) 
        {
            _canCharge = false;
        }
    }

    public bool GetChargingMode()
    {
        return _canCharge;
    }

    private void ManageLight()
    {
        if (_canCharge && _doOnceOn)
        {
            _lightOneBlue.SetActive(true);
            _lightTwoBlue.SetActive(true);
            _lightThreeBlue.SetActive(true);

            _lightOneRed.SetActive(false);
            _lightTwoRed.SetActive(false);
            _lightThreeRed.SetActive(false);

            _pointLightBlue.SetActive(true);
            _pointLightRed.SetActive(false);

            _doOnceOn = false;
            _soundManager.PlayChargerActivated();
        }
        if (!_canCharge && _doOnceOff)
        {
            _lightOneBlue.SetActive(false);
            _lightTwoBlue.SetActive(false);
            _lightThreeBlue.SetActive(false);

            _lightOneRed.SetActive(true);
            _lightTwoRed.SetActive(true);
            _lightThreeRed.SetActive(true);

            _pointLightBlue.SetActive(false);
            _pointLightRed.SetActive(true);

            _doOnceOff = false;
        }
    }


    private void UpdatePlayerPosition()
    {
        if ((transform.position - _playerPosition).sqrMagnitude < _senseRadius * _senseRadius) //if player is in range
        {
            ManageLight(); // manage lights
        }
        if ((transform.position - _playerPosition).sqrMagnitude > _senseRadius * _senseRadius) //if he is not in range
        {
            if(!_doOnceOff) //are they off?
            {
                _pointLightRed.SetActive(false); //then they should
                _doOnceOff = true;
            }
            if (!_doOnceOn) //are the on?
            {
                _pointLightBlue.SetActive(false); //then they should
                _doOnceOn = true;
            }
        }
    }

}