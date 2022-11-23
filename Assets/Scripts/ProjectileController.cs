using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void DestroyObject()
    {
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.y > Camera.main.orthographicSize)
        {
            DestroyObject();
        }

    }
}
