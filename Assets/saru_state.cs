using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class saru_state : MonoBehaviour {

    public Sprite m_saruDefault;
    public Sprite m_saruFlaying;
    public Sprite m_saruDelivery;

    class Count
    {
        private int m_max_count;
        private int m_current_count;

        public void startCount( int c )
        {
            m_max_count = c;
            m_current_count = 0;
        }
        public bool updateCount()
        {
            if (++m_current_count > m_max_count)
                return true;
            return false;
        }
        
    }

    private bool m_isSaruFlaying;
    private Transform parent_saru;
    public bool IsSaruFlaying{
      get { return m_isSaruFlaying; }
      set { 
              if ( value == true)
              {
                  parent_saru = transform.parent;
                  m_isSaruFlaying = true;
              }
              else
              {
                  if(parent_saru != transform.parent)
                  {
                      m_isSaruFlaying = false;
                  }
              }
          }
    }

    private bool m_isDelivery;
    private Count m_deliveryCount = new Count();
    public bool IsDelivery
    {
        get { return m_isDelivery; }
        set { 
            m_isDelivery = value;
            if (value == true)
                m_deliveryCount.startCount(4);
        }
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (IsSaruFlaying)
        {
            GetComponent<SpriteRenderer>().sprite = m_saruFlaying;
        }
        else if(IsDelivery)
        {
            GetComponent<SpriteRenderer>().sprite = m_saruDelivery;
            if( m_deliveryCount.updateCount() )
            {
                IsDelivery = false;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = m_saruDefault;
        }
	
	}


}
