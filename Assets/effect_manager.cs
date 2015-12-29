using UnityEngine;
using System.Collections;

public class effect_manager : MonoBehaviour {

    public GameObject m_windPrefab;
    public GameObject m_sorryPrefab;
    public GameObject m_akeomePrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddSpeedUpWind( float x , int comboLebel)
    {
        GameObject go = GameObject.Instantiate(m_windPrefab);

        //positioning
        Vector3 pos = go.transform.position;
        pos.x = x;
        go.transform.position = pos;

        go.GetComponent<wind_move>().m_windSpeed = comboLebel * 0.3f;
    }

    public void AddSorry(float x)
    {
        GameObject go = GameObject.Instantiate(m_sorryPrefab);

        //positioning
        Vector3 pos = go.transform.position;
        pos.x = x;
        go.transform.position = pos;
    }

    public void AddAkeome(float x, float y, int rank)
    {
        GameObject go = GameObject.Instantiate(m_akeomePrefab);

        if( rank == 1 )
            go.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 0.0f);
        if (rank == 2)
            go.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.5f, 0.5f);
        if (rank == 3)
            go.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);

        //currentPosition
        Vector3 pos = go.transform.position;
        pos.x = x;
        pos.y = y + 2.0f;
        go.transform.position = pos;

        //targetPosition
        pos.x = x + 4.0f;
        pos.y =  pos.y + 2.0f;

        go.GetComponent<akeome_move>().m_move_position_x = pos.x;
        go.GetComponent<akeome_move>().m_move_position_y = pos.y;
        


    }

}
