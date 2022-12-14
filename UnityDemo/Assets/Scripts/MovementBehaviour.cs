using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float _movementSpeed = 1.0f;

    protected Rigidbody _rigidbody;

    protected Vector3 _desiredMovementDirection = Vector3.zero;
    public Vector3 DesiredMovementDirection
    {
        get { return _desiredMovementDirection; }
        set { _desiredMovementDirection = value; }
    }

    protected Vector3 _desiredLookatPoint = Vector3.zero;
    public Vector3 DesiredLookatPoint
    {
        get { return _desiredLookatPoint; }
        set { _desiredLookatPoint = value; }
    }

    protected GameObject _target;
    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }

    }

    public float Speed
    {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    public void SetSpeed(float speed)
    {
        _movementSpeed = speed;
    }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        HandleRotation();
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
    }

    protected virtual void HandleMovement()
    {
        Vector3 movement = _desiredMovementDirection.normalized;
        movement *= _movementSpeed;
        _rigidbody.velocity = movement;
    }

    protected virtual void HandleRotation()
    {
        transform.LookAt(_desiredLookatPoint, Vector3.up);
    }
}
