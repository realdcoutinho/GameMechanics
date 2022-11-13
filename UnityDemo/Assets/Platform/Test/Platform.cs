using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject Player;
    public PlayerCharacter Character;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject  == Character)
        {
            Character.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Character)
        {
            Character.transform.parent = null;
        }
    }


}
