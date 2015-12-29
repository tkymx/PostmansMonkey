using UnityEngine;
using System.Collections;

public class turu_info : MonoBehaviour {

    [HideInInspector]
    public int m_turuIndex = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HintNextHomes()
    {
        transform.parent.GetComponent<turu_manager>().Hint(m_turuIndex+1);
    }

    public void HintBack()
    {
        transform.parent.GetComponent<turu_manager>().HintBack();
    }
}
