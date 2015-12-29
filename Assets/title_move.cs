using UnityEngine;
using System.Collections;

public class title_move : MonoBehaviour {

    private const float height_position = 20;

    private float position = 0;
    private float default_position = 0;
    private bool isUP = true;

    public float smoothStep = 0.1f;

	// Use this for initialization
	void Start () {

        position = transform.position.y + height_position;
        default_position = transform.position.y;
	
	}
	
	// Update is called once per frame
	void Update () {

        if( isUP )
        {
            float de = position - transform.position.y;
            transform.Translate(0, de * smoothStep, 0);
            if (de * de < 0.1)
            {
                isUP = false;
            }
        }
        else
        {
            float de = default_position - transform.position.y;
            transform.Translate(0, de * smoothStep, 0);
            if (de * de < 0.1)
            {
                isUP = true;
            }
        }
	
	}
}
