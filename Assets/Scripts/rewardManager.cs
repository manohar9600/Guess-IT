using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class rewardManager : MonoBehaviour {
	GameObject AdManager;

	void Start(){
		AdManager = GameObject.Find("/AdManager");
	}
	public void hint_fun(){
		int hints = GetComponent<userData>().hints_left;
		if(hints > 0){
			reveal_letters();
            hints -= 1;
            GetComponent<userData>().hints_left = hints;
		}else{
			//fhhf
			AdManager.GetComponent<adManager>().RewardVideo();
		}
	}

	public void reveal_letters(){
		string image_name = GetComponent<StartGame>().image_name;
        image_name = Regex.Replace(image_name, @"[^0-9a-zA-Z]+", "").ToUpper();

		var name_alphabets = GetComponent<communicator>().name_alphabets;
		var lower_alphabets = GetComponent<communicator>().lower_alphabets;
		int mismatch_ind = get_mismatch_indice(image_name, name_alphabets);
		remove_wrongAlphabets(mismatch_ind, name_alphabets);
		show_correctAlphabets(mismatch_ind, lower_alphabets, image_name);

        GetComponent<userData>().hint_used();
	}

	int get_mismatch_indice(string image_name, GameObject[] name_alphabets){
        int i = 0;
        for (i = 0; i < name_alphabets.Length; i++)
        {
            var text_obj = name_alphabets[i].GetComponentsInChildren<Text>();
            if (text_obj[0].text.ToCharArray().Length == 0)
            {
                break;
            }
            if (text_obj[0].text.ToCharArray()[0] != image_name[i])
            {
                break;
            }
        }
		return i;
	}

	void remove_wrongAlphabets(int mismatch_ind, GameObject[] name_alphabets){
		for(int i=name_alphabets.Length-1; i>=mismatch_ind; i--){
            var text_obj = name_alphabets[i].GetComponentsInChildren<Text>();
            if (text_obj[0].text.ToCharArray().Length == 0){
				continue;
			}
			GetComponent<communicator>().transfer_top2bottom();
		}
	}

    void show_correctAlphabets(int mismatch_ind, GameObject[] lower_alphabets,
								string image_name)
	{
		int te = (int)(lower_alphabets.Length / 3);
        for (int i = mismatch_ind; i < mismatch_ind+te && 
									i < lower_alphabets.Length; i++)
		{
			var alphabet = image_name[i].ToString();
            for (int j = 0; j < lower_alphabets.Length; j++)
            {
                var te1 = lower_alphabets[j].GetComponentsInChildren<Text>();
                var te2 = lower_alphabets[j].GetComponentInChildren<LowerAlphabet>();
                if (te1[0].text == alphabet && te2.visible)
                {
                    te2.hide_alphabet();
                    break;
                }
            }
		}
	}
}
