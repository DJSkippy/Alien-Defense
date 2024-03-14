using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private int powerUpID;
    [SerializeField] private float _speed = 3.0f;
    private Player _TripleShotActive;
    private Player _SpedBoostActive;
    private Player _ShieldsActive;
    [SerializeField] private AudioClip _clip;
    
    
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

            AudioSource.PlayClipAtPoint(_clip, transform.position);
                        
            if (player != null)
            { 
                switch (powerUpID)
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
