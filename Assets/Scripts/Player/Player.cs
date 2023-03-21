using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Ebac.StateMachine;
using NaughtyAttributes;

public class Player : Singleton<Player>
{
    public float speed;
    public float jumpForce;
    private Rigidbody _rigidbody;
    private CharacterController _characterController;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var speedVector = transform.forward * Input.GetAxis("Vertical") * speed;

        _characterController.Move(speedVector * Time.deltaTime);
    }

    public void Move()
    {

        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, speed);
    }

    public void Stop()
    {
        _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y);
    }
    public void Jump()
    {
        _rigidbody.velocity = jumpForce * Vector2.up;
    }



}