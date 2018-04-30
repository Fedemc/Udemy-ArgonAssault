using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 100;
    ScoreBoard scoreBoard;

	// Use this for initialization
	void Start ()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        Collider enemyCollider = gameObject.AddComponent<BoxCollider>();
        enemyCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update ()
    {
        
	}

    private void OnParticleCollision(GameObject other)
    {
        GameObject fx= Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;

        scoreBoard.ScoreHit(scorePerHit);

        Destroy(gameObject);
    }
}
