using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.0f;

    private Rigidbody2D rigidbody;

    public float minHeight = 2.56f;
    public float maxHeight = 5.0f;


    private Vector2 dir = new Vector3(0.0f, 1.0f, 0.0f);


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (rigidbody.position.y >= maxHeight)
            dir *= -1.0f;
        else if (rigidbody.position.y <= minHeight)
            dir *= -1.0f;

        rigidbody.velocity = dir * speed;
    }
}
