using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class checkWord : MonoBehaviour {

    string image_name;
	public Animator anim;

	// Use this for initialization


	public void check(GameObject[] objects){

        image_name = GetComponent<StartGame>().image_name;
        image_name = Regex.Replace(image_name, @"[^0-9a-zA-Z]+", "").ToUpper();

		string word = "";
		for(int i=0; i<objects.Length; i++){
            var text_obj = objects[i].GetComponentsInChildren<Text>();
			word += text_obj[0].text;
		}
		if(word == image_name){
			Debug.Log("Correct");
            GetComponent<color_changer>().changeColor_bottomBackground("correct_");
            GetComponent<ShuffleLogic>().remove_solved();
			Invoke("next_image", 0.8f);
		}else{
			Handheld.Vibrate();
            anim.SetTrigger("shake");
			GetComponent<color_changer>().changeColor_bottomBackground("wrong_");
		}
	}

	void next_image(){
        GetComponent<ShuffleLogic>().next_item();
	}
}
