using System;
using UnityEngine;

public class BouncingThing : MonoBehaviour
{
    [SerializeField] private float speedLimit = 2f;
    [SerializeField] private float bounceForce = 5f;

    [SerializeField] private Color slowColor = Color.red;
    [SerializeField] private Color fastColor = Color.white;


    private Rigidbody rb;
    private MeshRenderer renderer;

    private bool isReady = false;
    private float startTime;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<MeshRenderer>();
    }


    void FixedUpdate()
    {
        HandleMovement();
    }
    void HandleMovement()
    {
        float currentSpeed = rb.linearVelocity.magnitude;

        if (currentSpeed < speedLimit)
        {
            if (!isReady)
            {
                isReady = true;
                startTime = Time.time;
                renderer.material.color = slowColor;
            }
        }
        else
        {
            if (isReady)
            {
                isReady = false;
                renderer.material.color = fastColor;

                float duration = 0f;
                do
                {
                    duration = Time.time - startTime;
                }
                while (duration <= 0f);

                Debug.Log($"Скорость ниже порога в течение {Math.Round(duration, 2)} секунд");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        rb.linearVelocity = new Vector3(0, bounceForce, 0);
    }
}
