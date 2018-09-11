using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In meters per second")] [SerializeField] float controlSpeed = 4f;
    [SerializeField] float xLimit = 5.5f;
    [SerializeField] float yLimit = 3.5f;

    [Header("Screen-position based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;
    [Header("Control-throw based")]
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float controlRollFactor = -30f;
    [SerializeField] GameObject[] lasers;
    [SerializeField] GameObject beam;
    [SerializeField] GameObject beamSpawner;
    [SerializeField] GameObject crosshair;
    [SerializeField] float beamForce = 10f;
    BlowInPieces blowInPiecesActivator;



    float xThrow, yThrow;
    bool isControlEnabled = true;

    

    void Start()
    {
        blowInPiecesActivator = gameObject.GetComponent<BlowInPieces>();
    }

    void Update()
    {
        if (isControlEnabled)
        {
            ManageTranslation();
            ManageRotation();
            ManageFiring();
        }
        

    }

    

    private void ManageRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ManageTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        float newXPos = Mathf.Clamp(rawNewXPos, -xLimit, xLimit);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;
        float newYPos = Mathf.Clamp(rawNewYPos, -yLimit, yLimit);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }

    private void ManageFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetLasersActive(true);

            /*
            GameObject b = Instantiate(beam, beamSpawner.transform.position, Quaternion.identity);
            b.transform.Rotate(0,-90, 0, Space.Self);
            Rigidbody rb = b.GetComponent<Rigidbody>();

            var beamSpeed = gameObject.GetComponent<Rigidbody>().velocity;
            rb.velocity = transform.TransformDirection(new Vector3(0, 0, beamForce));
            //rb.AddForce(Vector3.forward * beamForce, ForceMode.Impulse);
            */
        }
        else
        {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {

        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
        
    }

    private void OnPlayerDeath()    //called by string reference from CollisionHandler.cs
    {
        isControlEnabled = false;
        SetLasersActive(false);
        blowInPiecesActivator.Explode();
    }    
}
