using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : BasicCharacter
{
    const string MOVEMENT_HORIZONTAL = "MovementHorizontal";
    const string MOVEMENT_VERTICAL = "MovementVertical";
    const string GROUND_LAYER = "Ground";
    const string PRIMARY_FIRE = "PrimaryFire";
    const string SECONDARY_FIRE = "SecondaryFire";
    const string RELOAD = "Reload";

    private Plane _cursorMovementPlane;

    private SoundManager _soundManager = null;
    private BatteryHolder batteryHolder = null;

    public bool _isGameOver;



    protected override void Awake()
    {
        base.Awake();
        _cursorMovementPlane = new Plane(Vector3.up, transform.position);
        batteryHolder = GetComponent<BatteryHolder>(); //gets the battery holder from the player
        _isGameOver = false; //the agme is not over
    }

    private void Start()
    {
        SoundManager soundManager = FindObjectOfType<SoundManager>(); //gets sound manager
        if (soundManager) //if it exists then youi can play sounds
        {
            _soundManager = soundManager;
        }

        _shootingBehaviour.SetCurrentAmmo(0); //sets the ammo at the satrt fo the game to 0;
    }

    private void Update()
    {
        UpdatePlayerState();
        if (!_isGameOver) //if the game is not over, meaning if the player is not dead, then you can continue the updates
        {
            HandleMovementInput(); //handles movement
            HandleFireInput(); //handles fire input
        }
    }

    void HandleMovementInput()
    {
        if (_movementBehaviour == null) //if movement behaviour exists, continnue
            return; 

        //movement
        float horizontalMovement = Input.GetAxis(MOVEMENT_HORIZONTAL);
        float verticalMovement = Input.GetAxis(MOVEMENT_VERTICAL);

        Vector3 movement = horizontalMovement * Vector3.right + verticalMovement * Vector3.forward;
        _movementBehaviour.DesiredMovementDirection = movement;

        //rotation
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 positionOfMouseInWorld = transform.position;

        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 100000.0f, LayerMask.GetMask(GROUND_LAYER)))
        {
            positionOfMouseInWorld = hitInfo.point;
        }
        else
        {
            _cursorMovementPlane.Raycast(mouseRay, out float distance);
            positionOfMouseInWorld = mouseRay.GetPoint(distance);
        }

        _movementBehaviour.DesiredLookatPoint = positionOfMouseInWorld;
    }

    void HandleFireInput()
    {
        if (_shootingBehaviour == null) return;

        //fire
        if (Input.GetAxis(PRIMARY_FIRE) > 0.0f)
            _shootingBehaviour.PrimaryFire();

        //reload
        if (Input.GetAxis(RELOAD) > 0.0f)
            _shootingBehaviour.Reload();
    }

    private void OnTriggerEnter(Collider collider)
    {
        ChargerBattery charger = collider.GetComponent<ChargerBattery>(); //if get collides with a small charger
        if (charger != null)
        {
            _shootingBehaviour.ReloadHalf(); //reaload only half
            Destroy(charger.gameObject); //and destroy the object charger
        }

        ChargingStation station = collider.GetComponent<ChargingStation>(); //if it collides with a charging station
        if (station != null)
        {
            if(station.GetChargingMode() == true) //if the charging station mode is on
            {
                station.SetChargeMode(); //set the charging station mode to false
                _shootingBehaviour.Reload(); //fully reload the player
                _soundManager.PlayPlayerCharging();  //play the sound of the player charging its batteries
            }
        }

        Gate gate = collider.GetComponent<Gate>(); //if it collides (with the right side) of the gate, 
        if (gate != null)
        {
            _soundManager.PlayGateOpen(); //play sound to open gate
            Destroy(gate.gameObject); //destroy gate
        }
    }

    public void GotHit() //player gets hit
    {
        batteryHolder.RemoveBattery(); //remove battery
        _shootingBehaviour.GotHit(); //tell shooting behaviour (which will then remove ammo)
    }
    
    public void UpdatePlayerState() //game is over, or player is dead according the the number of batteries left in his arsenal
    {
        _isGameOver = batteryHolder.GetPlayerState(); 
    }

    public bool IsGameOver()
    {
        return _isGameOver; 
    }


}
