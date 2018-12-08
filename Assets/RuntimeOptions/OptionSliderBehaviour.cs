using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSliderBehaviour : MonoBehaviour {

	public Text minval;
	public Text maxval;
	public Text curval;

	public Text title;

	public Slider slider;


	public void Start() {
		title.text = slider.onValueChanged.GetPersistentMethodName(0);
		minval.text = slider.minValue+"";
		maxval.text = slider.maxValue+"";
		slider.onValueChanged.AddListener((v)=>
		{
			curval.text = v+"";
		});
	}
}
