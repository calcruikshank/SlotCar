using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailHeadController : MonoBehaviour
{
    Rigidbody rb;
    float accelSpeed = 3;
    float topSpeed = 25;
    float turnSpeed = .5f;

    float randomTime = 1f;
    float randomTimeCounter = 0f;
    float turnValue;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        randomTimeCounter += Time.deltaTime;
        if (randomTimeCounter > randomTime)
        {
            topSpeed = Random.Range(25, 35);
                randomTime = Random.Range(1f, 10f);
            turnValue = Random.Range(-.75f, .75f);
            randomTimeCounter = 0f;
            Debug.Log(turnValue);
        }

        Vector3 carRot = this.transform.eulerAngles;
        transform.rotation = Quaternion.Euler(carRot.x, carRot.y + (turnValue * turnSpeed * rb.velocity.normalized.magnitude), carRot.z);
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.right * accelSpeed * 15);
        if (rb.velocity.magnitude > topSpeed)
        {
            rb.velocity = rb.velocity.normalized * topSpeed;
        }

    }
}
