using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorData;

public class Stage : MonoBehaviour
{
    [SerializeField] private Transform Bricks;
    [SerializeField] private List<Brick> _spawnBrickList = new List<Brick>();
    private int _bricksAmountOfEachCharater;
    private Dictionary<ColorTypeObject, List<Brick>> _pendingBrickList = new Dictionary<ColorTypeObject, List<Brick>>();
    
    public void OnInit()
    {
        /*foreach (Transform child in Bricks)
        {
            spawnBrickList.Add(child.GetComponent<Brick>());
        }
        bricksAmountOfEachCharater = spawnBrickList.Count / LevelManager.Ins.CharacterAmount;*/
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


    public void RemoveBrick()
    {

    }
}
