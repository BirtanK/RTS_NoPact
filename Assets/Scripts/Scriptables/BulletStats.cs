using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Bullet stats", fileName = "_BulletStats")] 
public class BulletStats : ScriptableObject {
    [System.Serializable] public struct BulletStat {
        public int Damage;
        public float Speed;
        public int Pierce;
        public int Demolish;
        public AnimationCurve MovementCurve;
    }
    [System.Serializable] public struct BulletStat_P {
        public int Damage;
        public float Speed;
        public int Pierce;
        public int Demolish;
        public AnimationCurve MovementCurve;
        public int StatIndex;

        public BulletStat_P(in BulletStat unitStat, in int index) {
            Damage = unitStat.Damage;
            Speed = unitStat.Speed;
            Pierce = unitStat.Pierce;
            Demolish = unitStat.Demolish;
            MovementCurve = unitStat.MovementCurve;

            StatIndex = index;
        }
    }
    public BulletStat bulletStat;
    public BulletAgent BulletPrefab;
}
