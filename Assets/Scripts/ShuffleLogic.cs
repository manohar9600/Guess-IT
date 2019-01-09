using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class ShuffleLogic : MonoBehaviour {
	public Sprite prev_sprite;
	public GameObject Fire_base;
	public Image panel_img;
    // string[] folders_names = { "Fruits_Veg" };
    string[] folders_names = {"Animals", "Countries", "Fruits_Veg", "icons", "Objects"};
    public TextAsset[] csv_files;

	Sprite[] sprite_arr = new Sprite[3];
	public string[] imageName_arr = new string[3];
	List<string>[] image_folders = new List<string>[5];
    public List<int>[] indices_to_solve = new List<int>[5];
    List<int> next_indice = new List<int>();
    List<int> next_bucket = new List<int>();

	void Start(){
		load_csv();
        GetComponent<userData>().Load_fromSave();
        load_next(0);
        //load_next();
	}

	void load_csv(){

		for(int i=0; i<csv_files.Length; i++){

            List<string> name_list = new List<string>();
            List<int> difficulty = new List<int>();
            List<int> indices = new List<int>();

            var lines = csv_files[i].text.Split(Environment.NewLine.ToCharArray());
            int count = 0;
            for(int j=0; j<lines.Length; j++){
                if(!string.IsNullOrEmpty(lines[j])){
                    var words = lines[j].Split(',');
                    name_list.Add(words[0]);
                    indices.Add(count);
                    count += 1;
                }
            }

            image_folders[i] = name_list;
            indices_to_solve[i] = indices;
		}

        GetComponent<userData>().Save_newgame(); // wont save if file already exists
	}

	public void receive_sprite(Sprite img, string img_name, int tem_ind, int ind,
                                int ind_num)
    {
        if(img == prev_sprite){
            load_next(tem_ind);
        }else{
            sprite_arr[tem_ind] = img;
            imageName_arr[tem_ind] = img_name;
            prev_sprite = img;
        }
        next_bucket.Add(ind);
        next_indice.Add(ind_num);
	}

	public void load_next(int num){
        var rand = new System.Random();
        int ind = rand.Next(folders_names.Length);
        var rand2 = new System.Random();
        int te = rand2.Next(indices_to_solve[ind].Count);
        int ind_num = indices_to_solve[ind][te];
        string img_name = image_folders[ind][ind_num];
		Fire_base.GetComponent<firebase_bucket>().
                    get_next(folders_names[ind], img_name, ".png", num, ind,
                    ind_num);
	}

	void next_(){
        sprite_arr[0] = sprite_arr[1];
        sprite_arr[1] = sprite_arr[2];
        imageName_arr[0] = imageName_arr[1];
        imageName_arr[1] = imageName_arr[2];
        imageName_arr[2] = String.Empty;
        int i=0;
        for(i=0; i<3; i++){
            if(string.IsNullOrEmpty(imageName_arr[i])){
                break;
            }
        }
		load_next(i);
	}

    public void next_item()
    {
		if(string.IsNullOrEmpty(imageName_arr[0])){
            load_next(0);
		}
        GetComponent<StartGame>().generate(imageName_arr[0]);
        panel_img.sprite = sprite_arr[0];
		next_();
    }

    public void remove_solved(){
        indices_to_solve[next_bucket[0]].Remove(next_indice[0]);
        next_bucket.RemoveAt(0);
        next_indice.RemoveAt(0);
        GetComponent<userData>().solved();
    }

	int random_next_indice(int ind){
		var rand = new System.Random();
		int num = rand.Next(indices_to_solve[ind].Count);
		return num;
	}
}

