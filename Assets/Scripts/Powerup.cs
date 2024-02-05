using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private int powerupID;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private Player _TripleShotActive;
    [SerializeField] private Player _SpedBoostActive;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(_speed * Time.deltaTime * Vector3.down);

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
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }

            Destroy(this.gameObject);
        }
              
    }

}
