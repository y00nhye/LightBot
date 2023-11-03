using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] maps;

    public void LoadMap(int roundCnt)
    {
        if (roundCnt > 1)
        {
            maps[roundCnt - 2].SetActive(false);
        }
        maps[roundCnt - 1].SetActive(false);
        maps[roundCnt - 1].SetActive(true);
    }
}
