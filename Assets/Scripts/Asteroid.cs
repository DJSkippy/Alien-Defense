using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private float _rotationSpeed = 20.0f;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private AudioClip _explosionSoundClip;
    [SerializeField] private AudioSource _audioSource;
    private SpawnManager _spawnManager;
        
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        transform.position = new Vector3(0, 5, 0);

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
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        //transform.Translate(_speed * Time.deltaTime * Vector3.down);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            _audioSource.Play();
            Destroy(this.gameObject, .25f);
        }
    }
    
}
