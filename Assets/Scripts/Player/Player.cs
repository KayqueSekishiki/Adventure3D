using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Ebac.StateMachine;
using NaughtyAttributes;

public class Player : Singleton<Player>//, IDamageable
{
    [HideInInspector] public List<Collider> colliders;
    public float speed;
    public float rotSpeed;
    public float gravity = -9.8f;
    public float jumpForce = 15f;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.X;
    public float speedRun = 1.5f;


    private float _vSpeed = 0f;
    private bool _alive = true;
    private Vector3 _externalVelocity;
    private float _externalVelocityDecrease = 1f;

    [Header("Flash")]
    public List<FlashColor> flashColors;


    private Animator _animator;
    private CharacterController _characterController;

    [Header("Life")]
    public HealthBase healthBase;


    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        var colls = GetComponents<Collider>();
        if (colls != null)
        {
            foreach (var c in colls)
            {
                colliders.Add(c);
            }
        }
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
        var dTGravity = gravity * Time.deltaTime;

        _characterController.Move(_externalVelocity * Time.deltaTime + speedVector * Time.deltaTime);

        if (_externalVelocity.y > 0)
        {
            _externalVelocity.y -= dTGravity;
            if (_externalVelocity.y < 0)
            {
                _externalVelocity.y = 0;
            }
        }

        int reverseDirection = _externalVelocity.x > 0 ? -1 : 1;
        float decrease = _externalVelocityDecrease * Time.deltaTime * reverseDirection;
        float abs = Mathf.Abs(_externalVelocity.x);

        if (abs < decrease && abs > 0.001f)
        {
            _externalVelocity.x = 0;
        }
        else
        {
            _externalVelocity.x += decrease;
        }

        reverseDirection = _externalVelocity.z > 0 ? -1 : 1;
        decrease = _externalVelocityDecrease * Time.deltaTime * reverseDirection;
        abs = Mathf.Abs(_externalVelocity.z);

        if (abs < decrease && abs > 0.001f)
        {
            _externalVelocity.z = 0;
        }
        else
        {
            _externalVelocity.z += decrease;
        }


        _animator.SetBool("Run", isWalking);
    }

    public void AddExternalVelocity(float velocity, Vector3 direction)
    {
        direction.Normalize();
        _externalVelocity += direction * velocity;
    }


    #region LIFE
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
    }

    public void Damage(float damage, Vector3 dir)
    {
        // Damage(damage);
    }

    private void OnKill(HealthBase h)
    {
        if (_alive)
        {
            _alive = false;
            _animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 3f);
        }
    }

    private void Revive()
    {
        healthBase.ResetLife();
        _alive = true;
        _animator.SetTrigger("Revive");
        Invoke(nameof(TurnOnColliders), 0.1f);
        Respawn();
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }
    #endregion
}

