using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class UpperAlphabetGenerator : MonoBehaviour {
	public Transform parent_transform;
	public GameObject block;
	public GameObject[] generate(string word){

        int number_alphabets = Regex.Replace(word, @"[^0-9a-zA-Z]+", "").Length;
		float between_dis = 60f;
        float height_dis = 122f;
		float center = parent_transform.position.x;
        GameObject[] upper_alphabets = new GameObject[number_alphabets];
        string[] lines = word.Split(Environment.NewLine.ToCharArray());

        if(lines.Length == 1){
            int[] segments = break_word(word);
            float[] te = {height_dis, 0f};
            if(segments[1] == 0){
                te[0] = 0f;
            }
            int prev = 0;
            for(int i=0; i<2; i++){
                if(segments[i] != 0){
                    GameObject[] temp = arrange_create_alphabets(center,
                                    segments[i],
                                    between_dis,
                                    te[i]);
                    Array.Copy(temp, 0, upper_alphabets, prev,
                                temp.Length);
                    prev += temp.Length;
                }
            }
            upper_alphabets = create_space(upper_alphabets, word);
        }else{
            int prev = 0;
            float[] te = { height_dis, 0f };
            for (int i = 0; i < lines.Length; i++)
            {
                int len = Regex.Replace(lines[i], @"[^0-9a-zA-Z]+", "").Length;
                GameObject[] temp = arrange_create_alphabets(center,
                                                    len,
                                                    between_dis,
                                                    te[i]);
                temp = create_space(temp, lines[i]);
                Array.Copy(temp, 0, upper_alphabets, prev,
                            temp.Length);
                prev += temp.Length;
            }
        }
        return upper_alphabets;
	}

    int[] break_word(string word){
        var splits = word.Split();
        int[] segments = new int[2];
        if(splits.Length == 1){
            if (word.Length >= 12)
            {
                segments[0] = 12;
                segments[1] = word.Length - 12;
            }
            else
            {
                segments[0] = word.Length;
                segments[1] = 0;
            }
        }else{
            if(splits[splits.Length-1].Length < 12){
                segments[0] = 0;
                for(int i=0; i<splits.Length-1; i++){
                    segments[0] += splits[i].Length;
                }
                segments[1] = Regex.Replace(word, @"[^0-9a-zA-Z]+", "").Length - segments[0];
            }else{
                segments[0] = 12;
                segments[1] = word.Length - 12;
            }
        }
        return segments;
    }

    GameObject[] arrange_create_alphabets(float center, int number_alphabets,
                                float between_dis, float pos_y)
    {
        /* start from center.  */
        float[] pos = new float[number_alphabets];
        float size = 135f;
        int text_size = 75;
        pos[0] = center;
        if(number_alphabets > 7){
            size = 1000f / number_alphabets;
            between_dis = 420f / number_alphabets - 1;
            text_size = 525 / number_alphabets;
        }
        for (int i = 1; i < number_alphabets; i++)
        {
            pos[i] = pos[i - 1] + between_dis;
            for (int j = 0; j < i; j++)
            {
                pos[j] = pos[j] - between_dis;
            }
        }

        GameObject[] objects = create_objects(pos, size, 
                                            parent_transform.position.y+pos_y,
                                            text_size);
        return objects;
    }

    GameObject[] create_objects(float[] pos, float size, float pos_y, 
                                int text_size)
    {
        GameObject[] upper_alphabets = new GameObject[pos.Length];
        for (int i = 0; i < pos.Length; i++)
        {
            var position = new Vector3(pos[i], pos_y, 0F);
            var rotation = Quaternion.identity;
            upper_alphabets[i] = Instantiate(block, position, rotation)
                                            as GameObject;
            upper_alphabets[i].GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
            var qe = upper_alphabets[i].GetComponentsInChildren<Text>();
            qe[0].fontSize = text_size;
            upper_alphabets[i].SetActive(true);
            upper_alphabets[i].transform.SetParent(parent_transform);
        }
        return upper_alphabets;
    }

    GameObject[] create_space(GameObject[] objects, string word){
        int number_alphabets = Regex.Replace(word, @"[^0-9a-zA-Z]+", "").Length;
        float space_width = 30f;
        string[] segments = word.Split();
        int space_pos = 0;
        for(int i=2; i<segments.Length; i++){
            space_pos += segments[i-1].Length;
            for(int j=0; j<space_pos; j++){
                var pos = objects[j].transform.position;
                pos.x -= space_width;
                objects[j].transform.position = pos;
            }
            for(int j=space_pos; j<number_alphabets; j++){
                var pos = objects[j].transform.position;
                pos.x += space_width;
                objects[j].transform.position = pos;
            }
        }
        return objects;
    }
}
