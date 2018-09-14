using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamHandler : MonoBehaviour {

    [SerializeField] float beamSpeed = 5f;
    [SerializeField] float beamTimeAlive = 3f;

    public float fireRate=5;
    public PlayerController ship;


    void Start ()
    {
        Destroy(gameObject, beamTimeAlive);
	}


    void Update ()
    {
        //Debug.Log(ship.GetCurrentSpeed());
        transform.position += transform.forward * (beamSpeed * Time.deltaTime) * ship.GetCurrentSpeed(); 
	}

    private void OnCollisionEnter(Collision collision)
    {
        beamSpeed = 0;
        Destroy(gameObject);
    }



    public void SetRotation()
    {

    }
}
