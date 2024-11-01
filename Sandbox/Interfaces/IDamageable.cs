namespace Key_Quest.Sandbox.Interfaces;

public interface IDamageable
{
    public void TakeDamage(int value);
    public int Health { get; set; }
}