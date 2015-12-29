using UnityEngine;
using System.Collections;

public class jump : MonoBehaviour {

    public bool isEndles = false;
    public float jump_height = 2.5f;
    private Vector3 default_position;

	// Use this for initialization
	void Start () {
        default_position = transform.position;
	}

    bool isUP = true;
	
	// Update is called once per frame
	void Update () {

        if( isUP )
        {
            Vector3 ob = default_position;
            ob.y += jump_height;

            iTween.MoveTo(gameObject, ob, 0.5f);

            if ((transform.position - ob).magnitude < 0.1f)
                isUP = false;
        }
        else
        {
            iTween.MoveTo(gameObject, default_position , 0.5f);

            if(isEndles)
            {
                if ((transform.position - default_position).magnitude < 0.1f)
                    isUP = true;
            }
        }

	}
}
