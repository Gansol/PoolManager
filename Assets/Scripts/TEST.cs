using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TEST : MonoBehaviour
{
    public GameObject spawnHere;        // 這是要把物件 Active的位置(你可以放到任何地方)

    PoolManager poolManager;
    GameObject clone;                   // 暫存clone物件
    GameObject[] tmpObject;             // 暫存物件

    bool flag;


    void Start()
    {
        flag = true;
        tmpObject = new GameObject[10];
        poolManager = GetComponent<PoolManager>();
    }

    void Update()
    {
        if (poolManager.poolingFlag && flag)     // 如果物件池 初始化完成  (注意:poolManager.poolingFlag = true 時才可以使用物件池)
        {
            flag = false;
            Debug.Log("Active Object!");

            clone = (GameObject)poolManager.ActiveObject(0); // 啟動物件Cat
            clone.transform.parent = spawnHere.transform;
            clone.transform.localPosition = Vector3.zero;

            // 生10個會超過物件池基本數量 就會在實體化新的物件出來
            for (int i = 0; i < 10; i++)
            {
                clone = (GameObject)poolManager.ActiveObject(0); // 啟動物件Cat
                clone.transform.parent = spawnHere.transform;
                clone.transform.localPosition =new  Vector3(i,i,i);
                tmpObject[i] = clone;   // 暫存物件 等等用來示範丟回物件池
            }

            clone = (GameObject)poolManager.ActiveObject(1); // 啟動物件Rabbit
            clone.transform.parent = spawnHere.transform;
            clone.transform.localPosition = Vector3.one;
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 100), "Recover"))
        {
            for (int i = 0; i < 10; i++)
            {
                clone = tmpObject[i];
                clone.transform.parent = poolManager.Pool.transform.FindChild(clone.name);  // 把物件 丟回物件池
                clone.SetActive(false);                                                     // 並讓他看不見
            }
            // 經過 一段時間(可自訂) 物件池會清理掉多的物件 並保留(可自訂數量) 的物件
        }
    }
}
