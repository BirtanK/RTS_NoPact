using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    [System.Serializable] public struct PoolObject {
        public string name;
        public int count;
        public bool extend;
        public MonoBehaviour behaviour;
    }
    public struct Pooled {
        public string name;
        public int Index;
        public bool extend;
        public MonoBehaviour Reference;
        public Transform Actives;
        public Transform Passives;

        public Pooled(in PoolObject po) {
            name = po.name;
            Index = InGameReferencer.ObjectPooler.Pool.Count;
            extend = po.extend;

            var parent = new GameObject().transform;
            parent.name = po.name;
            parent.parent = InGameReferencer.ObjectPooler.PoolParent;

            Reference = Instantiate(po.behaviour, parent);
            Reference.gameObject.SetActive(false);
            Reference.name = "Reference";

            Actives = new GameObject("Actives").transform;
            Actives.parent = parent;

            Passives = new GameObject("Passives").transform;
            Passives.parent = parent;
        }
    }
    public interface IPooled {
        public void Born();
        public void Die();
    }

    public PoolObject[] InnatePool;
    public Transform PoolParent;

    public List<Pooled> Pool = new();

    //

    private void GenerateItem(in int Index) {
        Instantiate(Pool[Index].Reference, Pool[Index].Passives);
    }

    int index;
    public int AddItem(PoolObject po) {
        index = Pool.FindIndex((p1) => { return p1.name == po.name; });
        if (index != -1) {
            return index;
        }

        Pool.Add(new(po));
        index = Pool.Count - 1;

        for (byte _i = 0; _i < po.count; _i++) {
            GenerateItem(index);
        }
        return index;
    }

    private Transform pulled;
    public T PullItem<T>(in int Index, in Vector3 pos)
        where T : MonoBehaviour {
        if (Pool[Index].Passives.childCount == 0) {
            if (Pool[Index].extend) {
                GenerateItem(Index);
            } else return null;
        }

        pulled = Pool[Index].Passives.GetChild(0);
        pulled.parent = Pool[Index].Actives;
        pulled.gameObject.SetActive(true);
        pulled.position = pos;
        pulled.name = Pool[Index].name;

        pulled.GetComponent<IPooled>().Born();

        return pulled.GetComponent<T>();
    }

    public void SleepItem(in int Index, Transform Item) {
        Item.parent = Pool[Index].Passives;
        Item.GetComponent<IPooled>().Die();
    }

    private void Start() {
        foreach (PoolObject _po in InnatePool) {
            AddItem(_po);
        }
    }
}
