using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.IO;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour {

	public Animator anim;
	public float anim_speed;
	GameObject te;
    AudioSource audio_source_camera;
	int a = 0;

	void Start(){
		anim.speed = anim_speed;
        te = GameObject.Find("/AdManager");
        audio_source_camera = GameObject.Find("/MainCamera").GetComponent<AudioSource>();
	}

	public void start_button(){
        audio_source_camera.Play();
		var temp = GetComponent<ShuffleLogic>().imageName_arr;
        int i = 0;
        for (i = 0; i < 3; i++)
        {
            if (string.IsNullOrEmpty(temp[i]))
            {
                break;
            }
        }
		if(i != 3){
            GetComponent<ShuffleLogic>().load_next(i);
		}
		if(a == 0){
			if(!string.IsNullOrEmpty(temp[0])){
                anim.Play("fadeIn");
                te.GetComponent<adManager>().RequestBanner();
                GetComponent<ShuffleLogic>().next_item();
                a = 1;
			}else{
				//play animation
                Invoke("start_button", 5f);
			}
		}else{
            anim.Play("fadeIn");
            te.GetComponent<adManager>().RequestBanner();
		}
	}

    public void stop_button()
    {
        audio_source_camera.Play();
		anim.Play("fadeOut");
        te.GetComponent<adManager>().destroy_banner();
    }

	public void backspace(){
        audio_source_camera.Play();
		GetComponent<color_changer>().changeColor_bottomBackground("default_");
		GetComponent<communicator>().transfer_top2bottom();
	}

	public void hint_button(){
        audio_source_camera.Play();
		GetComponent<rewardManager>().hint_fun();
		// none
		GetComponent<userData>().update_ui();
	}

	public void exit_button(){
        audio_source_camera.Play();
        Application.Quit();
	}

	bool sound_enabled = true;
	public Sprite sound_on;
	public Sprite sound_off;
	public Image sound_image;
	public void toggle_sound(){
        audio_source_camera.Play();
		if(sound_enabled){
			sound_image.sprite = sound_off;
            audio_source_camera.enabled = false;
            sound_enabled = false;
		}else{
            sound_image.sprite = sound_on;
            audio_source_camera.enabled = true;
            sound_enabled = true;
		}
	}
}
