using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class ChargingStation : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _canCharge = true;
    private float _timeSinceCharge = 0.0f;
    private float _timeBetweenCharges = 5.0f;
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

    private void Start()
    {
        InitializeStationObjects();
    }

    private void InitializeStationObjects()
    {
        _lightOneBlue = GameObject.Find("Light One Blue");
        _lightOneRed = GameObject.Find("Light One Red");
        _lightTwoBlue = GameObject.Find("Light Two Blue");
        _lightTwoRed = GameObject.Find("Light Two Red");
        _lightThreeBlue = GameObject.Find("Light Three Blue");
        _lightThreeRed = GameObject.Find("Light Three Red");

        _pointLightBlue = GameObject.Find("Point Light Blue");
        _pointLightRed = GameObject.Find("Point Light Red");
    }


    // Update is called once per frame
    void Update()
    {
        ManageLight();
        if (_canCharge == false)
        {
            _timeSinceCharge += Time.deltaTime;
            if (_timeSinceCharge >= _timeBetweenCharges)
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

}