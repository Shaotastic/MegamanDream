using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour {

	// Player player;
	[SerializeField] PlayerBar m_Bar;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		m_Bar.SetFillAmount(GameManager.Instance.GetSkillTime);
	}
}
