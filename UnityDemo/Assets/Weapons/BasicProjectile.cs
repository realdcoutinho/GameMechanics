using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 30.0f;

    [SerializeField]
    private float _lifeTime = 10.0f;

    [SerializeField]
    private int _damage = 5;


    private float _hitRange = 0.7f;


    private PlayerCharacter _player = null;

    private void Awake()
    {
        Invoke(KILL_METHODNAME, _lifeTime);

        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player)
        {
            //_player = player.gameObject;
            _player = player;
        }
    }

    void FixedUpdate()
    {

        if (!WallDetection())
            transform.position += transform.forward * Time.deltaTime * _speed;
    }

    //This cannot be defiend const as it can only apply to a field which is known at compile-time. WHich is not the case for an array, so doing static readonly, which means it can serve a very similar role
    static readonly string[] RAYCAST_MASK = { "StaticLevel", "DynamicLevel" };
    bool WallDetection()
    {
        Ray collisionRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(collisionRay, Time.deltaTime * _speed, LayerMask.GetMask(RAYCAST_MASK)))
        {
            Kill();
            return true;
        }

        return false;
    }


    const string KILL_METHODNAME = "Kill";
    void Kill()
    {
        Destroy(gameObject);
    }

    const string FRIENDLY_TAG = "Friendly";
    const string ENEMY_TAG = "Enemy";

    private void OnTriggerEnter(Collider other)
    {


        //make sure we only hit friendly or enemies
        if (other.tag != FRIENDLY_TAG && other.tag != ENEMY_TAG)
            return;

        //only hit the opposing team
        if (other.tag == tag)
            return;

        Health otherHealth = other.GetComponent<Health>();

        if (otherHealth != null)
        {
            otherHealth.Damage(_damage);
            Kill();
        }
    }

    private void Update()
    {
        PlayerHit();
    }

    private void PlayerHit() 
    {
        if (gameObject == null) return; //if bullet exists
        if ((transform.position - _player.transform.position).sqrMagnitude < _hitRange * _hitRange) // if player is in range
        {
            Destroy(gameObject); //destroy projectile
            _player.GotHit(); //get player hit
        }
    }
}
