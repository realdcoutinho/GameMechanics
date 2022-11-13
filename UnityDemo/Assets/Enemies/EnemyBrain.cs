using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : BasicCharacter
{
    private GameObject _playerTarget = null;
    private float _shootRange; //shoot or bomb radius
    private float _seeRange;
    private float _movementRange;
    private float _stopRange;
    private bool _killed;


    private GameObject _lightTop;
    private GameObject _lightFront;
    private bool _lightsOn;
    private Vector3 _playerPosition;

    [SerializeField] public bool _IsShooter = false;
    [SerializeField] public bool _IsTower = false;
    [SerializeField] public bool _IsKamikaze = false;

    private float _elapsedTime;
    private float _timeBetweenShots;

    private SoundManager _soundManager = null;

    private PlayerCharacter _player = null;

    private void Start()
    {
        InitializeLights();
        InitializeVariables();


        _elapsedTime = 0;
        _timeBetweenShots = 0.5f;
        //expensive method, use with caution
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player)
        {
            _playerTarget = player.gameObject;
            _player = player;

        }

        SoundManager soundManager = FindObjectOfType<SoundManager>();
        if (soundManager)
        {
            _soundManager = soundManager;
        }
    }

    private void Update()
    {
        _elapsedTime = _elapsedTime + Time.deltaTime;
        HandleAttacking();
        UpdateLights();
        HandleMovement();
    }

    private void InitializeLights()
    {
        _lightTop = GameObject.Find("Top Light");
        _lightFront = GameObject.Find("Front Light");
        _lightTop.SetActive(false);
        _lightFront.SetActive(false);
    }

    private void InitializeVariables()
    {
        if(_IsTower && !_IsKamikaze && !_IsShooter) //tower
        {
            _shootRange = 11.5f;
            _seeRange = 12.0f;
            _movementRange = 0.0f;
        }
        if (!_IsTower && _IsKamikaze && !_IsShooter) //kamikaze
        {
            _shootRange = 0.9f;
            _seeRange = 10.0f;
            _movementRange = 10.0f;
            _killed = false;
        }
        if (!_IsTower && !_IsKamikaze && _IsShooter) //shooter
        {
            _shootRange = 8.0f;
            _seeRange = 9.0f;
            _movementRange = 9.1f;
            _stopRange = 3.5f;
        }
    }

    void HandleMovement()
    {
        if (_movementBehaviour == null)
            return;

        _playerPosition = _playerTarget.transform.position;
        if(!_IsTower)
        {
            if(_IsShooter)
            {
                if ((transform.position - _playerPosition).sqrMagnitude < _movementRange * _movementRange)
                {
                    if ((transform.position - _playerPosition).sqrMagnitude < _stopRange * _stopRange)
                    {
                        _movementBehaviour.Target = null;
                    }
                    if ((transform.position - _playerPosition).sqrMagnitude > _stopRange * _stopRange)
                    {
                        _movementBehaviour.Target = _playerTarget;
                    }
                }
                if ((transform.position - _playerPosition).sqrMagnitude > _movementRange * _movementRange)
                {
                    _movementBehaviour.Target = null;
                }
            }
            else
            {
                if ((transform.position - _playerPosition).sqrMagnitude < _movementRange * _movementRange)
                {
                    _movementBehaviour.Target = _playerTarget;
                }
                if ((transform.position - _playerPosition).sqrMagnitude > _movementRange * _movementRange)
                {
                    _movementBehaviour.Target = null;
                }
            }

        }
    }

    void HandleAttacking()
    {
        if (_shootingBehaviour == null) return;

        if (_playerTarget == null) return;



        //if we are in range of the player, fire our weapon,
        //use sqr maginitude when comparing ranges as it is more efficient
        if ((transform.position - _playerTarget.transform.position).sqrMagnitude < _shootRange * _shootRange)
        {
            if(_IsKamikaze)
            {

                if (!_killed)
                {
                    _soundManager.PLayKamikazeBomb();
                    _killed = true;
                }

                Kill();
                Invoke(KILL_METHODNAME, 0.2f);
                return;
            }

            if (_elapsedTime > _timeBetweenShots)
            {
                _shootingBehaviour.PrimaryFire();
                _shootingBehaviour.Reload();
                _elapsedTime = 0.0f;
            }
        }
    }

    const string KILL_METHODNAME = "Kill";
    void Kill()
    {
        Destroy(gameObject);
    }

    private void UpdateLights()
    {
        if(gameObject == null) return;
        if ((transform.position - _playerPosition).sqrMagnitude < _seeRange * _seeRange)
        {
            _movementBehaviour.DesiredLookatPoint = _playerPosition;
            if (!_lightsOn)
            {
                _lightTop.SetActive(true);
                _lightFront.SetActive(true);
                _lightsOn = true;
            }
        }
        if ((transform.position - _playerPosition).sqrMagnitude > _seeRange * _seeRange)
        {
            if (_lightsOn)
            {
                _lightTop.SetActive(false);
                _lightFront.SetActive(false);
                _lightsOn = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        BasicProjectile projectile = collider.GetComponent<BasicProjectile>();
        if (projectile != null)
        {
            _soundManager.PlayEnemyHit();
            Destroy(projectile.gameObject);
            Kill();
        }
    }


}
