using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private AudioClip _explosionSoundClip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _explosionPrefab;
    private UIManager _uIManager;
    private Player _player;
    private Animator _anim;
    
    // Start is called before the first frame update
    void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if ( _player == null)
        {
            Debug.LogError("The Player is NULL!");
        }
        
        _anim = GetComponent<Animator>();

        if ( _anim == null )
        {
            Debug.LogError("Animator is NULL!");
        }

        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource on the player is NULL!");
        }
        else
        {
            _audioSource.clip = _explosionSoundClip;
        }

    }

    // Update is called once per frame
    void Update()
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
            Destroy(this.gameObject, 2.35f);
            _audioSource.Play();
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
            Destroy(this.gameObject, 2.35f);
            _audioSource.Play();
        }
    }

}
