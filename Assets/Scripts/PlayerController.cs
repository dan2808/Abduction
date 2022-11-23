using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerController : MonoBehaviour
{

    //position of the attached game object relative to the bottom screen 
    float positionUFOy;

    public int humansAbducted = 0;


    //ray activation event
    [SerializeField] UnityEvent rayActivation;

    //we need half width as the middle position of the screen(camera) is 0 
    private float screenHalfWidthInWorldUnits;

    public float speed = 1;

    //pull-up speed
    [SerializeField] float raiseSpeed = 1f;

    private float halfPlayerWidth;
    public event System.Action OnPlayerDeath;


    void Start()
    {
        halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - halfPlayerWidth;

        positionUFOy = transform.position.y + Camera.main.orthographicSize; //Camera.main.orthographicSize * 2f - - transform.localScale.y / 2f;
        Physics2D.queriesStartInColliders = false;

        if (rayActivation == null)
        {
            rayActivation = new UnityEvent();
        }
        rayActivation.AddListener(BeamRay);

    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = speed * inputX;
        transform.Translate(Vector2.right * velocity * Time.deltaTime);
        // Debug.Log("horizontal move " + velocity);

        if (transform.position.x < -screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
        }

        if (transform.position.x > screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
        }

        if (Input.GetKey("space") && rayActivation != null)
        {
            rayActivation.Invoke();
        }


    }


    //when the triggered is entered and has a certain tag ( in this case Projectiles ) the UFO and Projectile are destroyed
    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("UFO triggered entered");
        if (other.gameObject.tag == "Projectiles")
        {
            if (OnPlayerDeath != null)
            {
                OnPlayerDeath();
            }
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Humans")
        {
            humansAbducted++;
        }
    }
    void BeamRay()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, -transform.up, positionUFOy);
        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            hitInfo.transform.GetComponent<NpcController>().enabled = false;
            hitInfo.collider.transform.Translate(Vector3.up * raiseSpeed * Time.deltaTime);
            hitInfo.transform.GetComponent<NpcController>().enabled = true;

        }
        else
        {

            Debug.DrawLine(transform.position, transform.position - transform.up * positionUFOy, Color.green);


        }
    }
}
