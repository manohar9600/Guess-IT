using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class userData : MonoBehaviour {
	public int hints_left;
	public int total_hints_used;
	public int total_images_solved;
	public int number_of_folders;
    player_info data = new player_info();
	public Text total_solved;
	public Text hints_used;
	public Text hints_left_txt;

	// Use this for initialization
	void Start () {
        number_of_folders = GetComponent<ShuffleLogic>().csv_files.Length;
	}

	public void Save_newgame(){
        string path = Application.persistentDataPath + "/mama.banana";
		if(!File.Exists(path)){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(path, FileMode.Create);
			data.number_hints_left = 3;
			data.total_hints_used = 0;
			data.total_images_solved = 0;
			data.indices_toSolve = GetComponent<ShuffleLogic>().indices_to_solve;
			data.data_structure = GetComponent<ShuffleLogic>().data_structure;
			bf.Serialize(file, data);
			file.Close();
		}	
		update_ui();
	}

	public void Load_fromSave(){
        string path = Application.persistentDataPath + "/mama.banana";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            data = (player_info)bf.Deserialize(file);
            hints_left = data.number_hints_left;
            total_hints_used = data.total_hints_used;
            total_images_solved = data.total_images_solved;
            GetComponent<ShuffleLogic>().indices_to_solve = data.indices_toSolve;
			file.Close();
        }
		update_ui();
	}

    public void Save_game(){
        string path = Application.persistentDataPath + "/mama.banana";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            data.number_hints_left = hints_left;
            data.total_hints_used = total_hints_used;
            data.total_images_solved = total_images_solved;
            data.indices_toSolve = GetComponent<ShuffleLogic>().indices_to_solve;
            bf.Serialize(file, data);
            file.Close();
        }
        update_ui();
	}

	public void solved(){
		total_images_solved += 1;
		Save_game();
	}

    public void hint_used()
    {
        total_hints_used += 1;
        Save_game();
    }

	public void update_ui(){
		total_solved.text = total_images_solved.ToString();
		hints_used.text = total_hints_used.ToString();
        hints_left_txt.text = hints_left.ToString();
	}
}

[System.Serializable]
class player_info{
	public int number_hints_left;
	public int total_hints_used;
	public int total_images_solved;
    public List<int>[] indices_toSolve;
	public List<int> data_structure;
}