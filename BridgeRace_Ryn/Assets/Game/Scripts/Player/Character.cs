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
    [SerializeField] private GameObject _charaterBrick;
    [SerializeField] private Transform _brickContainer;
    [SerializeField] protected Transform _skin;
    [SerializeField] private List<Brick> _playerBrickList = new List<Brick>();
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
            Debug.DrawRay(nextPoint + Vector3.up, Vector3.down * 2f, Color.red, 0.5f);
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

        if (Physics.Raycast(nextPoint + Vector3.up, Vector3.down, out hit, 2.5f, stairLayer))
        {
            Debug.DrawRay(nextPoint + Vector3.up, Vector3.down * 2f, Color.green, 1f);
            /*if (nextPoint.z - TF.position.z < 1)
            {
                return true;
            }
            else
            {
                if (hit.collider.GetComponent<ColorObject>().OjbectColor == OjbectColor)
                {
                    return true;
                }
                else
                {
                    if (_playerBrickList.Count > 0)
                    {
                        RemoveBrick();
                        hit.collider.GetComponent<ColorObject>().ChangeColor(OjbectColor);

                        // Respawn a new Birck in current Stage
                        // _stage.SpawnNewBrick(OjbectColor);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }*/

            Stair stair = Cache.GetStair(hit.collider);

            if (stair.OjbectColor != _colorTypeObj && _playerBrickList.Count > 0)
            {
                stair.ChangeColor(_colorTypeObj);
                RemoveBrick();
                _stage.SpawnNewBrick(_colorTypeObj);
            }

            if (stair.OjbectColor != _colorTypeObj && _playerBrickList.Count == 0 && _skin.forward.z > 0)
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
        Brick newBrick = Instantiate(_charaterBrick.GetComponent<Brick>(), _brickContainer);
        newBrick.ChangeColor(OjbectColor);
        newBrick.TF.localPosition = Vector3.up * brickHeight * _playerBrickList.Count;
        _playerBrickList.Add(newBrick);
    }

    /// <summary>
    /// 
    /// </summary>
    public void RemoveBrick()
    {
        if (_playerBrickList.Count > 0)
        {
            Brick playerBrick = _playerBrickList[_playerBrickList.Count - 1];
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
        if (other.CompareTag(CONSTANTS.TAG_BRICK) && other.GetComponent<ColorObject>().OjbectColor == _colorTypeObj)
        {
            // ReAdd Brick to Stage
            // other.GetComponent<Brick>().ReAddBrick();
            AddBrick();
            Destroy(other.gameObject);
        }

      
    }
}
