using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class main_button_click : MonoBehaviour {

    public bool m_isUpButton = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        bool flag = false;
        GameObject saru = Camera.main.GetComponent<position_fix>().target;
        foreach (Transform obj in state_manager.Instance.m_homeManager.transform)
        {
            if( obj.GetComponent<check_hit>().isIn(saru)  )
            {
                flag = true;
                break;
            }           
        }
        if( flag )
        {
            GetComponent<Button>().image.color = new Color(1.0f,0.5f,0.5f);
            GetComponent<jump>().isEndles = true;
        }
        else
        {
            GetComponent<Button>().image.color = new Color(1.0f, 1.0f, 1.0f);
            GetComponent<jump>().isEndles = false;
        }
    
    }

    bool isEnableClick()
    {
        //エンドレスの時というのは選択できるということ
        return GetComponent<jump>().isEndles;
    }

    /// ボタンをクリックした時の処理
    public void OnClick()
    {
        if (!isEnableClick()) return;

        GameObject saru = Camera.main.GetComponent<position_fix>().target;

        //サルを配達にする
        saru.GetComponent<saru_state>().IsDelivery = true;

        //ループして当たったものを探している
        foreach( Transform obj in state_manager.Instance.m_homeManager.transform )
        {
            if (obj.GetComponent<check_hit>().hit(saru))
                break;

        }
    }
}
