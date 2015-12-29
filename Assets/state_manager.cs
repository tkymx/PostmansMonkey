using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class state_manager : SingletonMonoBehaviour<state_manager> {

    private bool m_isGameOver;
    public bool IsGameOver() { return m_isGameOver; }
    public void SetGameOver(bool b) { m_isGameOver = b; }

    private int m_score;
    private int[] m_suppy_count = new int[3];

    class high_score
    {        
        const string HIGH_SCORE_KEY = "highScore";
        public void SaveHighScore(int score)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
            PlayerPrefs.Save();
        }
        public int LoadHighScore()
        {
            return PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        }
    };
    private high_score m_hight_score = new high_score();

    public void AddScore(int s) { m_score += s; }
    public void AddSupply(int rank) { m_suppy_count[rank-1]++; }
    public int getSupply(int rank) { return m_suppy_count[rank-1]; }
    public int AllSupply() { return m_suppy_count[0] + m_suppy_count[1] + m_suppy_count[2]; }

    public GameObject m_scoreText;
    public GameObject m_resultScene;
    public GameObject m_comboManager;
    public GameObject m_backgroudManager;
    public GameObject m_homeManager;
    public GameObject m_turuManager;
    public GameObject m_effectManager;

    public GameObject m_titleUI;
    public GameObject m_playUI;
    public GameObject m_tutorialUI;
    public GameObject m_tutorialUI2;
    public GameObject m_countUI;

	// Use this for initialization
	void Start () {

        m_score = 0;
        m_suppy_count[0] = 0;
        m_suppy_count[1] = 0;
        m_suppy_count[2] = 0;

        m_isGameOver = false;
        m_resultScene.SetActive(false);

        count = 20;
	}

    int count = 0;

    int state = load_state;
    const int load_state = -3;
    const int title_state = -2;
    const int init_state = -1;
    const int tutorial_state = 3;
    const int tutorial_2_state = 5;
    const int count_state = 4;
    const int start_state = 0;
    const int play_state = 1;
    const int result_state = 2;

    public void reset()
    {
        Start();
        m_turuManager.GetComponent<turu_manager>().reset();
        m_comboManager.GetComponent<combo_manager>().reset();
        m_homeManager.GetComponent<home_manaager>().reset();
    }

    public void tutorial_show(int i)
    {
        switch(i)
        {
            case 1:
                m_tutorialUI.SetActive(true);
                m_tutorialUI2.SetActive(false);
                break;
            case 2:
                m_tutorialUI.SetActive(false);
                m_tutorialUI2.SetActive(true);
                break;
            case 0:
                m_tutorialUI.SetActive(false);
                m_tutorialUI2.SetActive(false);
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {

        //state machine

        switch( state )
        {
            case load_state:

                m_playUI.SetActive(false);
                m_titleUI.SetActive(true);
                m_resultScene.SetActive(false);
                tutorial_show(0);
                m_countUI.SetActive(false);

                m_turuManager.GetComponent<turu_manager>().pause(true);

                m_titleUI.transform.FindChild("HighScore").GetComponent<Text>().text
                    = m_hight_score.LoadHighScore().ToString();

                state = title_state;

                break;
            case title_state:

                m_playUI.SetActive(false);
                m_titleUI.SetActive(true);
                m_resultScene.SetActive(false);
                tutorial_show(0);
                m_countUI.SetActive(false);

                m_turuManager.GetComponent<turu_manager>().pause(true);

                if (Input.GetMouseButtonDown(0))
                {
                    state = tutorial_state;
                }

                break;
            case tutorial_state:

                m_playUI.SetActive(false);
                m_titleUI.SetActive(false);
                m_resultScene.SetActive(false);
                tutorial_show(1);
                m_countUI.SetActive(false);

                m_turuManager.GetComponent<turu_manager>().pause(true);

                if (Input.GetMouseButtonDown(0))
                {
                    state = tutorial_2_state;
                }

                break;
            case tutorial_2_state:

                m_playUI.SetActive(false);
                m_titleUI.SetActive(false);
                m_resultScene.SetActive(false);
                tutorial_show(2);
                m_countUI.SetActive(false);

                m_turuManager.GetComponent<turu_manager>().pause(true);

                if (Input.GetMouseButtonDown(0))
                {
                    state = count_state;
                    m_countUI.GetComponent<count_down>().CountDownStart();
                }

                break;
            case count_state:

                m_playUI.SetActive(false);
                m_titleUI.SetActive(false);
                m_resultScene.SetActive(false);
                tutorial_show(0);
                m_countUI.SetActive(true);

                if( m_countUI.GetComponent<count_down>().isCountDownEnd() )
                {
                    m_turuManager.GetComponent<turu_manager>().pause(true);
                    state = init_state;
                }

                break;
            case init_state:
                    
                m_playUI.SetActive(true);
                m_titleUI.SetActive(false);
                m_resultScene.SetActive(false);
                tutorial_show(0);
                m_countUI.SetActive(false);

                m_turuManager.GetComponent<turu_manager>().pause(true);
                m_turuManager.GetComponent<turu_manager>().Hint(0);
                state = start_state;

                break;
            case start_state:

                m_playUI.SetActive(true);
                m_titleUI.SetActive(false);
                m_resultScene.SetActive(false);
                tutorial_show(0);
                m_countUI.SetActive(false);

                //start
                if (--count < 0)
                {
                    m_turuManager.GetComponent<turu_manager>().pause(false);
                    state = play_state;
                }

                break;
            case play_state:

                m_playUI.SetActive(true);
                m_titleUI.SetActive(false);
                m_resultScene.SetActive(false);
                tutorial_show(0);
                m_countUI.SetActive(false);

                m_scoreText.GetComponent<Text>().text = m_score.ToString();
                if (m_isGameOver)
                {
                    m_hight_score.SaveHighScore( Mathf.Max( m_hight_score.LoadHighScore() , m_score ) );
                    state = result_state;
                }

                break;
            case result_state:
                                
                m_playUI.SetActive(false);
                m_titleUI.SetActive(false);
                m_resultScene.SetActive(true);
                tutorial_show(0);
                m_countUI.SetActive(false);

                m_turuManager.GetComponent<turu_manager>().pause(true);

                m_resultScene.transform.FindChild("TotalScore").GetComponent<Text>().text = AllSupply().ToString();
                m_resultScene.transform.FindChild("RealScore").GetComponent<Text>().text = m_score.ToString();
                m_resultScene.transform.FindChild("MaxCombo").GetComponent<Text>().text = m_comboManager.GetComponent<combo_manager>().getMaxComboCount().ToString();

                m_resultScene.transform.FindChild("GreatScore").GetComponent<Text>().text = getSupply(1).ToString();
                m_resultScene.transform.FindChild("SuperScore").GetComponent<Text>().text = getSupply(2).ToString();
                m_resultScene.transform.FindChild("OKScore").GetComponent<Text>().text = getSupply(3).ToString();


                if (Input.GetMouseButtonDown(0))
                {
                    reset();
                    state = load_state;
                }

                break;
        }
	}
}
