using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{

    public float speed = 5;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }

        

    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + new Vector3(Input.GetAxis("Horizontal")* speed, 0, Input.GetAxis("Vertical")) * Time.deltaTime * speed) ;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
            Debug.Log(collision.gameObject.tag);
        
    }
}
