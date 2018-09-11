using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamHandler : MonoBehaviour {

    [SerializeField] float beamSpeed = 1f;
    [SerializeField] float delayTime=8;
    private float counter=0;

	// Use this for initialization
	void Start ()
    {
        transform.Rotate(90, 0, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //transform.Translate(0, 0, beamSpeed);
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
