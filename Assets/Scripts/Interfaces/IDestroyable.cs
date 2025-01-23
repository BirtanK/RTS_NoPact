
public interface IDestroyable {
    public int Health {  get; set; }
    public bool IsDestroyed { get; set; }

    public void GetDamaged(in int damage);
    public void Destroy();
}
