using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform finishPoint;
    [SerializeField] private int botAmount;
    [SerializeField] private Stage[] stages;
    

    // Get set
    public Transform StartPoint
    {
        get
        {
            return startPoint;
        }

        set
        {
            this.startPoint = value;
        }
    }

    public Transform FinishPoint
    {
        get
        {
            return finishPoint;
        }

        set
        {
            this.finishPoint = value;
        }
    }

    public int BotAmount
    {
        get
        {
            return botAmount;
        }

        set
        {
            this.botAmount = value;
        }
    }

    public void OnInit()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].OnInit();
        }
    }
}
