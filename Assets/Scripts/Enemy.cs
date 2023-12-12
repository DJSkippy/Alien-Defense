using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);

    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per scond

        //if bottom of screen
        //respawn at the top with a new random x postion

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.5f)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7, 0);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //if other is Player
        //damage the player
        //Destroy Us

        //if other is laser
        //laser
        //destroy us

        if (other.tag == "Player") 
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player != null ) 
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

}
