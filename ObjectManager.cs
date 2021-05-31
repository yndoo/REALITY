using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;
    public static ObjectManager Instance
    {
        get
        {
            return instance;
        }
    }


    private void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }


    [SerializeField]
    private Move poolObj;
    [SerializeField]
    private int allocateCount = 10; // �̸� Ǯ�� �� ������Ʈ ����

    private Stack<Move> poolStack = new Stack<Move>();

    private void Start()
    {
        Allocate();
    }

    public void Allocate()               // Ǯ�� �� ������Ʈ�� �̸� ���� ������ŭ ����, ���ÿ� �Ҵ�
    {
        for (int i = 0; i < allocateCount; i++)
        {
            Move allocateObj = Instantiate(poolObj, this.gameObject.transform);
            poolStack.Push(allocateObj);
        }
    }

    public Move Pop()            // ������Ʈ�� Ǯ���� ����
    {
        if (poolStack.Count > 0)
        {
            Move obj = poolStack.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var obj = Create();
            obj.gameObject.SetActive(true);
            poolStack.Push(obj);
            return poolStack.Pop();
        }
    }

    public void Push(Move obj)      // ������Ʈ�� Ǯ�� ����
    {
        obj.gameObject.SetActive(false);
        poolStack.Push(obj);
    }

    public Move Create()
    {
        Move allocateObj = Instantiate(poolObj, this.gameObject.transform);
        return allocateObj;

    }

}
