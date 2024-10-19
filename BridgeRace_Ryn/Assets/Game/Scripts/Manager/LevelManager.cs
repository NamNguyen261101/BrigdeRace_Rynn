using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static ColorData;

public class LevelManager : Singleton<LevelManager> 
{
    readonly List<ColorTypeObject> colorTypes = new List<ColorTypeObject>() { ColorTypeObject.Black,
                                                ColorTypeObject.Red, ColorTypeObject.Blue, 
                                                ColorTypeObject.Green, ColorTypeObject.Yellow, 
                                                ColorTypeObject.Orange, ColorTypeObject.Brown, ColorTypeObject.Violet };

    public Level[] levelPrefabs;
    // public Bot botPrefab;
    public PlayerController player;

    public Vector3 FinishPoint => currentLevel.FinishPoint.position;

    public int CharacterAmount => currentLevel.BotAmount + 1;


    //private List<Bot> bots = new List<Bot>();
    private Level currentLevel;

    private int levelIndex;

    private void Awake()
    {
        levelIndex = PlayerPrefs.GetInt("Level", 0);
    }

    private void Start()
    {
        // LoadLevel(levelIndex);
        OnInit();
       // UIManager.Instance.OpenUI<MainMenu>();
    }

    public void OnInit()
    {
        //init vi tri bat dau game
        Vector3 index = currentLevel.StartPoint.position;
        float space = 2f;
        Vector3 leftPoint = ((CharacterAmount / 2) + (CharacterAmount % 2) * 0.5f - 0.5f) * space * Vector3.left + index;

        List<Vector3> startPoints = new List<Vector3>();

        for (int i = 0; i < CharacterAmount; i++)
        {
            startPoints.Add(leftPoint + space * Vector3.right * i);
        }

       /* //update navmesh data
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(currentLevel.navMeshData);*/

        //init random mau
        List<ColorTypeObject> colorDatas = Utilities.SortOrder(colorTypes, CharacterAmount);
        //

        //set vi tri player
        int rand = Random.Range(0, CharacterAmount);
        player.TF.position = startPoints[rand];
        player.TF.rotation = Quaternion.identity;
        startPoints.RemoveAt(rand);
        //set color player
        player.ChangeColor(colorDatas[rand]);
        colorDatas.RemoveAt(rand);

        player.OnInit();
        /*for (int i = 0; i < CharacterAmount - 1; i++)
        {
            //Bot bot = SimplePool.Spawn<Bot>(botPrefab, startPoints[i], Quaternion.identity);
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, startPoints[i], Quaternion.identity);
            bot.ChangeColor(colorDatas[i]);
            bot.OnInit();
            bots.Add(bot);
        }*/
    }

}
