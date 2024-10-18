using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ColorData;

public class Stage : MonoBehaviour
{
    #region Stage Try
    /*[SerializeField] private Transform Bricks;
    [SerializeField] private List<Brick> _spawnBrickList = new List<Brick>();
    private int _bricksAmountOfEachCharater;
    private Dictionary<ColorTypeObject, List<Brick>> _pendingBrickList = new Dictionary<ColorTypeObject, List<Brick>>();
    public void OnInit()
    {
       *//* foreach (Transform child in Bricks)
        {
            spawnBrickList.Add(child.GetComponent<Brick>());
        }
        bricksAmountOfEachCharater = spawnBrickList.Count / LevelManager.Ins.CharacterAmount;*//*
    }

    public void InitColor(ColorTypeObject colorType)
    {
        for (int i = 0; i < _bricksAmountOfEachCharater; i++)
        {
            SpawnNewBrick(colorType);
        }
    }

    public void AddBrick(Brick brick)
    {
        if (_pendingBrickList.ContainsKey(brick.OjbectColor))
        {
            var brickList = _pendingBrickList[brick.OjbectColor];
            brickList.RemoveAt(brickList.IndexOf(brick));
        }
        brick.ChangeColor(ColorTypeObject.Default);
        _spawnBrickList.Add(brick);
    }

    public void SpawnNewBrick(ColorTypeObject colorType)
    {
        if (_spawnBrickList.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, _spawnBrickList.Count);
            Brick brick = _spawnBrickList[rand];
            brick.stage = this;
            brick.ChangeColor(colorType);
            _spawnBrickList.RemoveAt(rand);

            // Add brick vao list tuong ung
            if (_pendingBrickList.ContainsKey(colorType))
            {
                // Key da ton tai thi them brick moi vao list 
                var brickList = _pendingBrickList[colorType];
                brickList.Add(brick);
            }
            else
            {
                // neu key chua ton tai thi tao 1 list moi va add brick moi vao 
                _pendingBrickList[colorType] = new List<Brick> { brick };
            }
        }
    }


    public void RemoveBrick(Brick brick)
    {
        _spawnBrickList.Add(brick);
        //bricks.Remove(brick);
    }
*/
    #endregion

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
        /*int amount = brickPoints.Length / LevelManager.Instance.CharacterAmount;

        for (int i = 0; i < amount; i++)
        {
            AddBrick(colorType);
        }*/
    }


    public void AddBrick(ColorTypeObject colorTypeObj)
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
            if (bricks[i].OjbectColor == colorType)
            {
                brick = bricks[i];
                break;
            }
        }

        return brick;
    }

    #endregion

}
