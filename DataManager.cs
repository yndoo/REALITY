using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{

    private static DataManager instance = null;

    public TextAsset txt;
    public string[,] Sentence;
    public int lineSize, rowSize;
    public int[,] frames; // 프레임마다 객체 몇 개인지


    private void Awake()
    {
        if(null == instance)
        {
            // 이 클래스 인스턴스가 탄생했을 때 instance에 DataManager인스턴스가 없다면,
            // 자신을 넣어준다.
            instance = this;

            // 씬 전환이 되더라도 파괴되지 않게 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // 이미 존재한다면 자신(새로운 씬의 DataManager)을 삭제
            Destroy(this.gameObject);
        }
    }
    public static DataManager Instance
    {
        get
        {
            if(null==instance)
            {
                return null;
            }
            return instance;
        }
    }

    public int MakeData ()
    {
        // 엔터단위와 탭으로 나눠서 배열의 크기 조정
        string curretText = txt.text.Substring(0, txt.text.Length - 1);
        string[] line = curretText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        Sentence = new string[lineSize, rowSize];
        //countArr = new int[10]; // player가 10명 이하라는 가정하에 10.


        // 한 줄에서 탭으로 나누고 Sentence를 채움
        for (int i = 0; i < lineSize; i++)
        {
            string[] row = line[i].Split('\t');
            for (int j = 0; j < rowSize; j++)
            {
                Sentence[i, j] = row[j];
                print(i + "," + j + Sentence[i, j] + rowSize + "\n");
            }
        }

        // frame 이 몇 개인지는 미리 받기로 함. 여기선 일단 44칸 만듦.
        frames = new int[44, 2];
        // frame 몇에 객체가 몇 개인지 저장. frame 배열은 0부터 시작. 그 frame의 시작이 몇line인지까지 저장.
        for (int i=0; i<lineSize; i++)
        {
            if (Sentence[i, 0] == "frame")
            {
                frames[i,0] = Int32.Parse(Sentence[i, 2]);
                frames[i, 1] = i;
                print(frames[i,0] + ", " + frames[i,1]);
            }
        }

        return lineSize;
    }
}
