using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(Random.Range(-10f, 10f), 7, 0);

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
            transform.position = new Vector3(Random.Range(-10f, 10f), 7, 0);
        }

    }
}
