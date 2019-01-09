using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class LowerAlphabet : MonoBehaviour {
    public GameObject self_obj;
    public bool visible = true;
    AudioSource audio_source_camera;

    void Start(){
        audio_source_camera = GameObject.Find("/MainCamera").GetComponent<AudioSource>();
    }
    public void hide_alphabet()
    {
        if(visible){
            audio_source_camera.Play();
            var text_obj = GetComponentsInChildren<Text>();
            /*GetComponent<Image>().color = new Color(0F,
                                                    0.5056266F,
                                                    0.8490566F,
                                                    1F);
            text_obj[0].color = new Color(0F,
                                        0.5056266F,
                                        0.8490566F,
                                        1F);*/
            self_obj.SetActive(false);
            visible = false;
            var te = GameObject.Find("/Game Manager");
            te.GetComponent<communicator>().transfer_bottom2top(text_obj[0].text);
        }
    }

    public void show_alphabet(){
        if (!visible)
        {
            var text_obj = GetComponentsInChildren<Text>();
            /*GetComponent<Image>().color = new Color(0.01059096F,
                                                    0.1320755F,
                                                    0.01059096F,
                                                    1F);
            text_obj[0].color = new Color(1F, 1F, 1F, 1F);*/

            self_obj.SetActive(true);
            visible = true;
        }
    }
}
