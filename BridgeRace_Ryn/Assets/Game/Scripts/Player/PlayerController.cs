using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorData;

public class PlayerController : Character
{
    [SerializeField] private float _speed = 5;


    private void Start()
    {
        OnInit();
    }

    public override void OnInit()
    {
        ChangeColor(ColorTypeObject.Violet);
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            Vector3 nextPoint = JoystickController.direct * _speed * Time.deltaTime + TF.position;

            if (CanMove(nextPoint))
            {
                TF.position = CheckGround(nextPoint);  
            }

            if (JoystickController.direct != Vector3.zero)
            {
                _skin.forward = JoystickController.direct;
            }

            ChangeAnim(CONSTANTS.ANIM_RUN);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ChangeAnim(CONSTANTS.ANIM_IDLE);
        }
    }
}
