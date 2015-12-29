using UnityEngine;
using System.Collections;

public class sorry_move : MonoBehaviour {

    public float m_move_position_y;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        iTween.MoveTo(this.gameObject, iTween.Hash(
            "y", m_move_position_y,
            "time", 0.8f));

	}
}
