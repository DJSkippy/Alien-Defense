using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private Player _TripleShotActive;
    //[SerializeField] private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(_speed * Time.deltaTime * Vector3.down);
        
        //while (_stopSpawning == false)
        {

            //Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            //GameObject newPowerup = Instantiate(_triple_Shot_PowerupPrefab, posToSpawn, Quaternion.identity);
            
            //float randomX = Random.Range(-8.0f, 8.0f);
            //transform.position = new Vector3(randomX, 7, 0);

        }

        if (transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
        }
                
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {

            Player player = other.transform.GetComponent<Player>();
            
            if (player != null)
            {
                player.TripleShotActive();
            }

            Destroy(this.gameObject);
        }
              
    }

}
