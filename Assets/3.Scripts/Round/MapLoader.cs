using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] maps;

    public void LoadMap()
    {
        if (GameManager.Instance.roundCnt > 1)
        {
            maps[GameManager.Instance.roundCnt - 2].SetActive(false);
        }
        maps[GameManager.Instance.roundCnt - 1].SetActive(false);
        maps[GameManager.Instance.roundCnt - 1].SetActive(true);
    }
}
