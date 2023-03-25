using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Ebac.StateMachine;
using NaughtyAttributes;

public class Player : Singleton<Player>//, IDamageable
{
    public float speed;
    public float rotSpeed;
    public float gravity = -9.8f;
    public float jumpForce = 15f;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.X;
    public float speedRun = 1.5f;


    private float _vSpeed = 0f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    public HealthBase healthBase;

    private Animator _animator;
    private CharacterController _characterController;


    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();

        healthBase.OnDamage += Damage;
    }

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
        var speedVector = inputAxisVertical * speed * transform.forward;

        if (_characterController.isGrounded)
        {
            _vSpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _vSpeed = jumpForce;
            }
        }

        var isWalking = inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                _animator.speed = speedRun;
            }
            else
            {
                _animator.speed = 1;

            }
        }


        _vSpeed += gravity * Time.deltaTime;
        speedVector.y = _vSpeed;

        _characterController.Move(speedVector * Time.deltaTime);
        _animator.SetBool("Run", isWalking);
    }


    #region LIFE
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
    }

    public void Damage(float damage, Vector3 dir)
    {
       // Damage(damage);
    }
    #endregion
}

