using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _player = null;
    [SerializeField]
    private float _followSpeed = 5.0f;


    private void FixedUpdate()
    {
        if(_player != null )
        {
            transform.position = Vector3.Lerp(transform.position, _player.transform.position, _followSpeed * Time.deltaTime);
        }
    }



    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
