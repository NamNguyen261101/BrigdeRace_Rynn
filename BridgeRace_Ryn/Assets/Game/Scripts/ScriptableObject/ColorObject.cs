using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorData;

public class ColorObject : GameUnit
{
    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer renderer;
    protected ColorTypeObject _colorTypeObj;


    public ColorTypeObject OjbectColor
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
        throw new System.NotImplementedException();
    }

    public override void OnInit()
    {
        throw new System.NotImplementedException();
    }

    
}
