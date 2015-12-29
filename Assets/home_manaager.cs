using UnityEngine;
using System.Collections;

public class home_manaager : MonoBehaviour {

    public GameObject m_homePrefab = null;
    public GameObject m_targetPrefab = null;
    public GameObject[] m_homePrefabs = new GameObject[3];

    public GameObject[] m_charaPrefabs = new GameObject[3]; //キャラのプレファブ
    public int[] m_charaCounts = new int[3];    //そのキャラが家に何人いるか？
    public GameObject m_enemyPrefab = null;     //触れちゃいけないやつ
    public float m_enemyRate;

    public int[] m_vhomeCounts = new int[10];
    public int getAnimalFromCount(int count)
    {
        ArrayList al = new ArrayList();
        for( int i = 0 ; i < m_charaCounts.Length ; i++ )
        {
            if (m_charaCounts[i] == count)
                al.Add( i );
        }

        return (int)al[ Random.Range(0,al.Count) ];
    }
    public int getHomeCount(int index)
    {
        return (int)m_vhomeCounts[index];
    }

    private GameObject AnimalInstantiate( GameObject pre , Vector3 pos , int id , Transform parent = null )
    {
        GameObject go = GameObject.Instantiate(pre);
        go.transform.position = pos;
        go.transform.localScale = go.transform.localScale * 0.7f;
        go.GetComponent<home>().setID(id);
        if (parent != null)
            go.transform.SetParent(parent, false);

        /*　動物と同じヒエラルキーに入れることでサイズの問題を解決する予定
        GameObject target = GameObject.Instantiate(m_targetPrefab);
        target.transform.position = pos;
        */
        return go;
    }
    private void SetHome( Vector3 pos , int id )
    {
        //家にいる人数の取得
        int animal_count = getHomeCount(id);
        //人数から動物を取得
        int r = getAnimalFromCount( animal_count );

        //真ん中の動物を生成
        Vector3[] position = new Vector3[] { 
            pos ,
            pos + new Vector3(3.0f      , +0.3f + Random.Range(0.0f,1.0f)   , 0),
            pos + new Vector3(-3.0f     , +0.3f + Random.Range(0.0f,1.0f)   , 0),
            pos + new Vector3(-5.9f     , +0.9f + Random.Range(0.0f,1.0f)   , 0),
            pos + new Vector3(5.9f      , +0.9f + Random.Range(0.0f,1.0f)   , 0),
        };
        int i = 0;
        for (i = 0; i < animal_count; i++)
        {
            AnimalInstantiate(m_charaPrefabs[r], position[i] , id, transform);
        }

        //まだ入れる余裕があれば，一定の確率で狐が登場
        if( i < position.Length )
        {
             if( m_enemyRate > Random.Range(0.0f,1.0f) )
             {
                 AnimalInstantiate(m_enemyPrefab, position[i], id, transform);
             }
        }

        //木の挿入
        state_manager.Instance.m_backgroudManager.GetComponent<background_manager>().AddTree(pos);
    }

    private void SetBackHome(Vector3 pos , int i)
    {
        GameObject go = GameObject.Instantiate(m_homePrefabs[i]);
        pos.y = go.transform.position.y;
        go.transform.position = pos;
    }

    public void reset()
    {
        foreach( Transform tf in transform )
        {
            tf.GetComponent<home>().Start();
        }

    }

    void SetBackHomes( Vector3 pos ,  int r )
    {
        if (r < 3)
        {
            SetBackHome(pos, 0);
        }
        else if (r < 5)
        {
            SetBackHome(pos, 1);
        }
        else
        {
            SetBackHome(pos, 2);
        }

    }

	// Use this for initialization
	void Start () {

        for (int i = 0; i < m_vhomeCounts.Length ; i++ ){


            //家の中の動物を決める
            SetHome(new Vector3(i * turu_manager.Offset, -1.65f, 1), i);

            //背景の家をランダム表示
            int r = (int)m_vhomeCounts[i];
            SetBackHomes(new Vector3(i * turu_manager.Offset, -1.65f, 1), r);

        }

        reset();
	
	}
	
    public void Failue( Transform obj )
    {
        state_manager.Instance.m_effectManager.GetComponent<effect_manager>().AddSorry(obj.transform.position.x);
        state_manager.Instance.m_effectManager.GetComponent<effect_manager>().AddSorry(Camera.main.transform.position.x);
        state_manager.Instance.m_comboManager.GetComponent<combo_manager>().Failure(0);
        obj.GetComponent<home>().Supply();
    }

	// Update is called once per frame
	void Update () {
	
        //check failue 失敗したら，コンボをやめて，すべて配ったことにする．
        foreach (Transform obj in transform)
        {
            if( obj.GetComponent<home>().isFailue() )
            {
                Failue( obj );
            }
        }
	}
}
