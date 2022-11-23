using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionController : MonoBehaviour
{
    //position of the attached game object relative to the bottom screen 
    float positionUFOy;

    //pull-up speed
    float raiseSpeed = 20f;



    //ray activation event
    UnityEvent rayActivation;
    void Start()
    {

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
        if (Input.GetKey("space") && rayActivation != null)
        {
            rayActivation.Invoke();
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
            Debug.Log("Raise Speed :" + Vector3.up * raiseSpeed * Time.deltaTime);
        }
        else
        {

            Debug.DrawLine(transform.position, transform.position - transform.up * positionUFOy, Color.green);


        }
    }
}
