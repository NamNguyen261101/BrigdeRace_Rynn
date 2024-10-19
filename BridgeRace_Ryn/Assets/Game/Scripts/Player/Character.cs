using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using static ColorData;

public class Character : ColorObject
{
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer, stairLayer;
    private string _currentAnim;

    // Brick
    [SerializeField] private PlayerBrick _playerBrickPrefab;
    // [SerializeField] private GameObject _charaterBrick;
    [SerializeField] private Transform _brickContainer;
    [SerializeField] protected Transform _skin;
    [SerializeField] private List<PlayerBrick> _playerBrickList = new List<PlayerBrick>();
    private float brickHeight = 0.1f;

    public int _BrickCount => _playerBrickList.Count;

    [HideInInspector] public Stage _stage;

    public override void OnInit()
    {
        ClearBrick();
        _skin.rotation = Quaternion.identity;
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
            Debug.DrawRay(nextPoint, Vector3.down * 2f, Color.red, 0.5f);
            return hit.point + Vector3.up * 0.1f;
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
        RaycastHit hit;
        bool isCanMove = true;

        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2.5f, stairLayer))
        {
            Debug.DrawRay(nextPoint, Vector3.down * 2f, Color.green, 0.5f);
          

            Stair stair = Cache.GetStair(hit.collider);

            if (stair._colorTypeObj != _colorTypeObj && _playerBrickList.Count > 0)
            {
                stair.ChangeColor(_colorTypeObj); // _colorTypeObj OjbectColor
                RemoveBrick();
                _stage.NewBrick(_colorTypeObj);
            }

            if (stair._colorTypeObj != _colorTypeObj && _playerBrickList.Count == 0 && _skin.forward.z > 0)
            {
                
                isCanMove = false;
            }
        }
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddBrick()
    {
        PlayerBrick playerBrick = Instantiate(_playerBrickPrefab, _brickContainer);
        playerBrick.ChangeColor(_colorTypeObj);
        playerBrick.TF.localPosition = Vector3.up * 0.25f * _playerBrickList.Count;
        _playerBrickList.Add(playerBrick);
    }

    /// <summary>
    /// 
    /// </summary>
    public void RemoveBrick()
    {
        if (_playerBrickList.Count > 0)
        {
            PlayerBrick playerBrick = _playerBrickList[_playerBrickList.Count - 1];
            _playerBrickList.RemoveAt(_playerBrickList.Count - 1);
            Destroy(playerBrick.gameObject);
        }
    }

    /// <summary>
    /// Clear Brick -> When start new level
    /// </summary>
    public void ClearBrick()
    {
        for (int i = 0; i < _playerBrickList.Count; i++)
        {
            Destroy(_playerBrickList[i]);
        }

        _playerBrickList.Clear();
    }

    /// <summary>
    /// Change Animation Player
    /// </summary>
    /// <param name="animName"></param>
    public void ChangeAnim(string animName)
    {
        if (_currentAnim != animName)
        {
            anim.ResetTrigger(_currentAnim); 
            _currentAnim = animName;
            anim.SetTrigger(_currentAnim);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CONSTANTS.TAG_BRICK)) 
        {
            Brick brick = Cache.GetBrick(other);
            
            if (brick._colorTypeObj == _colorTypeObj) // ObjectColor
            {
                brick.OnDespawn();
                AddBrick();
                Destroy(other.gameObject);
            }
              /*  AddBrick();
            Destroy(other.gameObject);*/
        }

      
    }
}
