using UnityEngine;
using UnityEngine.UI;
using System;


public class communicator : MonoBehaviour {
	public GameObject[] name_alphabets;
	public GameObject[] lower_alphabets;
	int cursor_pos = 0;
	public Sprite border_image;
	public Sprite no_border_image;

	public void transfer_bottom2top(string val){
        var te = name_alphabets[cursor_pos].GetComponentsInChildren<UpperAlphabet>();
        te[0].show_alphabet(val);
        name_alphabets[cursor_pos].GetComponentsInChildren<Image>(true)[0].sprite = no_border_image;
		cursor_pos += 1;
		if(cursor_pos == name_alphabets.Length){
			GetComponent<checkWord>().check(name_alphabets);
		}else{
			name_alphabets[cursor_pos].GetComponentsInChildren<Image>(true)[0].sprite = border_image;
		}
	}

	public void transfer_top2bottom(){
		if(cursor_pos <= 0){
			return;
		}	
		var val = name_alphabets[cursor_pos-1].GetComponentsInChildren<Text>()[0];
		for(int i=0; i<lower_alphabets.Length; i++){
			var te = lower_alphabets[i].GetComponentsInChildren<Text>(true);
            var te2 = lower_alphabets[i].GetComponentInChildren<LowerAlphabet>(true);
			if(te[0].text == val.text && !te2.visible){
				te2.show_alphabet();
				break;
			}
		}
        name_alphabets[cursor_pos - 1].GetComponentsInChildren<UpperAlphabet>()[0].hide_alphabet();
        if (cursor_pos != name_alphabets.Length)
        {
            name_alphabets[cursor_pos].GetComponentsInChildren<Image>(true)[0].sprite = no_border_image;
        }
        cursor_pos -= 1;
        name_alphabets[cursor_pos].GetComponentsInChildren<Image>(true)[0].sprite = border_image;
	}

	public void destroy_all(){
		for(int i=0; i<name_alphabets.Length; i++){
			Destroy(name_alphabets[i]);
			Destroy(lower_alphabets[i]);
		}
		cursor_pos = 0;
	}
}
