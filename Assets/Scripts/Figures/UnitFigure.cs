using UnityEngine;

public class UnitFigure : Figure, ObjectPooler.IPooled {
    public UnitStats.UnitStat_P unitStat;

    public override void Destroy() {
        // die
        Destroy(gameObject);
    }

    private Vector3 currentVelocity;
    private Vector3 direction;
    private float _dot;
    private BulletAgent _agent;
    private float attackTimer;
    public override void Ordered(in float deltaTime) {
        //Debug.Log(Order);
        if (Order == null) return;

        // act according to the last order
        switch (Order.Command) {
            case CommandManager.CommandType.Move:
                direction = (Order as CommandManager.MoveOrder?).Value.TargetPoint - _transform.position;
                if (direction.magnitude < 1) {
                    Order = null;
                    return;
                }

                _dot = Vector3.Dot(_transform.forward, direction.normalized);
                if (_dot < 1) _transform.rotation = Quaternion.RotateTowards(_transform.rotation, Quaternion.LookRotation(direction.normalized, Vector3.up), 360 * deltaTime);
                _transform.position = Vector3.SmoothDamp(_transform.position, (Order as CommandManager.MoveOrder?).Value.TargetPoint, ref currentVelocity, 0.3f, unitStat.Speed * _dot, deltaTime);
                break;
            case CommandManager.CommandType.Attack:
                if ((Order as CommandManager.AttackOrder?).Value.Target == null) {
                    Order = null;
                    return;
                }
                direction = (Order as CommandManager.AttackOrder?).Value.Target._transform.position - _transform.position;
                
                _dot = Vector3.Dot(_transform.forward, direction.normalized);
                if (_dot < 1) _transform.rotation = Quaternion.RotateTowards(_transform.rotation, Quaternion.LookRotation(direction.normalized, Vector3.up), 360 * deltaTime);
                if (direction.magnitude < unitStat.Range && _dot == 1) {    // attack
                    attackTimer += deltaTime;

                    if (attackTimer >= 1 / unitStat.AttackSpeed) {
                        attackTimer -= 1 / unitStat.AttackSpeed;
                        _agent = InGameReferencer.ObjectPooler.PullItem<BulletAgent>(unitStat.BulletIndex, _transform.position);//Instantiate(unitStat.BulletPrefab, _transform.position, Quaternion.identity);
                        _agent.Target = (Order as CommandManager.AttackOrder?).Value.Target;
                    }
                } else {    // follow
                    attackTimer = 0;
                    _transform.position = Vector3.SmoothDamp(_transform.position, (Order as CommandManager.AttackOrder?).Value.Target._transform.position, ref currentVelocity, 0.3f, unitStat.Speed * _dot, deltaTime);
                }
                break;
            default:
                return;
        }
    }

    public void Born() {
        unitStat = new (InGameReferencer.ArchieveOfStats.Units[unitStat.StatIndex].unitStat, unitStat.BulletIndex, unitStat.StatIndex);
        Health = unitStat.Health;
        IsDestroyed = false;

        Order = null;
        currentVelocity = default;
        attackTimer = 0;
    }
    public void Die() {
        Destroy();
    }
}
