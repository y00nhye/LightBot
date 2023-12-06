using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapProducer : MonoBehaviour
{
    public GameObject map_base;
    public GameObject map_light;
    public Transform mapPos;

    public Transform startPos;
    public Quaternion startAngle;

    private void Start()
    {
        MapLoad();
    }

    public void MapLoad()
    {   
        for(int i = 0; i < DataManager.Instance.datas.round1.Length; i++)
        {
            Vector3 basePos;
            GameObject obj;

            if (DataManager.Instance.datas.round1[i].type == 0)
            {
                obj = Instantiate(map_base, mapPos);
                basePos = new Vector3(DataManager.Instance.datas.round1[i].x, DataManager.Instance.datas.round1[i].y, DataManager.Instance.datas.round1[i].z);
            }
            else
            {
                obj = Instantiate(map_light, mapPos);
                basePos = new Vector3(DataManager.Instance.datas.round1[i].x, DataManager.Instance.datas.round1[i].y - 0.1f, DataManager.Instance.datas.round1[i].z);
            }

            if(DataManager.Instance.datas.round1[i].isStart == 1)
            {
                obj.name = "Base_start";
                obj.transform.eulerAngles = new Vector3(0, DataManager.Instance.datas.round1[i].yAngle, 0);
            }

            obj.transform.position = basePos;
        }
    }
}
