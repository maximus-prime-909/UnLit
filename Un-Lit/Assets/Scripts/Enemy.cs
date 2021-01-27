using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    //Current Spawn Location
    Vector3 spawnLocation;

    //Player Loccation
    private Transform character;

    //Distance b/w character and enemy
    private float distanceBw;

    public float enemySpeed;
    public float triggerDistance;

    public GameObject player;
    void Start()
    {
        spawnLocation = transform.position;

        character = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceBw = Vector3.Distance(character.position, transform.position);

        if(distanceBw <= triggerDistance)
        {
            transform.LookAt(character);
            GetComponent<Rigidbody>().AddForce(transform.forward * enemySpeed);
        }

        if(distanceBw <= 2f)
        {
            BlowUp();
        }
    }

    private void BlowUp()
    {
        print("Go Boom");
        
        //Add Amnimation//

        Destroy(gameObject);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    private Vector3 getRoamingPosition()
    {
       return (spawnLocation + (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized) * Random.Range (5f, 15f));
    }
}
