using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private bool _isTripleShotActive = false;
    [SerializeField] private bool _isSpeedBoostActive = false;
    [SerializeField] private bool _isShieldsActive = false;
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _speedBoost = 5f;
    [SerializeField] private float _fireRate = 0.25f;
    [SerializeField] private float _canFire = -1.0f;
    [SerializeField] private int _lives = 3;
    [SerializeField] private int _score;
    [SerializeField] private AudioClip _laserSoundClip;
    [SerializeField] private AudioClip _explosionSoundClip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldsVisualizer;
    [SerializeField] private GameObject _leftEngine;
    [SerializeField] private GameObject _rightEngine;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    
    //variable to store the audio clip
        
    //Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn manager is NULL!");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL!");
        }

        if ( _audioSource == null)
        {
            Debug.LogError("The AudioSource on the player is NULL!");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    //Update is called once per frame
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
               
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _audioSource.Play();
        //play the laser audio clip

    }

    public void Damage()
    {
      
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldsVisualizer.SetActive(false);
            return;
        }

        _lives--;

       //random?

        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }
        

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            //destroy all enemies if player is dead
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRountine());
    }
    
    IEnumerator TripleShotPowerDownRountine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    
    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed += _speedBoost;
        StartCoroutine(SpeedBoostPowerDownRountine());
    }

    IEnumerator SpeedBoostPowerDownRountine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed -= _speedBoost;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldsVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
