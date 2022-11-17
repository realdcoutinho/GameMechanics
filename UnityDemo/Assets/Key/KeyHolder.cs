//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Key;


public class KeyHolder : MonoBehaviour //holds the keys with the player
{
    private List<Key.KeyType> keyList;

    private SoundManager _soundManager = null;

    private void Awake()
    {
        keyList = new List<Key.KeyType>(); //player has a listy of keys
    }
    private void Start()
    {
        SoundManager soundManager = FindObjectOfType<SoundManager>(); //if sound manager exists
        if (soundManager)
        {
            _soundManager = soundManager; //get sounds
        }
    }

    public void AddKey(Key.KeyType keyType) //adds a keys
    {
        keyList.Add(keyType); //keys keytype and adds to list
        _soundManager.PlayKeysPickUp(); //polays sounds
    }

    public void RemoveKey(Key.KeyType keyType) //key gets used
    {
        keyList.Remove(keyType); //key gets removed from list
    }

    public bool ContainsKey(Key.KeyType keyType) //return which keys it contains in the list
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter(Collider collider) //if it collides...
    {
        Key key = collider.GetComponent<Key>(); //... with a keys
        if (key != null) 
        {
            AddKey(key.GetKeyType()); //adds the key to the keytype
            Destroy(key.gameObject); //destroys the key
        }

        KeyDoor keyDoor = collider.GetComponent<KeyDoor>(); //...with a door
        if (keyDoor != null)
        {
            if (ContainsKey(keyDoor.GetKeyType())) //if it is the same key type as the door
            {
                keyDoor.OpenDoor(); //opens door
                RemoveKey(keyDoor.GetKeyType()); // removes key
            }
        }
    }







}
