using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private GameObject _light1; //the round light around the player
    private GameObject _light2; //the light to see foward
    private GameObject _light3; //the light to see foward
    private GameObject _light4; //the light to see foward
    private GameObject _light5; //the light to see foward

    BatteryHolder _playerBatteries = null;

    private int _nrOfBatteries = 5; //max number of batteries in fact
    private SoundManager _soundManager = null; // from here sounds will be played

    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>(); //gets the player from the game

        if (player != null) //if exists
        {
            _playerBatteries = player.GetComponent<BatteryHolder>();//then gets its battery holder
        }
        SoundManager soundManager = FindObjectOfType<SoundManager>(); //get sound manager from game
        if (soundManager) //if exists
        {
            _soundManager = soundManager; //hold it
        }

        InitializeLights();
        _nrOfBatteries = 0; //initial number of battreies is 0;
    }

    // Update is called once per frame
    void Update()
    {
        _nrOfBatteries = _playerBatteries.currentBatteryNumber; //updates
        UpdateLights();
    }

    private void InitializeLights() //they start all off excpet for the one around the p[layer
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

    private void UpdateLights() //updates the state of the lights
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
            _soundManager.PlayPlayerLowBattery(); //plays a sound when battery level satrts to go down
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
