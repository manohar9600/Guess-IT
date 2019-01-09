using UnityEngine;
using UnityEngine.UI;

public class color_changer : MonoBehaviour {
	public Image bottom_background;

	public void changeColor_bottomBackground(string change_to){
		var default_blue = new Color(0.01668743f,
                                    0.4273829f,
                                    0.7075472f,
									1f);
		var wrong_red = new Color(0.9294118f,
                                    0.1098039f,
                                    0.1411765f,
									1f);
		var correct_green = new Color(0.1294118f,
                                    0.7372549f,
                                    0.003921569f,
									1f);
		if(change_to == "default_"){
			bottom_background.color = default_blue;
		}

		if(change_to == "wrong_"){
			bottom_background.color = wrong_red;
		}

		if(change_to == "correct_"){
			bottom_background.color = correct_green;
		}
	}
}
