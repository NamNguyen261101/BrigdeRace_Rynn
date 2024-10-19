using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorData;

public class ColorObject : GameUnit
{
    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer renderer;
    public ColorTypeObject _colorTypeObj;


    public ColorTypeObject ObjectColor
    {
        get
        {
            return _colorTypeObj;
        }

        set
        {
            this._colorTypeObj = value;
        }
    }

    public void ChangeColor(ColorTypeObject colorType)
    {
        this._colorTypeObj = colorType;
        renderer.material = colorData.GetColorMat(colorType);
    }


    public override void OnDespawn()
    {
        
    }

    public override void OnInit()
    {
        throw new System.NotImplementedException();
    }

    
}
