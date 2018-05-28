using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaEffectsController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        //int valor = Random.Range(0, 4);
        //Debug.Log("Instancia Id:" + gameObject.GetInstanceID().ToString() + "\nValor: " + valor.ToString());
        InvokeRepeating("Activate", Random.Range(0, 3), Random.Range(0, 5));
    }
	
	// Update is called once per frame
	void Update ()
    {
             
    }

    void Activate()
    {
        if(!gameObject.GetComponent<ParticleSystem>().isPlaying)
        {
            gameObject.GetComponent<ParticleSystem>().Play();
        }
    }
}
