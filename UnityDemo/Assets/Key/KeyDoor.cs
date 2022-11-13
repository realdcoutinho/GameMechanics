using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.KeyType keyType;


    private SoundManager _soundManager = null;
    private void Start()
    {
        SoundManager soundManager = FindObjectOfType<SoundManager>();
        if (soundManager)
        {
            _soundManager = soundManager;
        }
    }

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
        _soundManager.PlayKeysDoorOpen();
        gameObject.SetActive(false);
    }

}
