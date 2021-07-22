using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _charControl;
    [SerializeField]  private float _speed = 9;
    [SerializeField] private float _gravity = 1;
    [SerializeField] private Vector3 _direction, _velocity;
    [SerializeField] private float _jumpForce = 20;
    [SerializeField] private float _yVelocity;
    private Animator _anim;
    private bool _jumping;
    [SerializeField] private Vector3 _endPos;
    private bool _onLedge;
    private bool _standingJump;
 



    
    void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0; // VSync must be disabled.
        Application.targetFrameRate = 60;
#endif
    }
    


    // Start is called before the first frame update
    void Start()
    {
        _charControl = GetComponent<CharacterController>();
        if (_charControl == null)
        {
            Debug.LogError("Player:: CharacterController is null");
        }
        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Player:: Animator is null");
        }
    }

//    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _onLedge)
        {
            _anim.SetTrigger("Climb");
            _anim.SetBool("Jump", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _charControl.isGrounded && _velocity.z == 0)
        {
            _anim.SetBool("Jump",true);

            Debug.Log("Jump");

        }
     


        if (_charControl.isGrounded && _charControl.enabled)
        {
            
            if (_jumping)
            {
                _jumping = false;
                _anim.SetBool("Jump", _jumping);
                // StartCoroutine(JumpCoolDown());
            
            }            
            
            if (Input.GetKeyDown(KeyCode.Space)&& _velocity.z != 0)
            {
                Jumping();

            }
        }
    }
//    */
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_charControl.enabled)
        {
            CharMovement();
        }
    }

    private void CharMovement()
    {
        float _hAxis = Input.GetAxisRaw("Horizontal");

        if (_charControl.isGrounded && !_standingJump)
        {
            _velocity = Vector3.zero;
            /*
            if (_jumping && _timer < 0)
            {
                _jumping = false;
                _anim.SetBool("Jump", _jumping);
                // StartCoroutine(JumpCoolDown());
                _timer = 0.2f;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
            */
            _direction = new Vector3(0, 0, _hAxis);
            _velocity = _direction * _speed;
            //my rotation methods
            /*
            if (_hAxis < -0.1f)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if( _hAxis > 0.1f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            */

            //jonathans methods for rotating
            if (_hAxis != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180; // if direction.z is greater then 0 = 0 else 180;
                transform.localEulerAngles = facing;
            }



            _anim.SetFloat("Speed", Mathf.Abs(_hAxis));
            /*  
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpForce;

                _jumping = true;
                _anim.SetBool("Jump", _jumping);
            } 
 //         */
        }
        else
        {
            _yVelocity -= _gravity;
        }
        _velocity.y = _yVelocity;
        _charControl.Move(_velocity * Time.deltaTime);
    }

    private void Jumping()
    {
        _yVelocity = _jumpForce;
        _jumping = true;
        //_anim.SetTrigger("Jumping");
        _anim.SetBool("Jump", _jumping);
    }
    public void StandingJump(bool state)
    {
 
        _standingJump = state;
        //_anim.SetTrigger("Jumping");
        _anim.SetBool("Jump", state);
    }

    public void GrabLedge(Transform Hands, Transform EndPos)
    {
        _yVelocity = 0;
        _charControl.enabled = false;
       // _gravity = 0;
        _anim.SetTrigger("Ledge");
        _anim.SetFloat("Speed", 0f);
        _endPos = EndPos.position;
        //updatedate the pos.
        transform.position = Hands.position;
        _onLedge = true;
    }

    public void AfterClimb()
    {
        transform.position = _endPos;
        _charControl.enabled = true;
    }

    IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(0.15f);
        _jumping = false;
        _anim.SetBool("Jump", _jumping);
        yield break;
        

    }
}
