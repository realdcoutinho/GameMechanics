using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //include the UnityEngine.UI namespace
    [SerializeField] Image _healthBar = null;
    [SerializeField] Text _primaryAmmo = null;
    [SerializeField] Text _secondaryAmmo = null;

    private Health _playerHealth = null;
    private ShootingBehaviour _playerShootingBehaviour = null;


    private void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player != null)
        {
            _playerHealth = player.GetComponent<Health>();
            _playerShootingBehaviour = player.GetComponent<ShootingBehaviour>();
        }
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
            _healthBar.transform.localScale = new Vector3(_playerHealth.HealthPercentage, 1.0f, 1.0f);
        }

        //ammo
        if (_primaryAmmo && _playerShootingBehaviour)
        {
            _primaryAmmo.text = _playerShootingBehaviour.PrimaryWeaponAmmo.ToString();
        }
        if (_secondaryAmmo && _playerShootingBehaviour)
        {
            _secondaryAmmo.text = _playerShootingBehaviour.SecondaryWeaponAmmo.ToString();
        }
    }
}
