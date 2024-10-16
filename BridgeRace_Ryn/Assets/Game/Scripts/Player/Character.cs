using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigi;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer, stairLayer;

    // Brick
    [SerializeField] private GameObject charaterBrick;
    [SerializeField] private Transform brickContainer;
    // private List<Brick> brickList = new List<Brick>();
    private float brickHeight = 0.2f;


    // Get- Set
    public Rigidbody Rigidbody
    {
        get
        {
            return _rigi;
        }

        set
        {
            _rigi = value;
        }
    }
}
