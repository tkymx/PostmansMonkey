using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * コンボマネージャー
 *  クリックされたときにコンボを追加する．コンボが続いていたらそのエフェクトを出して，フィーバーになったら
 *  何かエフェクトを出す．ただそれだけ
 * 
 * */

public class combo_manager : MonoBehaviour {

    const int FirstSpeed = 5;
    const int SecondSpeed = 10;
    const int ThiredSpeed = 15;

    private int m_comboCountMax;
    private int m_comboCount;
    public void Success(int type)
    {
        m_comboCount++;
        m_comboCountMax = Mathf.Max(m_comboCountMax, m_comboCount);

        //スピードアップ処理
        if (getComboCount() == ThiredSpeed || getComboCount() == SecondSpeed || getComboCount() == FirstSpeed)
            state_manager.Instance.m_effectManager.GetComponent<effect_manager>().AddSpeedUpWind(Camera.main.GetComponent<position_fix>().target.transform.position.x, getComboLebel());

        //jump
        m_normalComboEffect.GetComponent<comb_mover>().jump();
    }
    public void Failure(int type)
    {
        m_comboCount = 0;
    }
    public int getComboCount()
    {
        return m_comboCount;
    }
    public int getMaxComboCount()
    {
        return m_comboCountMax;
    }

    public int getComboLebel()
    {
        if (getComboCount() >= ThiredSpeed)
            return 3;
        if (getComboCount() >= SecondSpeed)
            return 2;
        if (getComboCount() >= FirstSpeed)
            return 1;
        return 0;
    }

    public GameObject m_normalComboEffect = null;

    public void reset()
    {
        Start();
    }

	// Use this for initialization
	void Start () {
        m_comboCount = 0;
        m_comboCountMax = 0;
        m_normalComboEffect.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

            if(m_comboCount > 0)
            {
                m_normalComboEffect.SetActive(true);

                //コンボの位置を設定
                m_normalComboEffect.GetComponent<comb_mover>().setComobOriginPosition( 
                    Camera.main.WorldToScreenPoint( Camera.main.GetComponent<position_fix>().target.transform.position ) + 
                    new Vector3(
                        Screen.width/5,
                        Screen.height/7));

                if (m_normalComboEffect.GetComponentInChildren<Text>() != null)
                    m_normalComboEffect.GetComponentInChildren<Text>().text = m_comboCount.ToString();
            }
            else
            {
                m_normalComboEffect.SetActive(false);
            }
	
	}
}
