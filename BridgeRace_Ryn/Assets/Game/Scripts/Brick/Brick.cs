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

    public override void OnInit()
    {
        ChangeColor(ColorTypeObject.Violet);
    }
    /*public void ReAddBrick()
    {
        //stage.AddBrick(this);
    }*/

    public void OnDespawn()
    {
        stage.RemoveBrick(this);
    }

   /* private void OnTriggerEnter(Collider other)
    {
        // TO DO
        if (other.CompareTag("Player") && other.GetComponent<ColorObject>().OjbectColor == _colorTypeObj)
        {
            // Debug.Log("Remove brick");
            Destroy(t);
        }
    }*/

}
