using UnityEngine;
using System.Collections;

public class check_hit : MonoBehaviour {

    public GameObject hit_effect;
    public bool m_isEnmey = false;

    [HideInInspector]
    public const float Range1 = 0.2f;
    public const float Range2 = 0.5f;
    public const float Range3 = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private int CalcScore( int comboLebel , int rank )
    {
        //コンボによるスコアの補正
        return (comboLebel + 1) * 30 + (3-rank) * 20;
    }

    public bool isIn(GameObject saru)
    {
        if ( Mathf.Abs(saru.transform.position.x - transform.position.x) < Range3)
        {
            return true;
        }
        return false;
    }

    public bool hit( GameObject saru )
    {
        if (GetComponent<home>().isSupply()) return false;

        if ( Mathf.Abs(saru.transform.position.x - transform.position.x) > Range3) return false;

        if( m_isEnmey )
        {
            state_manager.Instance.m_homeManager.GetComponent<home_manaager>().Failue( transform );
            return true;
        }

        int rank = 3;
        if (Mathf.Abs(saru.transform.position.x - transform.position.x) < Range1)
            rank = 1;
        else if (Mathf.Abs(saru.transform.position.x - transform.position.x) < Range2)
            rank = 2;


        GameObject hit = Instantiate(hit_effect);
        hit.transform.parent = transform;
        hit.transform.position = new Vector3(0, 0, 0);
        hit.transform.localPosition = new Vector3(0, 0.1f, 0);

        //supply
        state_manager.Instance.m_comboManager.GetComponent<combo_manager>().Success(0);
        state_manager.Instance.AddScore(CalcScore(state_manager.Instance.m_comboManager.GetComponent<combo_manager>().getComboLebel(),rank));
        state_manager.Instance.AddSupply(rank);
        GetComponent<home>().Supply();
        state_manager.Instance.m_turuManager.GetComponent<turu_manager>().m_hint.GetComponent<hint_move>().Supply();

        //akeome
        state_manager.Instance.m_effectManager.GetComponent<effect_manager>().AddAkeome(Camera.main.transform.position.x , transform.position.y, rank);

        return true;
    }
}
