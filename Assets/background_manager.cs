using UnityEngine;
using System.Collections;

public class background_manager : MonoBehaviour {

    public GameObject m_treePrefab;
    public GameObject m_backTreePrefab;

	// Use this for initialization
	void Start () {

        int treeCount = state_manager.Instance.m_homeManager.GetComponent<home_manaager>().m_vhomeCounts.Length / 10 + 1;
        for (int i = 0; i < treeCount; i++)
        {
            GameObject go = GameObject.Instantiate(m_backTreePrefab);
            go.transform.Translate(go.GetComponent<BoxCollider>().size.x * i, 0, 0);
            go.transform.SetParent(this.transform, false);
        }
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    public void AddTree( Vector3 vec )
    {
        //create
        GameObject go = GameObject.Instantiate(m_treePrefab);

        //positoning
        Vector3 pos = go.transform.position;
        pos.x = vec.x;
        go.transform.position = pos;

        //set parent
        go.transform.SetParent( transform.FindChild("saruTree").transform , false );
    }
}
