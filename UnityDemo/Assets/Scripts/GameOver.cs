using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField]
    private GameObject _player = null;

    void Update()
    {
        if (_player == null)
            TriggerGameOver();
    }

    void TriggerGameOver()
    {
        //include the namespace UnityEngine.SceneManagmenet
        SceneManager.LoadScene(0);
    }
}
