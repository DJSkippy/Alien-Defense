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
        
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if (transform.position.y < -5.5f)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7, 0);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
     
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
