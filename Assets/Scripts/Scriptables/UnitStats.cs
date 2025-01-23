using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Unit stats", fileName = "_UnitStats")]
public class UnitStats : ScriptableObject {
    [System.Serializable] public struct UnitStat {
        public int Health;
        public float Speed;
        public float Range;
        public float AttackSpeed;
        public ObjectPooler.PoolObject BulletPrefab;
    }
    [System.Serializable] public struct UnitStat_P {
        public int Health;
        public float Speed;
        public float Range;
        public float AttackSpeed;
        public int BulletIndex;

        public int StatIndex;

        public UnitStat_P(in UnitStat unitStat, in int index) {
            Health = unitStat.Health;
            Speed = unitStat.Speed;
            Range = unitStat.Range;
            AttackSpeed = unitStat.AttackSpeed;

            BulletIndex = InGameReferencer.ObjectPooler.AddItem(unitStat.BulletPrefab);
            StatIndex = index;
        }
        public UnitStat_P(in UnitStat unitStat, in int bulletIndex, in int statIndex) {
            Health = unitStat.Health;
            Speed = unitStat.Speed;
            Range = unitStat.Range;
            AttackSpeed = unitStat.AttackSpeed;

            BulletIndex = bulletIndex;
            StatIndex = statIndex;
        }
    }
    public UnitStat unitStat;
    public UnitFigure UnitPrefab;
}
