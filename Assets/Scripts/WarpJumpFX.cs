using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpJumpFX : MonoBehaviour
{

    [SerializeField] [Range(1,100)] float speed=1;
    private GameObject[] particles;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        foreach (Transform child in transform)
        {
            ParticleSystem pr = child.gameObject.GetComponent<ParticleSystem>();
            var main = pr.main;
            main.startSpeed = Mathf.Clamp(speed,5, 40);
            ParticleSystemRenderer psr = child.gameObject.GetComponent<ParticleSystemRenderer>();
            psr.lengthScale = speed;
        }	
	}
}
