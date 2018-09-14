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
    [SerializeField] BeamHandler beam;
    [SerializeField] GameObject[] beamSpawner;
    [SerializeField] GameObject crosshair;
    private float timeToFire=0;
    private BlowInPieces blowInPiecesActivator;
    private Vector3 prevPos;
    private float currentSpeed;


    float xThrow, yThrow;
    bool isControlEnabled = true;



    void Start()
    {
        blowInPiecesActivator = gameObject.GetComponent<BlowInPieces>();
        prevPos = transform.position;
    }

    void Update()
    {
        if (isControlEnabled)
        {
            ManageTranslation();
            ManageRotation();
            ManageFiring();
        }

        Vector3 currentPos = transform.position;
        currentSpeed = getDistance(currentPos, prevPos)/Time.deltaTime;
        //Debug.Log(currentSpeed.ToString());
        prevPos = transform.position;
    }

    private float getDistance(Vector3 currentPos, Vector3 prevPos)
    {
        return Vector3.Distance(currentPos, prevPos);
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
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
        if (CrossPlatformInputManager.GetButton("Fire") && (Time.time >= timeToFire))
        {
            SetLasersActive(true);
            foreach(GameObject bSp in beamSpawner)
            {
                BeamHandler b = Instantiate(beam, bSp.transform.position, Quaternion.identity);
                b.transform.LookAt(crosshair.transform);
                timeToFire = Time.time + 1 / b.fireRate;
                b.ship = this;
            }                  
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
