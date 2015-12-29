using UnityEngine;
using System.Collections;

public class time_release : MonoBehaviour {

    public int max_time = 100;
    private int current_time = 0;

	// Use this for initialization
	void Start () {
        current_time = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if( ++current_time >= max_time )
        {
            GameObject.Destroy(gameObject);
        }
	}
}
