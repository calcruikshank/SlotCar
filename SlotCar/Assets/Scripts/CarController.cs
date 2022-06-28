using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public State state;
    Vector3 inputMovement;
    Rigidbody rb;
    float accelSpeed = 3;
    float topSpeed = 30;
    float turnSpeed = .5f;
    Vector3 carVisualStartingRotation;

    Transform carVisual;
    bool accelerating = false;

    float turnValue = 0;

    public List<TrailRenderer> trails;
    public MeshRenderer carMat;

    public int lap = 0;

    public enum State
    {
        Normal
    }

    private void Awake()
    {
        Color col = new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
        carMat.material.SetColor("_BaseColor", col);
        FollowScript.singleton.players.Add(this.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        ToggleTrails(false);
        state = State.Normal;
        rb = this.GetComponent<Rigidbody>();
        carVisual = this.transform.GetChild(0);
        carVisualStartingRotation = carVisual.forward;
    }


    void Update()
    {
        switch (state)
        {
            case State.Normal:
                HandleMovement();
                break;
        }

        if(lap >= 7)
        {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
    }

    private void HandleMovement()
    {
        //carVisual.transform.forward = carVisualStartingRotation;
        turnValue = inputMovement.x;

        Vector3 carRot = carVisual.transform.eulerAngles;
        carVisual.transform.rotation = Quaternion.Euler(carRot.x, carRot.y + (turnValue * turnSpeed * rb.velocity.normalized.magnitude), carRot.z);

        //trails
        if(accelerating && turnValue != 0)
        {
            ToggleTrails(true);
        }else
        {
            ToggleTrails(false);
        }
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                FixedHandleMovement();
                break;
        }
    }

    private void FixedHandleMovement()
    {
        if (accelerating)
        {
            rb.AddForce(carVisual.right * accelSpeed * 15);
            if (rb.velocity.magnitude > topSpeed)
            {
                rb.velocity = rb.velocity.normalized * topSpeed;
            }
        }
        
    }

    void OnMove(InputValue value)
    {
        inputMovement = value.Get<Vector2>();
    }

    void OnFirePressed()
    {
        accelerating = true;
        //fire = true;
    }

    void OnFireReleased()
    {
        accelerating = false;
        //  fire = false;
    }

    void ToggleTrails(bool on)
    {
        foreach(TrailRenderer trail in trails)
        {
            trail.emitting = on;
        }
    }
}

