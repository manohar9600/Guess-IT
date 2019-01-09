using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class LowerAlphabetGenerator : MonoBehaviour {
	public Transform parent_transform;
	public GameObject block;

    public GameObject[] generate(string word)
    {
        word = Regex.Replace(word, @"[^0-9a-zA-Z]+", "");
        int number_alphabets = word.Length;
        GameObject[] lower_alphabets = new GameObject[number_alphabets];
        float between_dis = 90f;
        float upper_dis = 186f;
		float center = parent_transform.position.x; 
        int[] segments = break_word(word);
        int prev = 0;
        float[] height_array = {parent_transform.position.y+upper_dis,
                                parent_transform.position.y,
                                parent_transform.position.y-upper_dis};
        for(int i=0; i<segments.Length; i++){
            if(segments[i] != 0){
                GameObject[] te = arrange_create_alphabets(center,
                                                        segments[i],
                                                        between_dis,
                                                        height_array[i]);
                Array.Copy(te, 0, lower_alphabets, prev, te.Length);
                prev += te.Length;
            }
        }
        lower_alphabets = link_alphabets(lower_alphabets, word.ToUpper());
        return lower_alphabets;
    }


    int[] break_word(string word){
        int[] segments = new int[3];
        int number_alphabets = word.Length;
        if(number_alphabets < 4){
            segments[0] = 0;
            segments[1] = number_alphabets;
            segments[2] = 0;
        }else{
            int total_created = 0;
            while(total_created != number_alphabets){
                for(int i=0; i<3; i++){
                    int random_number = UnityEngine.Random.Range(2, 6-segments[i]);
                    if(segments[i] + random_number < 7){
                        if(total_created + random_number > number_alphabets){
                            random_number = number_alphabets - total_created;
                        }
                        segments[i] += random_number;
                        total_created += random_number;
                    }
                }
            }
        }
        return segments;
    }




	GameObject[] arrange_create_alphabets(float center, int number_alphabets, 
								float between_dis, float pos_y){
		/* start from center.  */
		float[] pos = new float[number_alphabets];
        pos[0] = center;
        for (int i = 1; i < number_alphabets; i++)
        {
            pos[i] = pos[i - 1] + between_dis;
            for (int j = 0; j < i; j++)
            {
                pos[j] = pos[j] - between_dis;
            }
        }

		GameObject[] objects = create_objects(pos, pos_y);
		return objects;
	}


	GameObject[] create_objects(float[] pos, float pos_y){
		GameObject[] objects = new GameObject[pos.Length];
        for (int i = 0; i < pos.Length; i++)
        {
            var position = new Vector3(pos[i], pos_y, 0F);
            var rotation = Quaternion.identity;
            objects[i] = Instantiate(block, position, rotation)
                                            as GameObject;
            objects[i].transform.SetParent(parent_transform);
        }
		return objects;
	}


    GameObject[] link_alphabets(GameObject[] objects, string word){
        var word_array = shuffle(word.ToCharArray());
        for(int i=0; i<objects.Length; i++){
            var te = objects[i].GetComponentInChildren<Text>();
            te.text = word_array[i].ToString();
        }
        return objects;
    }


    char[] shuffle(char[] word){
        for(int i=0; i<word.Length; i++){
            int num = UnityEngine.Random.Range(0, word.Length);
            char temp = word[i];
            word[i] = word[num];
            word[num] = temp;
        }
        return word;
    }

}
