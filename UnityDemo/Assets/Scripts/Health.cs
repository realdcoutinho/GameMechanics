using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _startHealth = 10;

    private int _currentHealth = 0;

    public float HealthPercentage
    {
        get 
        { 
            return _currentHealth / _startHealth; 
        }
    }

    void Awake()
    {

        _currentHealth = _startHealth;
    }
    public void Damage(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
            Kill();
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
