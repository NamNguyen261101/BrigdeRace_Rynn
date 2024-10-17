using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Character : ColorObject
{
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer, stairLayer;

    // Brick
    [SerializeField] private GameObject charaterBrick;
    [SerializeField] private Transform brickContainer;
    [SerializeField] protected Transform skin;
    // private List<Brick> brickList = new List<Brick>();
    private float brickHeight = 0.2f;


    public override void OnInit()
    {
        ClearBrick();
        skin.rotation = Quaternion.identity;
    }

    /// <summary>
    /// check diem tiep theo xem co phai la ground khong - tra ve vi tri next do - tra ve vi tri hien tai
    /// </summary>
    /// <param name="nextPoint"></param>
    /// <returns></returns>
    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;

        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 1.1f;
        }

        return TF.position;
    }

    /// <summary> 
    /// Check Move - To stair - k cung mau thi fill + het gach + k cung mau + huong di len
    /// </summary>
    /// <param name="nextPoint"></param>
    /// <returns></returns>
    public bool CanMove(Vector3 nextPoint)
    {
        //check mau stair
        //k cung mau -> fill
        //het gach + k cung mau + huong di len

        bool isCanMove = true;
        RaycastHit hit;

       /* if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, stairLayer))
        {
            Stair stair = Cache.GetStair(hit.collider);

            if (stair.colorType != colorType && playerBricks.Count > 0)
            {
                stair.ChangeColor(colorType);
                RemoveBrick();
                stage.NewBrick(colorType);
            }

            if (stair.colorType != colorType && playerBricks.Count == 0 && skin.forward.z > 0)
            {
                isCanMove = false;
            }
        }*/

        return isCanMove;
    }






    public void ClearBrick()
    {

    }
}
