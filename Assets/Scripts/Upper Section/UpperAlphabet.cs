using UnityEngine;
using UnityEngine.UI;

public class UpperAlphabet : MonoBehaviour {
	public bool visible = false;
	public void hide_alphabet(){
		if(visible){
            GetComponent<Image>().color = new Color(0.9490196F,
                                            0.9490196F,
                                            0.9647059F,
                                            1F);
            var text_obj = GetComponentsInChildren<Text>();
            text_obj[0].text = "";
            visible = false;
		}
	}

	public void show_alphabet(string val){
		if(!visible){
            GetComponent<Image>().color = new Color(0.01960784F,
                                                    0.427451F,
                                                    0.7098039F,
                                                    1F);
            var text_obj = GetComponentsInChildren<Text>();
            text_obj[0].text = val;
            visible = true;
		}
	}
}
