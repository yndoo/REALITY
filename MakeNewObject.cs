using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class MakeNewObject : MonoBehaviour
{

    //public GameObject cubePrefab;

    int num = 0; // ������ ���� �� �ѹ�
    

    // MakeData�� ���� ����
    public TextAsset txt;
    public string[,] Sentence;
    public int lineSize, rowSize;
    public int[,] frames; // �����Ӹ��� ��ü �� ������


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

    void Create(int index)          // ���ڶ���ŭ �߰�
    {
        for (int i=0;i<index; i++)
        {
            ObjectManager.instance.Pop();
        }
    }

    void Return(int index)          // ���¸�ŭ ��ȯ
    {
        for(int i=0; i<index; i++)
        {
            ObjectManager.instance.Push(ObjectManager.instance.transform.GetChild(i).gameObject.GetComponent<Move>());
        }
    }

    public int MakeData()
    {
        // ���ʹ����� ������ ������ �迭�� ũ�� ����
        string curretText = txt.text.Substring(0, txt.text.Length - 1);
        string[] line = curretText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        Sentence = new string[lineSize, rowSize];


        // �� �ٿ��� ������ ������ Sentence�� ä��
        for (int i = 0; i < lineSize; i++)
        {
            string[] row = line[i].Split('\t');
            for (int j = 0; j < rowSize; j++)
            {
                Sentence[i, j] = row[j];
                //(i + "," + j + "   " + Sentence[i, j] + "   " + rowSize + "\n");
            }
        }

        // frame �� �� �������� �̸� �ޱ�� ��. ?? �ʿ�������� ??�ϴ� 44ĭ
        frames = new int[45, 2];
        // frame � ��ü�� �� ������ ����. frame �迭�� 0���� ����. �� frame�� ������ ��line�������� ����.
        int f = 0;

            for (int i = 0; i < lineSize; i++)
            {
                if (Sentence[i, 0] == "frame")
                {
                    frames[f, 0] = Int32.Parse(Sentence[i, 2]); // ��ü ����
                    frames[f, 1] = i;                           // ���°��������
                    print(frames[f, 0] + ", " + frames[f, 1]);
                    f++;
                }

            }

        return lineSize;
    }
}
