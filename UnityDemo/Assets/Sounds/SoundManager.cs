using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour // Holds all the sounds played in game.
{
    //enemy
    [SerializeField] private AudioSource _enemyHit;
    [SerializeField] private AudioSource _enemyShooterGun;
    [SerializeField] private AudioSource _enemyTowerGun;
    [SerializeField] private AudioSource _enemyKamikazeBomb;

    //keys and door
    [SerializeField] private AudioSource _keysPickUp;
    [SerializeField] private AudioSource _keysDoorOpen;


    //player
    [SerializeField] private AudioSource _playerShooting;
    [SerializeField] private AudioSource _playerCharging;
    [SerializeField] private AudioSource _playerDead;
    [SerializeField] private AudioSource _playerLowBattery;

    //charger
    [SerializeField] private AudioSource _chargerActivated;

    //Gate
    [SerializeField] private AudioSource _gateOpen;


    //background
    [SerializeField] private AudioSource _backgroundMusic;

    //enemy

    public void PlayEnemyHit()
    {
        _enemyHit.Play();
    }

    public void PlayEnemyShooterGun()
    {
        _playerShooting.Play();
    }

    public void PlayEnemyTowerGun()
    {
       _enemyTowerGun.Play();   
    }

    public void PLayKamikazeBomb()
    {
        _enemyKamikazeBomb.Play();
    }


    //keys and doors
    public void PlayKeysPickUp()
    {
        _keysPickUp.Play();
    }

    public void PlayKeysDoorOpen()
    {
        _keysDoorOpen.Play();
    }

    //player
    public void PlayPlayerShooting()
    {
        _playerShooting.Play();
    }
    public void PlayPlayerCharging()
    {
        _playerCharging.Play(); 
    }

    public void PlayPlayerDead()
    {
        _playerDead.Play();
    }

    public void PlayPlayerLowBattery() 
    { 
        _playerLowBattery.Play();
    }  


    //charger
    public void PlayChargerActivated()
    {
        _chargerActivated.Play();
    }

    public void PlayGateOpen()
    {
        _gateOpen.Play();
    }

    public void BackgroundMusic()
    {
        _backgroundMusic.Play(); 
    }




}
