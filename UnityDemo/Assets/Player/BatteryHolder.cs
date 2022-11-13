using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryHolder : MonoBehaviour
{
    private int _currentBatteryNumber;
    private int _currentAmmo;
    private ShootingBehaviour _playerShootingBehaviour = null;
    private bool _IsPlayerDead;

    private void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player != null)
        {
            _playerShootingBehaviour = player.GetComponent<ShootingBehaviour>();
            _currentAmmo = _playerShootingBehaviour.currentAmmmo;
            _IsPlayerDead = false;
            _currentBatteryNumber = 0;
        }
    }

    private void Update()
    {
        UpdateNumberOfBatteries();
    }

    private void UpdateNumberOfBatteries()
    {
        _currentAmmo = _playerShootingBehaviour.currentAmmmo;
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

    public int currentBatteryNumber
    {
        get
        {
            if (_playerShootingBehaviour)
                return _currentBatteryNumber;
            else
                return 0;
        }
    }

    public void RemoveBattery()
    {
        if(_currentBatteryNumber > 0)
        {
            Debug.Log("YO");
            _currentBatteryNumber =_currentBatteryNumber - 1;
        }
        if(currentBatteryNumber <= 0)
        {
            _IsPlayerDead = true;
            
        }
    }

    public bool GetPlayerState()
    {
        return _IsPlayerDead;
    }

}
