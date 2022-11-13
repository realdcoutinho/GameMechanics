using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private GameObject _light1;
    private GameObject _light2;
    private GameObject _light3;
    private GameObject _light4;
    private GameObject _light5;

    BatteryHolder _playerBatteries = null;

    private int _nrOfBatteries = 5;
    private SoundManager _soundManager = null;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player != null)
        {
            _playerBatteries = player.GetComponent<BatteryHolder>();
        }
        SoundManager soundManager = FindObjectOfType<SoundManager>();
        if (soundManager)
        {
            _soundManager = soundManager;
        }

        InitializeLights();
        _nrOfBatteries = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _nrOfBatteries = _playerBatteries.currentBatteryNumber;
        UpdateLights();
    }

    private void InitializeLights()
    {
        _light1 = GameObject.Find("Spot Light 1");
        _light2 = GameObject.Find("Spot Light 2");
        _light3 = GameObject.Find("Spot Light 3");
        _light4 = GameObject.Find("Spot Light 4");
        _light5 = GameObject.Find("Spot Light 5");

        _light1.SetActive(true);
        _light2.SetActive(false);
        _light3.SetActive(false);
        _light4.SetActive(false);
        _light5.SetActive(true);
    }

    private void UpdateLights()
    {
        if(_nrOfBatteries == 5)
        {
            _light2.SetActive(false);
            _light3.SetActive(false);
            _light4.SetActive(false);
            _light5.SetActive(true);
        }
        if (_nrOfBatteries == 4)
        {
            _light2.SetActive(false);
            _light3.SetActive(false);
            _light4.SetActive(true);
            _light5.SetActive(false);
        }
        if (_nrOfBatteries == 3)
        {
            _light2.SetActive(false);
            _light3.SetActive(true);
            _light4.SetActive(false);
            _light5.SetActive(false);
        }
        if (_nrOfBatteries == 2)
        {
            _soundManager.PlayPlayerLowBattery();
            _light2.SetActive(true);
            _light3.SetActive(false);
            _light4.SetActive(false);
            _light5.SetActive(false);
        }
        if (_nrOfBatteries == 1)
        {
            _light2.SetActive(false);
            _light3.SetActive(false);
            _light4.SetActive(false);
            _light5.SetActive(false);
        }
        if (_nrOfBatteries == 0)
        {
            _light2.SetActive(false);
            _light3.SetActive(false);
            _light4.SetActive(false);
            _light5.SetActive(false);
        }
    }
    
}
