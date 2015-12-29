using UnityEngine;
using System.Collections;

public class wind_move : MonoBehaviour {

    public float m_windSpeed = 1;
	
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate( -m_windSpeed , 0 , 0  );

	}
}
