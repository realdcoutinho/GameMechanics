using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBrain : BasicCharacter //controlls all enemies from this script
{
    private GameObject _playerTarget = null;
    private float _shootRange; //shoot or bomb radius
    private float _seeRange; //how far it can see
    private float _movementRange; //when it satrts to move
    private float _stopRange; //when it will stop moving
    private bool _killed; // is killed


    private GameObject _lightTop; //they have a top light
    private GameObject _lightFront; //and a front light
    private bool _lightsOn; //and lights start off
    private Vector3 _playerPosition; //they also have player position


    //which type of enemy are they?
    [SerializeField] public bool _IsShooter = false; //are they shooter?
    [SerializeField] public bool _IsTower = false; //are they towers?
    [SerializeField] public bool _IsKamikaze = false; //are they kamikaze bombers?
    //depending on thier type, their behaviour changes with it


    private float _elapsedTime; //has time plassed
    private float _timeBetweenShots; //time to reload before each shot

    private SoundManager _soundManager = null; //holds sounds

    private PlayerCharacter _player = null; //holds player

    private void Start()
    {
        InitializeLights();  //controls lights
        InitializeVariables(); //initialize variables
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
        //expensive method, use with caution
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>(); //gets player
        if (player) //if exists
        {
            _playerTarget = player.gameObject;
            _player = player;

        }

        SoundManager soundManager = FindObjectOfType<SoundManager>(); //hols sounds
        if (soundManager) //if exists
        {
            _soundManager = soundManager;
        }


        _elapsedTime = 0;
        _timeBetweenShots = 0.5f; //time between shots
        //variables have been explained
        if (_IsTower && !_IsKamikaze && !_IsShooter) //tower
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
        if (_movementBehaviour == null) //if movement exists, then continue
            return;

        _playerPosition = _playerTarget.transform.position; //get the current player position
        
        
        if(!_IsTower) //first we check for towers. towers dont move. if its not a tower we can handle the in game movement
        {
            if(_IsShooter)//okay, is it a shooter?
            {
                if ((transform.position - _playerPosition).sqrMagnitude < _movementRange * _movementRange) //if so, we need to check if the player is in range to move
                {
                    //alright he is, but....
                    if ((transform.position - _playerPosition).sqrMagnitude < _stopRange * _stopRange) //is he to close? if he is, stop all movement. otherwise the enemy will drag the player
                    {
                        _movementBehaviour.Target = null;
                    }
                    if ((transform.position - _playerPosition).sqrMagnitude > _stopRange * _stopRange) //he is close enought to move, but not close enought to bump against? then keep moving
                    {
                        _movementBehaviour.Target = _playerTarget;
                    }
                }
                if ((transform.position - _playerPosition).sqrMagnitude > _movementRange * _movementRange) //if he is not even in range to move, dont move, there is no target
                {
                    _movementBehaviour.Target = null;
                }
            }
            else //if he is not a shotter and he is not a tower, he must be a kamikazee
            {
                if ((transform.position - _playerPosition).sqrMagnitude < _movementRange * _movementRange) //is he in range to move?
                {
                    _movementBehaviour.Target = _playerTarget; // then  move until you hit him
                }
                if ((transform.position - _playerPosition).sqrMagnitude > _movementRange * _movementRange) //else
                {
                    _movementBehaviour.Target = null; // dont move
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
        if ((transform.position - _playerTarget.transform.position).sqrMagnitude < _shootRange * _shootRange) // is player in range to shoot?
        {
            if(_IsKamikaze) //is enemy a kamikazze?
            {

                if (!_killed) // if kamikaze is still around
                {
                    _player.GotHit(); //player gets hit
                    _soundManager.PLayKamikazeBomb(); //play bomb sound
                    _killed = true; //and gets killed
                }

                Kill(); //kills the kamikaze
                Invoke(KILL_METHODNAME, 0.2f);
                return; //ends here
            }

            if (_elapsedTime > _timeBetweenShots) // he if he is ahooter or a tower, he damages the player with shots. if he can shoot
            {
                _shootingBehaviour.PrimaryFire(); // do primary fire
                _shootingBehaviour.Reload(); //realod
                _elapsedTime = 0.0f; // and set time back to 0
            }
        }
    }

    const string KILL_METHODNAME = "Kill";
    void Kill()
    {
        Destroy(gameObject); //destroys object
    }

    private void UpdateLights() //updates lights
    {
        if(gameObject == null) return; //if it exists
        if ((transform.position - _playerPosition).sqrMagnitude < _seeRange * _seeRange) //is player in seeing range?
        {//if so
            _movementBehaviour.DesiredLookatPoint = _playerPosition; //the player because the point the enemies have to look to, so they can shoot properly
            if (!_lightsOn)//if lights were off, the you can turn them on
            {
                _lightTop.SetActive(true);
                _lightFront.SetActive(true);
                _lightsOn = true; //now they are on
            }
        }
        if ((transform.position - _playerPosition).sqrMagnitude > _seeRange * _seeRange) // the player is not in seeing radious
        {
            if (_lightsOn) //were lights on?
            {
                _lightTop.SetActive(false);
                _lightFront.SetActive(false);
                _lightsOn = false; //now they are off
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        BasicProjectile projectile = collider.GetComponent<BasicProjectile>(); //gets projectyile
        if (projectile != null) //if collides with projectile
        {
            _soundManager.PlayEnemyHit(); //plays dead sound
            Destroy(projectile.gameObject); //destoys projectile
            Kill(); // kills enemy
        }
    }


}
