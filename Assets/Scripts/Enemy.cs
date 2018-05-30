using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 100;
    [SerializeField] int maxHits=3;
    ScoreBoard scoreBoard;
    BlowInPieces blowInPiecesActivator;

    bool enemyDead;

    // Use this for initialization
    void Start ()
    {
        enemyDead = false;
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
        blowInPiecesActivator = gameObject.GetComponent<BlowInPieces>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if(!enemyDead)
        {
            ProcessHit();
            if (maxHits <= 0)
            {
                scoreBoard.ScoreHit(scorePerHit);
                enemyDead = true;
                KillEnemy();
            }
        }
    }

    private void ProcessHit()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        maxHits--;
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        if(blowInPiecesActivator != null)
        {
            blowInPiecesActivator.Explode();
        }
        Invoke("DestroyGameObject", 0.4f);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
