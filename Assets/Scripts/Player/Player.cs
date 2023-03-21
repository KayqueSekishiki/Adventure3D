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
    public float jumpForce = 15f;

    private float _vSpeed = 0f;


    private Animator _animator;
    private CharacterController _characterController;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (_characterController.isGrounded)
        {
            _vSpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _vSpeed = jumpForce;
            }
        }
       


        _vSpeed += gravity * Time.deltaTime;
        speedVector.y = _vSpeed;

        _characterController.Move(speedVector * Time.deltaTime);

        _animator.SetBool("Run", inputAxisVertical != 0);

    }




  

}

