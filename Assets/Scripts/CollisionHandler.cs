using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 1f;
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
        deathFX.SetActive(true);
        Invoke("ReloadLevel", levelLoadDelay);
        //TO DO Desprender pedazos de nave
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
