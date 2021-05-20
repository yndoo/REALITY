using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class MakeNewObject : MonoBehaviour
{

    public GameObject cubePrefab;
    int j = 0;

    // Start is called before the first frame update
    void Start()
    {
        //int size;

        DataManager.Instance.MakeData();

        
        //for (int j=0; j<44; j++) // 44개의 프레임
        //{
            for (int i = 0; i < DataManager.Instance.frames[j,0]; i++) // j번째 프레임에서의 객체 개수
            {
                Vector3 vec = new Vector3(float.Parse(DataManager.Instance.Sentence[DataManager.Instance.frames[j, 1]+i, 1]), 0, float.Parse(DataManager.Instance.Sentence[DataManager.Instance.frames[j, 1] + i, 2]));
                Instantiate(cubePrefab, vec, Quaternion.identity);
            }
        //}
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < DataManager.Instance.frames[j, 0]; i++) // j번째 프레임에서의 객체 개수
        {
            Vector3 vec = new Vector3(float.Parse(DataManager.Instance.Sentence[DataManager.Instance.frames[j, 1] + i, 1]), 0, float.Parse(DataManager.Instance.Sentence[DataManager.Instance.frames[j, 1] + i, 2]));
            Instantiate(cubePrefab, vec, Quaternion.identity);
        }
        j++;

        if(j>=44)
            Destroy(this.gameObject);
    }
}
