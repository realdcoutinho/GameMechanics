using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.KeyType keyType;


    private SoundManager _soundManager = null;
    private void Start()
    {
        SoundManager soundManager = FindObjectOfType<SoundManager>(); //if sound manager exist
        if (soundManager)
        {
            _soundManager = soundManager; //get sounds
        }
    }

    public Key.KeyType GetKeyType() //which type is the door(?)
    {
        return keyType;
    }

    public void OpenDoor() //to open door
    {
        _soundManager.PlayKeysDoorOpen(); // play door open sound
        gameObject.SetActive(false); // set the game object no longer visable.
    }

}
