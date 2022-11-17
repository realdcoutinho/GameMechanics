using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShootingBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _primaryGunTemplate = null;

    [SerializeField]
    private GameObject _primarySocket = null;

    private BasicWeapon _primaryGun = null;

    public int PrimaryWeaponAmmo
    {
        get
        {
            if (_primaryGun)
                return _primaryGun.CurrentAmmo;
            else
                return 0;
        }
    }




    private void Awake()
    {
        //spawn guns
        if (_primaryGunTemplate != null && _primarySocket != null)
        {
            var gunObject = Instantiate(_primaryGunTemplate, _primarySocket.transform, true);
            gunObject.transform.localPosition = Vector3.zero;
            gunObject.transform.localRotation = Quaternion.identity;
            _primaryGun = gunObject.GetComponent<BasicWeapon>();
        }
    }

    void Update()
    {
    }

    public void PrimaryFire()
    {
        if (_primaryGun != null)
            _primaryGun.Fire();
    }



    public void Reload()
    {
        if (_primaryGun != null)
            _primaryGun.Reload();
    }

    public void ReloadHalf()
    {
        if (_primaryGun != null)
            _primaryGun.ReloadHalf();
    }

    public int clipSize
    {
        get
        {
            return _primaryGun.ClipSize;
        }

    }

    public int currentAmmmo //gets the current ammo
    {
        get
        {
            return _primaryGun.CurrentAmmo;
        }

    }

    public void SetCurrentAmmo(int ammo) //se4ts ammo depending on hits and charger pickups
    {
        _primaryGun.SetCurrentAmmo(ammo);
    }

    public void GotHit() // gets hit
    {
        _primaryGun.GotHit();
    }

}
