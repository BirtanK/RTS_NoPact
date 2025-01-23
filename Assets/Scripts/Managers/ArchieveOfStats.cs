using System.Collections.Generic;
using UnityEngine;

public class ArchieveOfStats : MonoBehaviour {
    public UnitStats[] Units;
    public BulletStats[] Bullets;

    [System.Serializable] public struct Starter {
        public Vector3 Position;
        public int UnitIndex;
        public bool IsCommandable;
        public bool IsFriendly;
    }
    public List<Starter> StarterUnits;

    //

    void Start() {
        // unit & bullet pool
        byte _i = 0;
        foreach (var bullet in Bullets) {
            bullet.BulletPrefab.bulletStats = new(bullet.bulletStat, _i++);
            InGameReferencer.ObjectPooler.AddItem(new() {
                name = bullet.BulletPrefab.name,
                count = 10,
                extend = true,
                behaviour = bullet.BulletPrefab
            });
        }

        _i = 0;
        foreach (var unit in Units) {
            unit.UnitPrefab.unitStat = new(unit.unitStat, _i++);
            InGameReferencer.ObjectPooler.AddItem(new() {
                name = unit.UnitPrefab.name,
                count = 10,
                extend = true,
                behaviour = unit.UnitPrefab
            });
        }

        // Starter

        UnitFigure started;
        foreach (Starter starter in StarterUnits) {
            started = InGameReferencer.ObjectPooler.PullItem<UnitFigure>(Bullets.Length + starter.UnitIndex, starter.Position);
            started.IsCommandableByPlayer = starter.IsCommandable;
            started.IsFriendlyFigure = starter.IsFriendly;

            InGameReferencer.ActionManager.Units.Add(started);
        }
    }
}
