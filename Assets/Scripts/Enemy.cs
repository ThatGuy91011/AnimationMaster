using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bullet;

    public float speed;

    public bool isRespawning;

    public int respawnTime;
    // Start is called before the first frame update
    void Start()
    {
        enemyShoot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        // If the enemy is shot...
        if (other.CompareTag("Bullet"))
        {
            // "Destroy" the enemy
            this.gameObject.transform.localScale = new Vector3( 0,0,0);
            // Destroy the bullet
            Destroy(other.gameObject);
            // Start the timer for enemy respawn
            StartCoroutine(enemyRespawn());
        }
    }

    IEnumerator enemyRespawn()
    {
        //W hile respawning...
        isRespawning = true;
        //Once the enemy is defeated, they will wait a certain amount of time before respawning.
        yield return new WaitForSeconds(respawnTime);
        // "Reactivate" the enemy
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        // It is no longer respawning
        isRespawning = false;
    }

    void enemyShoot()
    {
        if (isRespawning)
        {
            //Do nothing.
        }
        else
        {
            //TODO: Shoot single bullet
            //New game object based on a prefab
            GameObject instBullet = Instantiate(bullet, transform.position + new Vector3(0, 0, -1f), Quaternion.identity) as GameObject;
            //New rigidbody based on prefab
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            //Set the velocity of the bullet to the relative forward of the bullet spawn object
            instBulletRigidbody.velocity = transform.forward * speed;
            //Timer for bullet despawn
            StartCoroutine(bulletDespawn(instBullet));
        }

    }
    IEnumerator bulletDespawn(GameObject instBullet)
    {
        //Once the bullet is shot, wait two seconds before destroying it.
        yield return new WaitForSeconds(2);
        Destroy(instBullet);
        yield return new WaitForSeconds(5);
        enemyShoot();
    }

}
