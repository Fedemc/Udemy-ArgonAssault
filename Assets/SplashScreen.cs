using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Invoke("GoPlayScene", 3f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void GoPlayScene()
    {
        SceneManager.LoadScene(1);
    }
}
