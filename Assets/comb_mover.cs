using UnityEngine;
using System.Collections;

public class comb_mover : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        jump_height = (float)Screen.height / 100.0f * 10.0f;
        jump_position = 0;
    }


    private bool jumping = true;
    private float jump_height;
    private float jump_position;

    public void jump()
    {
        jumping = true;
        jump_position = 0;
    }

    public void setComobOriginPosition( Vector3 pos )
    {
        pos.y += jump_position;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumping == true)
        {
            jump_position += (jump_height-jump_position) * 0.5f;
            if( Mathf.Abs( jump_position - jump_height ) < 5.0f )
            {
                jumping = false;
            }
        }
        else
        {
            jump_position += (0 - jump_position) * 0.1f;
        }

    }
}
