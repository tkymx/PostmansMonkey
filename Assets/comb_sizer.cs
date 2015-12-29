using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class comb_sizer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        int comboLebel = state_manager.Instance.m_comboManager.GetComponent<combo_manager>().getComboLebel();
        transform.localScale = new Vector3(1, 1, 1) * ( 1 + comboLebel * 0.5f );

        switch( comboLebel )
        {
            case 0:
                transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
                break;
            case 1:
                transform.GetComponent<Image>().color = new Color(0.0f , 1.0f, 0.0f);
                break;
            case 2:
                transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 0.0f);
                break;
            case 3:
                transform.GetComponent<Image>().color = new Color(1.0f, 0.8f, 0.8f);
                break;
        }
    	
	}
}
