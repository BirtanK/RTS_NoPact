using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureFigure : Figure {
    public int StructureArmor = 10;
    public int MaxHealth = 500;
    public Renderer _renderer;
    public Material Broken;



    private void Start() {
        Health = MaxHealth;
    }

    public override void GetDamaged(in int damage) {
        base.GetDamaged(damage.ArmorMitigation(StructureArmor));
        Debug.Log(Health);
    }
    public override void Destroy() {
        // yikil
        gameObject.layer = 1;
        _renderer.material = Broken;
        transform.localScale = new Vector3(2, 0.5f, 2);
        Destroy(this);
    }

    public override void Ordered(in float deltaTime) {
        // none
    }
}
