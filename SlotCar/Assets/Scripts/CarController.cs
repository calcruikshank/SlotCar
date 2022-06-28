using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public State state;
    Vector3 inputMovement;
    public enum State
    {
        Normal
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Normal;
    }


    void Update()
    {
        switch (state)
        {
            case State.Normal:
                break;
        }
    }
    void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                break;
        }
    }


    void OnMove(InputValue value)
    {
        //inputMovement = value.Get<Vector2>();
    }

    void OnFire()
    {
        //fire = true;
    }

    void OnFireReleased()
    {
      //  fire = false;
    }
}

