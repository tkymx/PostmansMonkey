using UnityEngine;
using System.Collections;

public class position_fix : MonoBehaviour {

    public GameObject target;
    public float xoffset = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 vec = transform.position;
        vec.x = target.transform.position.x + xoffset;
        transform.position = vec;
	
	}
}
