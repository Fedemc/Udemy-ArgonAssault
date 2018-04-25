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
    float xThrow, yThrow;
    bool isControlEnabled = true;

    [SerializeField] float explosionForce = 8f;

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ManageTranslation();
            ManageRotation();
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

    private void OnPlayerDeath()    //called by string reference from CollisionHandler.cs
    {
        isControlEnabled = false;
        BlowInPieces(); //Se puede remover
    }

    private void BlowInPieces()
    {
        GameObject shipBody = GameObject.Find("Body");
        foreach (Transform child in shipBody.GetComponentInChildren<Transform>())
        {
            Vector3 pushVector = new Vector3(UnityEngine.Random.Range(-explosionForce, explosionForce), UnityEngine.Random.Range(-explosionForce, explosionForce), UnityEngine.Random.Range(-explosionForce, explosionForce));
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.GetComponent<Rigidbody>().velocity = pushVector;
            //TO DO: Hacer que roten las partes
        }
    }   
}
