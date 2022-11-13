using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacter _player = null;

    private void Awake()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player != null)
        {
            _player = player.GetComponent<PlayerCharacter>();
        }
    }
    void Update()
    {
       
        if(_player.IsGameOver())
        {
            TriggerGameOver();
            //Invoke(GAMEOVER_METHODNAME, 5.0f);
        }
        
    }

    const string GAMEOVER_METHODNAME = "TriggerGameOver";


    void TriggerGameOver()
    {
        //include the namespace UnityEngine.SceneManagmenet
        SceneManager.LoadScene(0);
    }
}
