using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class MakeNewObject : MonoBehaviour
{

    //public GameObject cubePrefab;

    int num = 0; // 프레임 순서 및 넘버
    

    // MakeData를 위한 선언
    public TextAsset txt;
    public string[,] Sentence;
    public int lineSize, rowSize;
    public int[,] frames; // 프레임마다 객체 몇 개인지


    public void Start()
    {
        MakeData();

        Debug.Log(Sentence[0,2]);
    }

    // Update is called once per frame
    void Update()
    {
        if (num > 44)
        {
            return;
        }

        int before = 0, after = 0;

        if (num > 0)
        {
            before = frames[num - 1, 0];
        }else
        {
            before = 0;
        }

        after = frames[num, 0];

        Debug.Log(frames[num, 1]);

        if (before < after)
        {
            Create(after - before);
        }
        else
        {
            Return(before - after);
        }

        for (int i=0; i<after; i++)
        {
            ObjectManager.instance.transform.GetChild(i).position 
                = new Vector3(float.Parse(Sentence[frames[num, 1] + i, 1]), 0, float.Parse(Sentence[frames[num, 1] + i, 2]));
            //Debug.Log(i);
        }



        num++;
        Debug.Log("num : " + num);
    }

    void Create(int index)          // 모자란만큼 추가
    {
        for (int i=0;i<index; i++)
        {
            ObjectManager.instance.Pop();
        }
    }

    void Return(int index)          // 남는만큼 반환
    {
        for(int i=0; i<index; i++)
        {
            ObjectManager.instance.Push(ObjectManager.instance.transform.GetChild(i).gameObject.GetComponent<Move>());
        }
    }

    public int MakeData()
    {
        // 엔터단위와 탭으로 나눠서 배열의 크기 조정
        string curretText = txt.text.Substring(0, txt.text.Length - 1);
        string[] line = curretText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        Sentence = new string[lineSize, rowSize];


        // 한 줄에서 탭으로 나누고 Sentence를 채움
        for (int i = 0; i < lineSize; i++)
        {
            string[] row = line[i].Split('\t');
            for (int j = 0; j < rowSize; j++)
            {
                Sentence[i, j] = row[j];
                //(i + "," + j + "   " + Sentence[i, j] + "   " + rowSize + "\n");
            }
        }

        // frame 이 몇 개인지는 미리 받기로 함. ?? 필요없을수도 ??일단 44칸
        frames = new int[45, 2];
        // frame 몇에 객체가 몇 개인지 저장. frame 배열은 0부터 시작. 그 frame의 시작이 몇line인지까지 저장.
        int f = 0;

            for (int i = 0; i < lineSize; i++)
            {
                if (Sentence[i, 0] == "frame")
                {
                    frames[f, 0] = Int32.Parse(Sentence[i, 2]); // 객체 개수
                    frames[f, 1] = i;                           // 몇번째라인인지
                    print(frames[f, 0] + ", " + frames[f, 1]);
                    f++;
                }

            }

        return lineSize;
    }
}
