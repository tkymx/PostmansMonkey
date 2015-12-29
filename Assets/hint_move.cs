using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class hint_move : MonoBehaviour {

    private int m_nowHagakiCount;
    public void SetHagakiCount( int i )
    {
        m_nowHagakiCount = i;
    }

    public void Supply()
    {
        m_nowHagakiCount--;
    }

    void OnCompleteHandler(){
    }

    bool isFoward = true;

    public void MoveStart(int i)
    {
        transform.position = new Vector3(-70, transform.position.y );
        SetHagakiCount(i);

        isFoward = true;
    }

    public void MovebackStart()
    {
        isFoward = false;
    }

    // Use this for initialization
	void Start () {
        isFoward = true;
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.FindChild("Text").GetComponent<Text>().text =  m_nowHagakiCount.ToString();

        if( isFoward )
            iTween.MoveTo( this.gameObject , iTween.Hash(
                "x", Screen.width/5,
                "time", 0.8f,
                "oncomplete", "OnCompleteHandler",
                "oncompletetarget", this.gameObject));
        else
            iTween.MoveTo(this.gameObject, iTween.Hash(
                "x", -70,
                "time", 0.8f,
                "oncomplete", "OnCompleteHandler",
                "oncompletetarget", this.gameObject));
	
	}
}
