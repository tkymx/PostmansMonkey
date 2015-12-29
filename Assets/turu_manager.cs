using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class turu_manager : MonoBehaviour {

    public GameObject m_turuPrefab = null;
    public GameObject m_saruPrefab = null;
    public float m_turuSpeedRate = 50.0f;
    public float m_initVelocity = 70;

    public GameObject m_hint = null;

    [HideInInspector]
    public const float Offset = 18;
    
    public GameObject SetTuru( int id , Vector3 pos , float deg , float vel , GameObject before )
    {
        GameObject go = Instantiate(m_turuPrefab);
        go.transform.position = pos;
        go.GetComponent<turu_move>().defaultDeg = deg;
        go.GetComponent<turu_move>().maxhVelocity = vel;

        if (before != null)
            before.GetComponent<turu_move>().nextObject = go;

        go.transform.SetParent( transform , false);
        go.GetComponent<turu_info>().m_turuIndex = id;

        return go;
    }

    // Use this for initialization
	void Start () {

        GameObject saru = Instantiate(m_saruPrefab);
        Camera.main.GetComponent<position_fix>().target = saru;

        //init deg;
        float rightDeg = 70;
        float[] DefaultDegree = { 360 - rightDeg, rightDeg };
        float MaxVelocity = m_initVelocity;

        //一つ目
        GameObject go = SetTuru( 0  ,new Vector3( 0 , 7.56f, 0) , DefaultDegree[0] , MaxVelocity , null );

        if( go.transform.FindChild("top") != null)
            saru.transform.SetParent( go.transform.FindChild("top").transform , false );

        GameObject before_go = go;

        //それ以降
        int homeCount = state_manager.Instance.m_homeManager.GetComponent<home_manaager>().m_vhomeCounts.Length;
        for (int i = 1; i < homeCount ; i++)
            before_go = SetTuru( i , new Vector3(i * Offset, 7.56f, 0), DefaultDegree[i % 2], MaxVelocity, before_go);

        if (m_hint != null)
            m_hint.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {

        float MaxVelocity = m_initVelocity;

        //コンボ数に応じてスピードを変える
        int comboLebel = state_manager.Instance.m_comboManager.GetComponent<combo_manager>().getComboLebel();
        MaxVelocity += (comboLebel ) * m_turuSpeedRate;

        foreach (Transform tf in transform)
        {
            if (tf.GetComponent<turu_move>() != null)
                tf.GetComponent<turu_move>().maxhVelocity = MaxVelocity;
        }
        
	}

    public void reset()
    {
        bool flag = true;

        Transform before_tf = null;
        foreach (Transform tf in transform)
        {
            //saru
            if (flag)
            {
                flag = false;
                if (tf.transform.FindChild("top") != null)
                    GameObject.Find("saru(Clone)").transform.SetParent(tf.transform.FindChild("top").transform, false);
            }
            else
            {
                if( before_tf != null )
                    before_tf.GetComponent<turu_move>().nextObject = tf.gameObject;
            }

            //initialize
            tf.GetComponent<turu_move>().Start();

            before_tf = tf;
        }
    }

    public void pause( bool t )
    {
        foreach (Transform tf in transform)
        {
            if (tf.GetComponent<turu_move>() != null)
                tf.GetComponent<turu_move>().pause = t;
        }
    }

    public void Hint( int i )
    {
        if (m_hint == null) return;

        m_hint.SetActive(true);
        m_hint.GetComponent<hint_move>().MoveStart( 
            state_manager.Instance.m_homeManager.GetComponent<home_manaager>().getHomeCount(i)
            );

    }

    public void HintBack()
    {
        m_hint.GetComponent<hint_move>().MovebackStart();
    }
}
