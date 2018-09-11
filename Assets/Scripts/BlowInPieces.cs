using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowInPieces : MonoBehaviour {
    [SerializeField] float explosionForce = 8f;
    
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    
    public void Explode()
    {
        GameObject body = GameObject.Find(gameObject.name + "/Body");
        foreach (Transform child in body.GetComponentInChildren<Transform>())
        {
            Vector3 pushVector = new Vector3(Random.Range(-explosionForce, explosionForce), Random.Range(-explosionForce, explosionForce), Random.Range(-explosionForce, explosionForce));
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.GetComponent<Rigidbody>().velocity = pushVector;
            child.gameObject.GetComponent<Rigidbody>().AddTorque(pushVector, ForceMode.Impulse);
        }
    }
}
