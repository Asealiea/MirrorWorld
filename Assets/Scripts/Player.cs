using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

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
    private bool _roll;
    private int _coins;
    private int _lives = 3;
    [SerializeField] private Transform _ogPos;



    
    void Awake()
    {
        QualitySettings.vSyncCount = 0; 
        Application.targetFrameRate = 60;
    }
    


    // Start is called before the first frame update
    void Start()
    {
        _coins = 0;
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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _onLedge)
        {
            _anim.SetTrigger("Climb");
            _anim.SetBool("Jump", false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false; // makes it stop while in test play
#else
		    Application.Quit();// quits the game (only works after it's been built)
#endif

        }




        if (_charControl.isGrounded && _charControl.enabled)
        {
            
            if (_jumping)
            {
                _jumping = false;
                _anim.SetBool("Jump", _jumping);
            
            }            
            
            if (Input.GetKeyDown(KeyCode.Space)&& _velocity.z != 0 && !_roll)
            {
                Jumping();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _velocity.z == 0)
            {
                _anim.SetBool("Jump", true);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && Mathf.Abs(_velocity.z) > 0)
            {
                _roll = true;
                _anim.SetBool("Roll",true);
            }
     
        }
    }

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

        if (_charControl.isGrounded && !_standingJump && !_roll)
        {
           

            _direction = new Vector3(0, 0, _hAxis);
            _velocity = _direction * _speed;

            if (_hAxis != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180; // if direction.z is greater then 0 = 0 else 180;
                transform.localEulerAngles = facing;
            }

            _anim.SetFloat("Speed", Mathf.Abs(_hAxis));
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
        _anim.SetBool("Jump", _jumping);
    }
        //_anim.SetTrigger("Jumping");
    public void StandingJump(bool state)
    {
 
        _standingJump = state;
        //_anim.SetTrigger("Jumping");
        _anim.SetBool("Jump", state);
        _yVelocity = 0;
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
    }

    public void Standing()
    {
        _charControl.enabled = true;
    }

    public void StartingRoll(Vector3 Center, float Height)
    {
        _roll = true;
        _charControl.center = Center;
        _charControl.height = Height;
    }

    public void AfterRoll(Vector3 Center, float Height)
    {
        _roll = false;
        _anim.SetBool("Roll", false);
        _charControl.center = Center;
        _charControl.height = Height;
    }

    public void UpdateCoins()
    {
        _coins++;
        UIManager.Instance.UpdateUICoin(_coins);
    }

    public void UpdateLives()
    {
        _lives--;
        _charControl.enabled = false;
        transform.position = _ogPos.position;
        UIManager.Instance.UpdateLives(_lives);
        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
        _charControl.enabled = true;
    }

 
}
