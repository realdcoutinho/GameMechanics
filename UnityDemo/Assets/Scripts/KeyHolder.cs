//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static class Key

public class KeyHolder : MonoBehaviour
{
    private List<Key.KeyType> keyList;


    private void Awake()
    {
        keyList = new ist<Key.KeyType>();
    }

    public void AddKey(Key.KeyType keyType)
    {
        keyList.Add(keyType);
    }

    public void RemoveKey(Key.KeyType keyType)
    {
        keyList.Remove(keyType);
    }

    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList.ContainsKey(keyType);
    }
}
