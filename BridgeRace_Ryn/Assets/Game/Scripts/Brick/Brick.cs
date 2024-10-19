using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static ColorData;

public class Brick : ColorObject
{
    [HideInInspector] public Stage stage;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        ChangeColor(ColorTypeObject.Violet);
    }
   
    public void OnDespawn(Collider colli)
    {
        stage.RemoveBrick(this);
    }

}
