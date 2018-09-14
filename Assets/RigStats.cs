using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigStats : MonoBehaviour
{
    private Vector3 prevPos = new Vector3(0,0,0);


	void Start ()
    {
        prevPos = transform.position;
    }

    void Update ()
    {
        Debug.Log("Previous position: " + prevPos.ToString());
        Vector3 currentPos = transform.position;
        Debug.Log("Current position: " + currentPos.ToString());
        Debug.Log("Distance: " + Vector3.Distance(currentPos,prevPos).ToString());
        prevPos = transform.position;
	}
}
