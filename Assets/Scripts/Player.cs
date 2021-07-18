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
    [SerializeField] private int _lives = 3;
    [SerializeField] private Transform _spawnPoint;
    private int _coinCount;

    void Start()
    {

        _controller = GetComponent<CharacterController>();
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
        _direction = new Vector3(_hMovement, 0, 0);
        _velocity = _direction * _speed;
        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpForce;
                _doubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _doubleJump)
            {

                _yVelocity += _jumpForce;
                _doubleJump = false;
            }
            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
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
        _velocity.y = 0;
        transform.position = _spawnPoint.position;
    }

    public void UpdateLives()
    {

        _lives--;
        UIManager.Instance.UpdateLives(_lives);
        if (_lives <1)
        {
            SceneManager.LoadScene(0);
        }
    }

}

        /*
        */
