using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(float _amount);

    public void Push(Vector3 _force);
    public void Kill();
}
