using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StartGame : MonoBehaviour {
    public Sprite border_image;

	public string image_name;
    public Animator anim;

	// Use this for initialization
	void Start () {
		anim.Play("startLogo");
	}

	public void generate(string img_name){
		GetComponent<communicator>().destroy_all();
		GetComponent<color_changer>().changeColor_bottomBackground("default_");

		this.image_name = img_name;
		GameObject[] upper_alphabets;
        upper_alphabets = GetComponent<UpperAlphabetGenerator>().generate(image_name);
        upper_alphabets[0].GetComponentsInChildren<Image>(true)[0].sprite = border_image;
        GetComponent<communicator>().name_alphabets = upper_alphabets;

        GameObject[] lower_alphabets;
        lower_alphabets = GetComponent<LowerAlphabetGenerator>().generate(image_name);
        GetComponent<communicator>().lower_alphabets = lower_alphabets;		
	}
}
