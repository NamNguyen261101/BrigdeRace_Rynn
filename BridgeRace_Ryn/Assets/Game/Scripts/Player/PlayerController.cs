using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField] private float _speed = 5;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            Vector3 nextPoint = JoystickController.direct * _speed * Time.deltaTime + TF.position;

            if (CanMove(nextPoint))
            {
                TF.position = CheckGround(nextPoint);
                //ChangeAnim(CONSTANTS.ANIM_RUN);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CONSTANTS.TAG_BRICK))
        {
            // Brick brick = Cache.GetBrick(other);
            Brick brick;
            Debug.Log("Caught Brick");
            /* if (OjbectColor. == brick)
             {
                 brick.OnDespawn();
                 AddBrick();
                 Destroy(brick.gameObject);
             }*/
        }
    }



}
