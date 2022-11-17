using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //include the UnityEngine.UI namespace
    [SerializeField] Image _healthBar = null;
    [SerializeField] Image _GreenKey = null;
    [SerializeField] Image _RedKey = null;
    [SerializeField] Image _BlueKey = null;


    [SerializeField] Text _primaryAmmo = null;
    [SerializeField] Text _numberBatteries = null;


    private Health _playerHealth = null;
    private ShootingBehaviour _playerShootingBehaviour = null;
    private BatteryHolder _playerBatteryHolder = null;
    private KeyHolder _playerKeyHolder = null;


    private void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player != null) //if player exists
        {
            _playerHealth = player.GetComponent<Health>();
            _playerShootingBehaviour = player.GetComponent<ShootingBehaviour>();
            _playerBatteryHolder = player.GetComponent<BatteryHolder>();
            _playerKeyHolder = player.GetComponent<KeyHolder>();
        }
        _GreenKey.transform.localScale = Vector3.zero; //key HUD not visible
        _RedKey.transform.localScale = Vector3.zero; //key HUD not visible
        _BlueKey.transform.localScale = Vector3.zero; //key HUD not visible
    }

    private void Update()
    {
        SyncData();
    }

    void SyncData()
    {
        //health
        if (_healthBar && _playerHealth)
        {
            _healthBar.transform.localScale = new Vector3((_playerHealth.HealthPercentage/5 * _playerBatteryHolder.currentBatteryNumber), 1.0f, 1.0f); //health bar will be dependent on player battery
        }

        //ammo
        if (_primaryAmmo && _playerShootingBehaviour) //showcases to the user the amount of ammo
        {
            _primaryAmmo.text = _playerShootingBehaviour.PrimaryWeaponAmmo.ToString();
            _numberBatteries.text = _playerBatteryHolder.currentBatteryNumber.ToString();
        }

        if(_playerKeyHolder.ContainsKey(Key.KeyType.Green)) //if player has key
        {
            _GreenKey.transform.localScale = Vector3.one; //then set its size to its original one
        }
        else
        {
            _GreenKey.transform.localScale = Vector3.zero; //else size stays zero
        }

        if (_playerKeyHolder.ContainsKey(Key.KeyType.Red)) //if player has key
        {
            _RedKey.transform.localScale = Vector3.one;  //then set its size to its original one
        }
        else
        {
            _RedKey.transform.localScale = Vector3.zero; //else size stays zero
        }
        if (_playerKeyHolder.ContainsKey(Key.KeyType.Blue)) //if player has key
        {
            _BlueKey.transform.localScale = Vector3.one; //then set its size to its original one
        }
        else
        {
            _BlueKey.transform.localScale = Vector3.zero;//else size stays zero
        }
    }
}
