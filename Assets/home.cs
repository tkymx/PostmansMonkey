using UnityEngine;
using System.Collections;

public class home : MonoBehaviour {

    private bool m_isSupplyCount;
    public void Supply() { m_isSupplyCount = true; }
    public bool isSupply() { return m_isSupplyCount; }

    private int m_home_id;
    public void setID(int i)
    {
        m_home_id = i;
    }

	// Use this for initialization
	public void Start () {
        m_isSupplyCount = false;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool isFailue()
    {
        if (GetComponent<check_hit>().m_isEnmey) return false; 

        if( transform.position.x + check_hit.Range3 < Camera.main.GetComponent<position_fix>().target.transform.position.x )
        {
            if (isSupply())
                return false;
        }
        else
        {
            return false;
        }

        return true;
    }
}
