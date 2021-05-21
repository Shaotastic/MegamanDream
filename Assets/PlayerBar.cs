using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class PlayerBar : MonoBehaviour
{
    [SerializeField] Image m_LeftBar, m_RightBar;

    [Range(0,1)]
    [SerializeField] float m_FillAmount;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBars();

    }

    void UpdateBars()
    {
        if (m_LeftBar)
        {
            m_LeftBar.fillAmount = m_FillAmount;

            UpdateBarColor(m_LeftBar);
        }
        if (m_RightBar)
        {
            m_RightBar.fillAmount = m_FillAmount;
            UpdateBarColor(m_RightBar);
        }
    }

    void UpdateBarColor(Image bar)
    {
        if (m_LeftBar.fillAmount != 1)
            bar.color = Color.red;
        else
            bar.color = Color.green;
    }


    public void SetFillAmount(float amount)
    {
        m_FillAmount = amount;
    }
}
