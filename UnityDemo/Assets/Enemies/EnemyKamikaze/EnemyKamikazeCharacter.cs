using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikazeCharacter : BasicCharacter
{
    private GameObject _playerTarget = null;
    [SerializeField] private float _bombRange = 1.0f;
    [SerializeField] private float _seeRange = 15.0f;
    [SerializeField] private float _movementRange = 10.0f;


    private GameObject _lightTop;
    private GameObject _lightFront;
    private bool _lightsOn;
    Vector3 _playerPosition;

    private void Start()
    {
        //expensive method, use with caution
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player)
        {
            _playerTarget = player.gameObject;
        }
        InitializeLights();
    }



    private void Update()
    {
        UpdateLights();


        HandleMovement();
        HandleAttacking();
    }

    void HandleMovement()
    {
        if (_movementBehaviour == null)
            return;

        _playerPosition = _playerTarget.transform.position;
        if ((transform.position - _playerPosition).sqrMagnitude < _movementRange * _movementRange)
        {

            _movementBehaviour.Target = _playerTarget;
        }
        if ((transform.position - _playerPosition).sqrMagnitude < _movementRange * _movementRange)
        {
            return;
        }


    }

    void HandleAttacking()
    {
        if (_shootingBehaviour == null) return;

        if (_playerTarget == null) return;

        

        //if we are in range of the player, fire our weapon,
        //use sqr maginitude when comparing ranges as it is more efficient
        if ((transform.position - _playerTarget.transform.position).sqrMagnitude < _bombRange * _bombRange)
        {
            _shootingBehaviour.PrimaryFire();

            //this is a kamikaze enemy,
            //when it fires it should destroy itself

            Invoke(KILL_METHODNAME, 0.2f);
        }
    }

    const string KILL_METHODNAME = "Kill";
    void Kill()
    {
        Destroy(gameObject);
    }

    private void InitializeLights()
    {
        _lightTop = GameObject.Find("Top Light");
        _lightFront = GameObject.Find("Front Light");
        _lightTop.SetActive(false);
        _lightFront.SetActive(false);
    }
    private void UpdateLights()
    {
        if ((transform.position - _playerPosition).sqrMagnitude < _seeRange * _seeRange)
        {
            _movementBehaviour.DesiredLookatPoint = _playerPosition;
            if (!_lightsOn)
            {
                _lightTop.SetActive(true);
                _lightFront.SetActive(true);
                _lightsOn = true;
                Debug.Log("Lights are on");
            }
        }
        if ((transform.position - _playerPosition).sqrMagnitude > _seeRange * _seeRange)
        {
            if (_lightsOn)
            {
                _lightTop.SetActive(false);
                _lightFront.SetActive(false);
                _lightsOn = false;
                Debug.Log("Lights are off");
            }
        }
    }
}
