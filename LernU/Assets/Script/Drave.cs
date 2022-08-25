using System;
using UnityEngine;

public class Drave : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] float forseMoment = 1f;
    [SerializeField] float boostSpeed = 50f;
    [SerializeField] float baseSpeed = 5f;
    SurfaceEffector2D surfaceEffector;
    bool crash = true;
    void Start()
    {
        surfaceEffector = FindObjectOfType<SurfaceEffector2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (crash) { 
        RotatePlayer();
        RespondToBoost();
        }
    }


    public void DesibaleMet() {
        crash = false;
    }

    void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.LeftShift)) {
            surfaceEffector.speed = boostSpeed; 
        }else
        if (Input.GetKey(KeyCode.S)) {
            surfaceEffector.speed = baseSpeed;
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.A))
            rb2d.AddTorque(forseMoment);
        else if (Input.GetKey(KeyCode.D))
            rb2d.AddTorque(-forseMoment);
    }

 
}
