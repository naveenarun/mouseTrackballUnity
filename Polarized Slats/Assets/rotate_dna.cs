using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_dna : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.AngleAxis(Time.realtimeSinceStartup*180, Vector3.up);
    }
}
