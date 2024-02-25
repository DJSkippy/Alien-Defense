using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private GameObject _laserPrefab;
    private float _fireRate = 3.5f;
    private float _canFire = -1f;
    private bool _isDead = false;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private AudioSource _audioSource;
    private UIManager _uIManager;
    private Player _player;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL!");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is NULL!");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Enemy Audio Source is NULL!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        EnemyFire();
    }

    void EnemyFire()
    {
        if (Time.time > _canFire && !_isDead)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }

        }

    }

    void CalculateMovement()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if (transform.position.y < -5.5f)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
     
        if (other.tag == "Player") 
        {
                        
            if (_player != null) 
            {
                _player.Damage();
            }
            
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.35f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }
            
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            _isDead = true;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.35f);
        }

    }

}
