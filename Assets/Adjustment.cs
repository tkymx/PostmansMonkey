using UnityEngine;
using System.Collections;

[RequireComponent(typeof(saru_state))]
public class Adjustment : MonoBehaviour{

    public Vector3 m_initPosition = new Vector3();
    public float smoothVelocity = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.localPosition += (m_initPosition - transform.localPosition) * smoothVelocity;

        //サルの状態を変更
        if ( (m_initPosition - transform.localPosition).magnitude < smoothVelocity)
        {
            GetComponent<saru_state>().IsSaruFlaying = false;
        }

        if( GetComponent<saru_state>().IsSaruFlaying )
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));	
        }
	}
}
