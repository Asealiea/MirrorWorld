using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _gravity = 1;
    [SerializeField] private float _speed = 5;
    private Vector3 _velocity;
    private Vector3 _direction;
    private float _yVelocity;
    private float _jumpForce = 20f;
    private CharacterController _controller;
    private float _hMovement;
    private bool _doubleJump;
    private int _coinCount;
    [SerializeField] private int _lives = 3;
    [SerializeField] private Transform _spawnPoint;
    private bool _wallJump;
    private Vector3 _wallDirection;
    private float _pushPower = 3;


    void Start()
    {

        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Player:: Controller is null");
        }
        UIManager.Instance.UpdateLives(_lives);
    }


    void Update()
    {
        if (transform.position.y > -8f)
        {
            Movement();
        }
        else
        {
            Respawn();
        }
       
    }
            
    private void Movement()
    {
        _hMovement = Input.GetAxis("Horizontal");
        if (_controller.isGrounded)
        {
            _direction = new Vector3(_hMovement, 0, 0);
            _velocity = _direction * _speed;
            _wallJump = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpForce;
                _doubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) )
            {
                if (_doubleJump && !_wallJump)
                {
                    _yVelocity += _jumpForce;
                    _doubleJump = false;
                }

                if (_wallJump)
                {
                    _velocity = _wallDirection* _speed;
                    _yVelocity = _jumpForce;
                    _wallJump = false;
                }
            }
            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
            //Debug.DrawRay(hit.point, hit.normal, Color.blue);
        if (hit.transform.CompareTag("MoveableBox"))
        {
            Rigidbody rig = hit.collider.attachedRigidbody;

            //checking for rigidbody
            if (rig == null || rig.isKinematic)
            {
                return;
            }

            //stop pushing things below us
            if (hit.moveDirection.y < -0.3f)
            {
                return;
            }

            Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, 0);

            //box moving time
            rig.velocity = pushDirection * _pushPower;
        
        }




        if (!_controller.isGrounded && hit.transform.CompareTag("Wall"))
        {
            _wallJump = true;
           _wallDirection = hit.normal;
        }
    }
        


    public void UpdateCoinCount()
    {
        //update coin count
        _coinCount++;
        UIManager.Instance.UpdateCoins(_coinCount);
    }

    public void Checkpoint(Transform Checkpoint)
    {
        _spawnPoint = Checkpoint;
    }

    public void Respawn()
    {
        _yVelocity = 0;
        transform.position = _spawnPoint.position;
    }

    public void UpdateLives()
    {

        _lives--;
        UIManager.Instance.UpdateLives(_lives);
        if (_lives <1)
        {
            //need to add "using UnityEngine.SceneManagement;" for this part.
            SceneManager.LoadScene(0);
        }
    }

    public int CoinsCount()
    {
        return _coinCount;
    }

}

        /*
        */
