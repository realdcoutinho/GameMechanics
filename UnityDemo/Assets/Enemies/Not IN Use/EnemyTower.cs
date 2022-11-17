using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : BasicCharacter
{
    private GameObject _playerTarget = null;
    [SerializeField] private float _attackRange = 5.0f;
    //private Vector3 _desiredLookatPoint;


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

    private void InitializeLights()
    {
        _lightTop = GameObject.Find("Top Light");
        _lightFront = GameObject.Find("Front Light");
        _lightTop.SetActive(false);
        _lightFront.SetActive(false);
    }

    private void UpdateLights()
    {
        if ((transform.position - _playerPosition).sqrMagnitude < _attackRange * _attackRange)
        {
            if (!_lightsOn)
            {
                _lightTop.SetActive(true);
                _lightFront.SetActive(true);
                _lightsOn = true;
                Debug.Log("Lights are on");
            }
        }
        if ((transform.position - _playerPosition).sqrMagnitude > _attackRange * _attackRange)
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

    private void Update()
    {
        UpdateLights();
        HandleMovement();

    }
    void HandleMovement()
    {
        if (_movementBehaviour == null)
            return;

        _playerPosition = _playerTarget.transform.position;
       // _movementBehaviour.DesiredLookatPoint = _playerPosition;
    }

    void HandleAttacking()
    {
        if (_shootingBehaviour == null) return;

        if (_playerTarget == null) return;
    }


}
