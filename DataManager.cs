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
    public int[,] frames; // �����Ӹ��� ��ü �� ������


    private void Awake()
    {
        if(null == instance)
        {
            // �� Ŭ���� �ν��Ͻ��� ź������ �� instance�� DataManager�ν��Ͻ��� ���ٸ�,
            // �ڽ��� �־��ش�.
            instance = this;

            // �� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // �̹� �����Ѵٸ� �ڽ�(���ο� ���� DataManager)�� ����
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
        // ���ʹ����� ������ ������ �迭�� ũ�� ����
        string curretText = txt.text.Substring(0, txt.text.Length - 1);
        string[] line = curretText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        Sentence = new string[lineSize, rowSize];
        //countArr = new int[10]; // player�� 10�� ���϶�� �����Ͽ� 10.


        // �� �ٿ��� ������ ������ Sentence�� ä��
        for (int i = 0; i < lineSize; i++)
        {
            string[] row = line[i].Split('\t');
            for (int j = 0; j < rowSize; j++)
            {
                Sentence[i, j] = row[j];
                print(i + "," + j + Sentence[i, j] + rowSize + "\n");
            }
        }

        // frame �� �� �������� �̸� �ޱ�� ��. ���⼱ �ϴ� 44ĭ ����.
        frames = new int[44, 2];
        // frame � ��ü�� �� ������ ����. frame �迭�� 0���� ����. �� frame�� ������ ��line�������� ����.
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
