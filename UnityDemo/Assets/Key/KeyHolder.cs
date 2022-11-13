//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Key;


public class KeyHolder : MonoBehaviour
{
    private List<Key.KeyType> keyList;

    private SoundManager _soundManager = null;

    private void Awake()
    {
        keyList = new List<Key.KeyType>();
    }
    private void Start()
    {
        SoundManager soundManager = FindObjectOfType<SoundManager>();
        if (soundManager)
        {
            _soundManager = soundManager;
        }
    }

    public void AddKey(Key.KeyType keyType)
    {
        keyList.Add(keyType);
        _soundManager.PlayKeysPickUp();
    }

    public void RemoveKey(Key.KeyType keyType)
    {
        keyList.Remove(keyType);
    }

    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Key key = collider.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }

        KeyDoor keyDoor = collider.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                keyDoor.OpenDoor();
                RemoveKey(keyDoor.GetKeyType());
            }
        }
    }







}
