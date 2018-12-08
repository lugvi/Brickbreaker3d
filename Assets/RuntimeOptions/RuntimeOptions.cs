using UnityEngine;
using UnityEngine.UI;

public class RuntimeOptions : MonoBehaviour {

	public Button optionsButton;


	public GameObject optionsPanel;


	// Use this for initialization
	void Start () {
		optionsButton.onClick.AddListener(()=>
		{
			optionsPanel.SetActive(!optionsPanel.activeInHierarchy);
			if(optionsPanel.activeInHierarchy)
				Time.timeScale = 0;
			else
				Time.timeScale = 1;
		});
	}
	
}
