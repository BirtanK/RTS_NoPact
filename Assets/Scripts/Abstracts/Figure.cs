using UnityEngine;

public abstract class Figure : MonoBehaviour, IActionable, IDestroyable {
    #region Interface properties
    public int Health { get; set; }
    public bool IsDestroyed { get; set; }
    public GameObject SelectionIndicator { get; set; }
    #endregion

    [Header("References")]
    public Transform _transform;    // Faster reference
    //public Renderer _renderer;

    [Space(8)]
    public bool IsCommandableByPlayer;
    public bool IsFriendlyFigure = false;

    [HideInInspector] public CommandManager.IIssuedOrder? Order;
    
    //

    public virtual void GetDamaged(in int damage) {
        Health -= damage;
        if (Health <= 0) {
            Health = 0;
            Destroy();
        }
    }
    public abstract void Destroy();


    public abstract void Ordered(in float deltaTime);

}
