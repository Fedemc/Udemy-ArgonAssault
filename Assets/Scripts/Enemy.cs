﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;

    //
    [SerializeField] ScoreBoard scoreBoard;
    //

	// Use this for initialization
	void Start ()
    {
        Collider enemyCollider = gameObject.AddComponent<BoxCollider>();
        enemyCollider.isTrigger = false;

        //
        scoreBoard = FindObjectOfType<ScoreBoard> ();
        //
    }

    // Update is called once per frame
    void Update ()
    {
        
	}

    private void OnParticleCollision(GameObject other)
    {
        GameObject fx= Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        
        //
        scoreBoard.ScoreHit();
        //

        Destroy(gameObject);
    }
}
