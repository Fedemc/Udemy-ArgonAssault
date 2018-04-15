using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        int numMusicPlayers=FindObjectsOfType<MusicPlayer>().Length;
        if(numMusicPlayers>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
