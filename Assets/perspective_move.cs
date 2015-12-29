using UnityEngine;
using System.Collections;

public class perspective_move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position;
        pos.x = +Camera.main.transform.position.x*0.5f;
        transform.position = pos;
	
	}
}
