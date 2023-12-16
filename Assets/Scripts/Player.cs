﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 6.9f;
    [SerializeField] private GameObject _laserPrefab;
    private float _fireRate = 0.25f;
    private float _canFire = -1.0f;
    [SerializeField] private int _lives = 3;
    [SerializeField] private SpawnManager _spawnManager;
        
    // Start is called before the first frame update
    void Start()
    {
        
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn manager is NULL.");
        }

    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        { 
            FireLaser();
        }
    }
        void CalculateMovement()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.75f, 0), 0);

        if (transform.position.x > 11.25f)
        {
            transform.position = new Vector3(-11.25f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.25f)
        {
            transform.position = new Vector3(11.25f, transform.position.y, 0);
        }

    }

    void FireLaser()
    {

        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        
    }

    public void Damage()
    {
        _lives--;

        if (_lives < 1)
        {
            //destroy all enemies if player is dead
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

}
