using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _bulletTemplate = null;
    [SerializeField] private int _clipSize = 50;
    [SerializeField] private float _fireRate = 25.0f;
    [SerializeField] private List<Transform> _fireSockets = new List<Transform>();

    private bool _triggerPulled = false;
    private int _currentAmmo = 50;
    private float _fireTimer = 0.0f;

    [SerializeField] private AudioSource _fireSound; 


    public int CurrentAmmo
    {
        get
        {
            return _currentAmmo;
        }
    }

    public void SetCurrentAmmo(int ammo)
    {
        _currentAmmo = ammo;
    }


    public int ClipSize
    {
        get
        {
            return _clipSize;
        }
    }

    private void Awake()
    {
        _currentAmmo = _clipSize;
    }



    //TESTCODE to see if the gun functions


    void Update()
    {
        //handle the countdown of the fire timer
        if (_fireTimer > 0.0f)
            _fireTimer -= Time.deltaTime;

        if (_fireTimer <= 0.0f && _triggerPulled)
            FireProjectile();

        //the trigger will release by itself,
        //if we still are firing, we will receive new fire input
        _triggerPulled = false;
    }

    private void FireProjectile()
    {
        //no ammo, we can't fire
        if (_currentAmmo <= 0)
            return;

        //no bullet to fire
        if (_bulletTemplate == null)
            return;

        //consume a bullet;
        --_currentAmmo;

        for (int i = 0; i < _fireSockets.Count; i++)
        {
           Instantiate(_bulletTemplate, _fireSockets[i].position, _fireSockets[i].rotation);
        }

        //set the time so we can respect the firerate
        _fireTimer += 1.0f / _fireRate;


        if(_fireSound)
        {
            _fireSound.Play(); //play shooting sound. This will depend on who is shooting. Is it the player, the enemy tower or the enemy shooter(?)
        }


    }

    public void Fire()
    {
        _triggerPulled = true;
    }

    public void Reload() //reloads gun
    {
        _currentAmmo = _clipSize;
    }

    public void ReloadHalf() //only reloads half of the ammo
    {
        _currentAmmo += _clipSize/2;
        if( _currentAmmo > _clipSize) //if by adding 25 if gets greater than clip size
        {
            _currentAmmo = _clipSize; //then it will become the same as clip size
        }
    }

    public void GotHit() // player gets hit
    {
        if(_currentAmmo >= 0) //ig current ammo is
        {
            _currentAmmo = _currentAmmo - 10; // then deduct 10 from it
        }
        if (_currentAmmo < 0) //if now its lower then 0, maintain at 0
        {
            _currentAmmo = 0;
        }
    }


}

