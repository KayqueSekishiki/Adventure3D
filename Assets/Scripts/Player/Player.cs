using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Ebac.StateMachine;
using NaughtyAttributes;

public class Player : Singleton<Player>
{
    public float speed;
    public float rotSpeed;
    public float gravity = -9.8f;

    private float _vSpeed = 0f;



    private CharacterController _characterController;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime, 0);
        var speedVector = transform.forward * Input.GetAxis("Vertical") * speed;

        _vSpeed += gravity * Time.deltaTime;
        speedVector.y = _vSpeed;

        _characterController.Move(speedVector * Time.deltaTime);
    }
}