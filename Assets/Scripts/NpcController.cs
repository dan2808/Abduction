using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{

    //we need half width as the middle position of the screen(camera) is 0 
    private float screenHalfWidthInWorldUnits;
    public GameObject projectile;

    //gravitational force
    float gravity = 5f;
    public float speed = 3f;

    float nextProjectileTime;

    private Vector3 direction;

    private Vector3[] directions = { Vector3.right, Vector3.left };


    private float halfNpcWidth;
    // Start is called before the first frame update
    void Start()
    {
        nextProjectileTime = Time.time + 1;
        direction = directions[Random.Range(0, 1)];
        halfNpcWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - halfNpcWidth;

    }
    public void DestroyObject()
    {
        Destroy(gameObject);

    }

    private void Fire(GameObject projectileBullet)
    {
        Vector3 positionSpawnedProjectile = transform.position;
        positionSpawnedProjectile.y = positionSpawnedProjectile.y + transform.localScale.y / 2f;
        Instantiate(projectileBullet, positionSpawnedProjectile, Quaternion.identity);
    }

    //when the collision is triggered 
    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("TriggerEnter");
        if (other.gameObject.tag == "UFO")
        {
            DestroyObject();
        }
    }



    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"transform.localScale.y/2f - Camera.main.orthographicSize{transform.localScale.y / 2f - Camera.main.orthographicSize}");
        // Debug.Log($"transform.position.y : {transform.position.y}");
        if (transform.localScale.y / 2f - Camera.main.orthographicSize < transform.position.y)
        {
            transform.Translate(Vector3.down * gravity * Time.deltaTime);
            Debug.Log("Gravity Speed :" + Vector3.down * gravity * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);

            if (transform.position.x < -screenHalfWidthInWorldUnits)
            {
                transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
                direction = Vector3.right;
            }

            if (transform.position.x > screenHalfWidthInWorldUnits)
            {
                transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
                direction = Vector3.left;

            }
        }
        Debug.Log($"Time.time:{Time.time}");
        #region Projectile firing at random timing
        if (Time.time > nextProjectileTime)
        {
            Debug.Log($"nextProjectileTime:{nextProjectileTime}");
            Fire(projectile);
            nextProjectileTime = Time.time + Random.Range(1, 5);
        }
        #endregion

    }
}
