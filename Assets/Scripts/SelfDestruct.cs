using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    [SerializeField] float timeToWait = 5f;


	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, timeToWait);
	}
}
