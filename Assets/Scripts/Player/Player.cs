using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Ebac.StateMachine;
using NaughtyAttributes;
using Cloth;

public class Player : Singleton<Player>, IAddExternalVelocity   //, IDamageable
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
    private bool _jumping = false;
    private Vector3 _externalVelocity;
    private bool xExternalValid, yExternalValid, zExternalValid;
    private readonly float _externalVelocityDecrease = 20f;

    [Header("Flash")]
    public List<FlashColor> flashColors;
    public SFXType sfxJump;


    private Animator _animator;
    private CharacterController _characterController;

    [Header("Life")]
    public HealthBase healthBase;

    [Space]
    [SerializeField] private ClothChanger _clothChanger;

    public SOPlayerData playerData;


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
        if (!_alive) return;

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = inputAxisVertical * speed * transform.forward;


        if (_characterController.isGrounded)
        {
            if (_jumping)
            {
                _jumping = false;
                _animator.SetTrigger("Landing");
            }

            _vSpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _vSpeed = jumpForce;

                if (!_jumping)
                {
                    _jumping = true;
                    _animator.SetTrigger("Jump");
                    PlaySFXJump();
                }
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

        if (yExternalValid)
        {
            if (_externalVelocity.y > 0)
            {
                _externalVelocity.y += dTGravity;
                if (_externalVelocity.y < 0)
                {
                    _externalVelocity.y = 0;
                    yExternalValid = false;
                }
            }
        }

        int reverseDirection = _externalVelocity.x > 0 ? -1 : 1;
        float decrease = _externalVelocityDecrease * Time.deltaTime * reverseDirection;
        float abs = Mathf.Abs(_externalVelocity.x);
        if (xExternalValid)
        {
            if (abs < decrease && abs > 0.001f)
            {
                _externalVelocity.x = 0;
                xExternalValid = false;
            }
            else
            {
                _externalVelocity.x += decrease;
            }
        }

        reverseDirection = _externalVelocity.z > 0 ? -1 : 1;
        decrease = _externalVelocityDecrease * Time.deltaTime * reverseDirection;
        abs = Mathf.Abs(_externalVelocity.z);

        if (zExternalValid)
        {
            if (abs < decrease && abs > 0.001f)
            {
                _externalVelocity.z = 0;
                zExternalValid = false;
            }
            else
            {
                _externalVelocity.z += decrease;
            }
        }


        _animator.SetBool("Run", isWalking);
    }

    public void AddExternalVelocity(float velocity, Vector3 direction)
    {
        direction.Normalize();
        _externalVelocity += direction * velocity;
        xExternalValid = yExternalValid = zExternalValid = true;
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        playerData.currentClothType = setup.clothType;
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
        playerData.currentClothType = ClothType.DEFAULT;

    }

    public void ChangeJumpForce(float jumpForce, float duration)
    {
        StartCoroutine(ChangeJumpForceCoroutine(jumpForce, duration));
    }

    IEnumerator ChangeJumpForceCoroutine(float jumpForce, float duration)
    {
        var defaultJumpForce = this.jumpForce;
        this.jumpForce = jumpForce;
        yield return new WaitForSeconds(duration);
        this.jumpForce = defaultJumpForce;
    }

    private void PlaySFXJump()
    {
        SFXPool.Instance.PlaySFX(sfxJump);
    }



    #region LIFE
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
    }

    //public void Damage(float damage, Vector3 dir)
    //{
    //    // Damage(damage);
    //}

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

