using UnityEngine;
using System.Collections;

public class akeome_move : MonoBehaviour {

    public float m_move_position_x;
    public float m_move_position_y;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        iTween.MoveTo(this.gameObject, iTween.Hash(
            "x", m_move_position_x,
            "y", m_move_position_y,
            "time", 1.5f));
	
	}
}
