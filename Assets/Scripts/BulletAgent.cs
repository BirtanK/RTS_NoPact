using UnityEngine;

public class BulletAgent : MonoBehaviour, ObjectPooler.IPooled {
    public Transform _transform;
    public BulletStats.BulletStat_P bulletStats;

    [Space(8)]
    public Vector3 TargetPoint;
    public Figure Target;

    //

    private float lerpTimer;
    private void FixedUpdate() {
        if (Target == null) {
            Destroy(gameObject);
        }

        lerpTimer += Time.deltaTime * bulletStats.Speed;
        _transform.position = Vector3.Lerp(originPoint, Target? Target._transform.position : TargetPoint, bulletStats.MovementCurve.Evaluate(lerpTimer));

        if (lerpTimer >= 1) {
            Target.GetDamaged(bulletStats.Damage);
            Destroy(gameObject);
        }
    }

    private Vector3 originPoint;
    public void Born() {
        originPoint = transform.position;
        lerpTimer = 0;
    }
    public void Die() {
        // none
    }
}
