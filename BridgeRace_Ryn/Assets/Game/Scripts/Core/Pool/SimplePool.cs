/// Simple pooling for Unity.
///   Author: Martin "quill18" Glaude (quill18@quill18.com)
///   Latest Version: https://gist.github.com/quill18/5a7cfffae68892621267
///   License: CC0 (http://creativecommons.org/publicdomain/zero/1.0/)
///   UPDATES:
/// 	2015-04-16: Changed Pool to use a Stack generic.
/// 	
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public static class SimplePool 
{
    // You can avoid resizing of the Stack's internal data by
    // setting this to a number equal to or greater to what you
    // expect most of your pool sizes to be.
    // Note, you can also use Preload() to set the initial size
    // of a pool -- this can be handy if only some of your pools
    // are going to be exceptionally large (for example, your bullets.)
    const int DEFAULT_POOL_SIZE = 3;

    // Key and Value in Pool
    static Dictionary<int, Pool> pools = new Dictionary<int, Pool>();

    // Type Pool From Enum
    static Dictionary<PoolType, GameUnit> poolTypes = new Dictionary<PoolType, GameUnit>();


    // TF Object
    private static GameUnit[] _gameUnitResources;


    private static Transform _root;
    /// <summary>
    /// Getter - Setter
    /// </summary>
    public static Transform Root
    {
        get
        {
            if (_root == null)
            {
                // root = GameObject.FindObjectOfType<PoolControler>().transform; // FindObjectOfType<PoolControler>().transform;

                if (_root == null)
                {
                    _root = new GameObject("Pool").transform;
                }
            }
            return _root;
        }
    }

    #region Class Pool
    /// <summary>
    /// The Pool class represents the pool for a particular prefab.
    /// </summary>
    class Pool
    {
        private Transform _msRoot = null;
        // collect in
        private bool _mCollect;

        // not active
        Queue<GameUnit> _inactiveObj;

        //collect obj active ingame
        List<GameUnit> _activeObj;


        // The prefab that we are pooling
        GameUnit prefab;

        bool _mclamp;
        int m_Amount;

        // get set
        public bool IsCollect
        {
            get
            {
                return _mCollect;
            }
        }

        public int Count
        {
            get
            {
                return _inactiveObj.Count;
            }
        }

        // constructor
        public Pool (GameUnit prefab, int initialQty, Transform parent, bool collect, bool clamp)
        {
            _inactiveObj = new Queue<GameUnit>(initialQty);
            _msRoot = parent;
            this.prefab = prefab;
            this._mCollect = collect;
            this._mclamp = clamp;
            // 
            _activeObj = new List<GameUnit>();

        }

        // Spawn an object from our pool
        public GameUnit Spawn(Vector3 pos, Quaternion rot)
        {
            GameUnit obj = Spawn();

            obj.TF.SetPositionAndRotation(pos, rot);

            return obj;
        }

        public GameUnit Spawn()
        {
            GameUnit obj;
            if (_inactiveObj.Count == 0)
            {
                obj = (GameUnit)GameObject.Instantiate(prefab, _msRoot);

                if (!pools.ContainsKey(obj.GetInstanceID()))
                    pools.Add(obj.GetInstanceID(), this);
            }
            else
            {
                // Grab the last object in the inactive array
                obj = _inactiveObj.Dequeue();

                if (obj == null)
                {
                    return Spawn();
                }
            }

            if (_mCollect) 
            {
                _activeObj.Add(obj);
            }

            if (_mclamp && _activeObj.Count >= m_Amount) 
            {
                Despawn(_activeObj[0]);
            }
            obj.gameObject.SetActive(true);

            return obj;
        }

        // Return an object to the inactive pool.
        public void Despawn(GameUnit obj)
        {
            if (obj != null /*&& !inactive.Contains(obj)*/)
            {
                obj.gameObject.SetActive(false);
                _inactiveObj.Enqueue(obj);
            }

            if (_mCollect) 
            {
                _activeObj.Remove(obj);
            }
        }

        public void Clamp(int amount)
        {
            while (_inactiveObj.Count > amount)
            {
                GameUnit go = _inactiveObj.Dequeue();
                GameObject.DestroyImmediate(go);
            }
        }

        public void Release()
        {
            while (_inactiveObj.Count > 0)
            {
                GameUnit go = _inactiveObj.Dequeue();
                GameObject.DestroyImmediate(go);
            }
            _inactiveObj.Clear();
        }

        public void Collect()
        {
            while (_activeObj.Count > 0)
            {
                Despawn(_activeObj[0]);
            }
        }
    }
    #endregion


    #region All out of Pool

    static Dictionary<int, Pool> poolInstanceID = new Dictionary<int, Pool>();
    /// <summary>
    /// -> Init Object Pooling From ID - Initialize our dictionary.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="qty"></param>
    /// <param name="parent"></param>
    /// <param name="collect"></param>
    /// <param name="clamp"></param>
    static void Init(GameUnit prefab = null, int qty = DEFAULT_POOL_SIZE, Transform parent = null, bool collect = false, bool clamp = false)
    {
        if (prefab != null && !IsHasPool(prefab.GetInstanceID()))
        {
            poolInstanceID.Add(prefab.GetInstanceID(), new Pool(prefab, qty, parent, collect, clamp));
        }
    }
    /// <summary>
    /// Check ID has or not
    /// </summary>
    /// <param name="instanceID"></param>
    /// <returns></returns>
    static public bool IsHasPool(int instanceID)
    {
        return poolInstanceID.ContainsKey(instanceID);
    }

    static public void Preload(GameUnit prefab, int qty = 1, Transform parent = null, bool collect = false, bool clamp = false)
    {
        if (!poolTypes.ContainsKey(prefab.poolType))
        {
            poolTypes.Add(prefab.poolType, prefab);
        }

        if (prefab == null)
        {
            Debug.LogError(parent.name + " : IS EMPTY!!!");
            return;
        }

        Init(prefab, qty, parent, collect, clamp);

        // Make an array to grab the objects we're about to pre-spawn.
        GameUnit[] obs = new GameUnit[qty];
        for (int i = 0; i < qty; i++)
        {
            obs[i] = Spawn(prefab);
        }

        // Now despawn them all.
        for (int i = 0; i < qty; i++)
        {
            Despawn(obs[i]);
        }
    }
    /// <summary>
    /// Overloadding - Spawn Game Unit
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    /// <returns></returns>
    static public GameUnit Spawn(GameUnit obj, Vector3 pos, Quaternion rot)
    {
        if (!poolInstanceID.ContainsKey(obj.GetInstanceID()))
        {
            Transform newRoot = new GameObject(obj.name).transform;
            newRoot.SetParent(Root);
            Preload(obj, 1, newRoot, true, false);
        }

        return poolInstanceID[obj.GetInstanceID()].Spawn(pos, rot);
    }

    public static GameUnit Spawn(GameUnit obj)
    {
        if (!poolInstanceID.ContainsKey(obj.GetInstanceID()))
        {
            Transform newRoot = new GameObject(obj.name).transform;
            newRoot.SetParent(Root);
            Preload(obj, 1, newRoot, true, false);
        }

        return poolInstanceID[obj.GetInstanceID()].Spawn();
    }

    /// <summary>
    /// Despawn 
    /// </summary>
    /// <param name="obj"></param>
    static public void Despawn(GameUnit obj)
    {
        if (obj.gameObject.activeSelf)
        {
            if (pools.ContainsKey(obj.GetInstanceID()))
                pools[obj.GetInstanceID()].Despawn(obj);
            else
                GameObject.Destroy(obj.gameObject);
        }
    }

    static public void Collect(GameUnit obj)
    {
        if (poolInstanceID.ContainsKey(obj.GetInstanceID()))
            poolInstanceID[obj.GetInstanceID()].Collect();
    }

    static public void Release(GameUnit obj)
    {
        if (pools.ContainsKey(obj.GetInstanceID()))
        {
            pools[obj.GetInstanceID()].Release();
            pools.Remove(obj.GetInstanceID());
        }
        else
        {
            GameObject.DestroyImmediate(obj);
        }
    }

    static public T Spawn<T>(GameUnit obj, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        return Spawn(obj, pos, rot) as T;
    }

    #endregion
}

[System.Serializable]
public class PoolAmount
{
    [Header("-- Pool Amount --")]
    public Transform root;
    public GameUnit prefab;
    public int amount;
    public bool collect;
    public bool clamp;
}



/// <summary>
/// Generate Bot - Brick through by enum
/// </summary>
public enum PoolType
{
    Bot,
    Brick,
}
