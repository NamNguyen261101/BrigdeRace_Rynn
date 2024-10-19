using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ColorData;

public class Stage : MonoBehaviour
{

    #region Stage using TF unit 
    [SerializeField] private Transform[] brickPoints;

    [SerializeField] private List<Vector3> emptyPoint = new List<Vector3>();

    [SerializeField] private List<Brick> bricks = new List<Brick>();

    [SerializeField] private Brick brickPrefab;

    internal void OnInit()
    {
        for (int i = 0; i < brickPoints.Length; i++)
        {
            emptyPoint.Add(brickPoints[i].position);
        }
    }

    public void InitColor(ColorTypeObject colorType)
    {
        int amount = brickPoints.Length / LevelManager.Instance.CharacterAmount;

        for (int i = 0; i < amount; i++)
        {
            NewBrick(colorType);
        }
    }


    public void NewBrick(ColorTypeObject colorTypeObj)
    {
        if (emptyPoint.Count > 0)
        {
            int rand = Random.Range(0, emptyPoint.Count);
            Brick brick = SimplePool.Spawn<Brick>(brickPrefab, emptyPoint[rand], Quaternion.identity);
            brick.stage = this;
            brick.ChangeColor(colorTypeObj);
            emptyPoint.RemoveAt(rand);
            bricks.Add(brick);
        }
    }

    internal void RemoveBrick(Brick brick)
    {
        emptyPoint.Add(brick.TF.position);
        bricks.Remove(brick);
    }

    internal Brick SeekBrickPoint(ColorTypeObject colorType)
    {
        Brick brick = null;

        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].ObjectColor == colorType)
            {
                brick = bricks[i];
                break;
            }
        }
        return brick;
    }

    #endregion

}
