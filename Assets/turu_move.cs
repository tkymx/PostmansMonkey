using UnityEngine;
using System.Collections;

public class turu_move : MonoBehaviour {

    public float defaultDeg = 280;
    public float maxhVelocity = 20.0f;
    public GameObject nextObject = null;

    private bool is_right_swing;
    private float smoothVelocity = 1.0f;

    public bool pause = false;

	// Use this for initialization
	public void Start () {

        transform.rotation = Quaternion.AngleAxis( defaultDeg , Vector3.forward);
        is_right_swing = true;

        isNextAppear = false;
        isEndAppear = false;
	
	}

    private bool isNextAppear = false;
    private void OnNext()
    {
        GetComponent<turu_info>().HintNextHomes();
    }

    private bool isEndAppear = false;
    private void OnEnd()
    {
        GetComponent<turu_info>().HintBack();
    }

    // Update is called once per frame
	void Update () {

        if (pause) return;

        float startDeg = defaultDeg;
        float endDeg = 360 - defaultDeg;

        if (startDeg < endDeg){
            float t = startDeg;
            startDeg = endDeg;
            endDeg = t;
        }
            

        if (is_right_swing){

            transform.rotation = Quaternion.AngleAxis(
                Mathf.SmoothDampAngle(
                    transform.rotation.eulerAngles.z,
                    endDeg,
                    ref smoothVelocity, 0.1f, maxhVelocity)
                , Vector3.forward);

            if (Mathf.Abs(transform.rotation.eulerAngles.z - endDeg) < 1){
                if( nextObject != null ){
                    Transform tf = transform.FindChild("top").transform.FindChild("saru(Clone)");
                    if( tf!= null){
                        tf.SetParent( nextObject.transform.FindChild("top").transform , true );
                        nextObject = null;
                    }
                } else {
                    Transform tf = transform.FindChild("top").transform.FindChild("saru(Clone)");
                    if (tf != null){
                        state_manager.Instance.SetGameOver(true);
                    }

                }
                is_right_swing = false;
            }

            //ヒントを出すタイミング
            else if (Mathf.Abs(transform.rotation.eulerAngles.z - endDeg) < 15){
                if (nextObject != null){
                    Transform tf = transform.FindChild("top").transform.FindChild("saru(Clone)");
                    if (tf != null)
                        if (!isNextAppear)
                        {
                            OnNext();
                            isNextAppear = true;

                            tf.GetComponent<saru_state>().IsSaruFlaying = true;
                        }
                }
            }

            //終了のタイミング
            else if (Mathf.Abs(transform.rotation.eulerAngles.z - endDeg) < 35)
            {
                Transform tf = transform.FindChild("top").transform.FindChild("saru(Clone)");
                if (tf != null)
                    if (!isEndAppear)
                    {
                        OnEnd();
                        isEndAppear = true;
                    }
            }
        }
        else{

            transform.rotation = Quaternion.AngleAxis(
                Mathf.SmoothDampAngle(
                    transform.rotation.eulerAngles.z,
                    startDeg,
                    ref smoothVelocity, 0.1f, maxhVelocity)
                , Vector3.forward);

            if (Mathf.Abs(transform.rotation.eulerAngles.z - startDeg) < 1)
            {
                isEndAppear = false;
                isNextAppear = false;
                is_right_swing = true;
            }
        }
	
	}
}
