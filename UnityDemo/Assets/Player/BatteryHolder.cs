using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class BatteryHolder : MonoBehaviour
{
    private int _currentBatteryNumber; //number of batteries
    private int _currentAmmo; //number of bullets
    private ShootingBehaviour _playerShootingBehaviour = null; 
    private bool _IsPlayerDead;

    private void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>(); //get player from the game

        if (player != null) //if exists
        {
            _playerShootingBehaviour = player.GetComponent<ShootingBehaviour>(); //then get his shooting behavious
            _currentAmmo = _playerShootingBehaviour.currentAmmmo; //from their its current ammo
            _IsPlayerDead = false; //set that he is not dead
            _currentBatteryNumber = 0; //and the current battery (at start) is set to 0
        }
    }

    private void Update()
    {
        UpdateNumberOfBatteries();
    }

    private void UpdateNumberOfBatteries()
    {
        _currentAmmo = _playerShootingBehaviour.currentAmmmo; //constantly update the ammount of ammo the player has
        //the ammount of ammo available will be a factor when deciding how much energy a player has too
        if (_currentAmmo > 40)
        {
            _currentBatteryNumber = 5;
        }
        if (_currentAmmo > 30 && _currentAmmo <= 40)
        {
            _currentBatteryNumber = 4;
        }
        if (_currentAmmo > 20 && _currentAmmo <= 30)
        {
            _currentBatteryNumber = 3;
        }
        if (_currentAmmo > 10 && _currentAmmo <= 20)
        {
            _currentBatteryNumber = 2;
        }
        if (_currentAmmo > 0 && _currentAmmo <= 10)
        {
            _currentBatteryNumber = 1;
        }
        if (_currentAmmo == 0 )
        {
            _currentBatteryNumber = 0;
        }
    }

    public int currentBatteryNumber //to get battery number
    {
        get
        {
            if (_playerShootingBehaviour) //if shooting behaviour exists
                return _currentBatteryNumber;
            else
                return 0;
        }
    }

    public void RemoveBattery() //called when player gets hit
    {
        if(_currentBatteryNumber > 0) //if the current battery is higher than 0, remove one
        {
            _currentBatteryNumber =_currentBatteryNumber - 1;
        }
        if(currentBatteryNumber <= 0) //else, its because now its 0 or lower, and the player is dead
        {
            _IsPlayerDead = true;
            
        }
    }

    public bool GetPlayerState() //gets the player state
    {
        return _IsPlayerDead;
    }

}
