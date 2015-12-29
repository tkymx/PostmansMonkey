using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class count_down : MonoBehaviour {

    public Sprite m_thied;
    public Sprite m_second;
    public Sprite m_first;

	// Use this for initialization
	void Start () {
	
	}

    int state = 0;
    const int init_state = -1;
    const int third_state = 0;
    const int second_state = 1;
    const int first_state = 2;
    const int end_state = 3;

    public void CountDownStart()
    {
        state = init_state;
    }

    public bool isCountDownEnd()
    {
        return state == end_state;
    }
	
    void end()
    {
        transform.Find("Image").localScale = new Vector3(1, 1, 1);
        switch (state)
        {
            case third_state:
                state = second_state;
                break;
            case second_state:
                state = first_state;
                break;
            case first_state:
                state = end_state;
                break;
        }
        
    }

	// Update is called once per frame
	void Update () {

        switch (state)
        {
            case third_state:
                GetComponentInChildren<Image>().sprite = m_thied;
                break;
            case second_state:
                GetComponentInChildren<Image>().sprite = m_second;
                break;
            case first_state:
                GetComponentInChildren<Image>().sprite = m_first;
                break;
        }

        switch(state)
        {
            case init_state:
                transform.Find("Image").localScale = new Vector3(1, 1, 1);
                state = third_state;
                break;

            case third_state:
            case second_state:
            case first_state:
                iTween.ScaleTo( transform.Find("Image").gameObject , iTween.Hash(
                    "scale", new Vector3(2, 2, 2),
                    "time", 0.8f,
                    "oncomplete", "end",
                    "oncompletetarget", this.gameObject));
                break;

            case end_state:
                break;
        }

	
	}
}
