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

        if (player != null)
        {
            _playerHealth = player.GetComponent<Health>();
            _playerShootingBehaviour = player.GetComponent<ShootingBehaviour>();
            _playerBatteryHolder = player.GetComponent<BatteryHolder>();
            _playerKeyHolder = player.GetComponent<KeyHolder>();
        }
        _GreenKey.transform.localScale = Vector3.zero;
        _RedKey.transform.localScale = Vector3.zero;
        _BlueKey.transform.localScale = Vector3.zero;
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
            _healthBar.transform.localScale = new Vector3((_playerHealth.HealthPercentage/5 * _playerBatteryHolder.currentBatteryNumber), 1.0f, 1.0f);
        }

        //ammo
        if (_primaryAmmo && _playerShootingBehaviour)
        {
            _primaryAmmo.text = _playerShootingBehaviour.PrimaryWeaponAmmo.ToString();
            _numberBatteries.text = _playerBatteryHolder.currentBatteryNumber.ToString();
        }

        if(_playerKeyHolder.ContainsKey(Key.KeyType.Green))
        {
            _GreenKey.transform.localScale = Vector3.one;
        }
        else
        {
            _GreenKey.transform.localScale = Vector3.zero;
        }

        if (_playerKeyHolder.ContainsKey(Key.KeyType.Red))
        {
            _RedKey.transform.localScale = Vector3.one;
        }
        else
        {
            _RedKey.transform.localScale = Vector3.zero;
        }
        if (_playerKeyHolder.ContainsKey(Key.KeyType.Blue))
        {
            _BlueKey.transform.localScale = Vector3.one;
        }
        else
        {
            _BlueKey.transform.localScale = Vector3.zero;
        }
    }
}
