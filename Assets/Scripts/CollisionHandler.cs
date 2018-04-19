using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] GameObject deathFX;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        print("Player dying...");
        SendMessage("OnPlayerDeath");
        //TO DO Desprender pedazos de nave
        deathFX.SetActive(true);
        Invoke("ReloadLevel", levelLoadDelay);
        
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
