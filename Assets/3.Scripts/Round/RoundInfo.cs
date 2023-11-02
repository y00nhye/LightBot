using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundInfo", menuName = "Scriptable Object / RoundInfo", order = int.MaxValue)]
public class RoundInfo : ScriptableObject
{
    public int playerYPos;
    public int lightCnt;
    public int[] inputBtnArr;
}
