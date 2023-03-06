using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Killzone2D : MonoBehaviour
{
    private enum ZoneType { Square, Circle }
    private enum DamageType { OnEnter, Continuous }
    private enum UpdateType { Update, LateUpdate, FixedUpdate }

    [SerializeField] private DamageType damageType;
    [SerializeField] private UpdateType updateType;
    [SerializeField] private ZoneType zoneType;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool kill;

    [Header("Only if kill is not enable")]
    [SerializeField] private float damage;

    [Header("Only for Square")]
    [SerializeField] private Vector2 size;

    [Header("Only for Circle")]
    [SerializeField] private float radius;

    //PreviousFrame
    private List<IDamagable> damagablesInZone = new List<IDamagable>();

    private void Update()
    {
        if (updateType == UpdateType.Update)
            CheckZone();
    }

    private void LateUpdate()
    {
        if (updateType == UpdateType.LateUpdate)
            CheckZone();
    }

    private void FixedUpdate()
    {
        if (updateType == UpdateType.FixedUpdate)
            CheckZone();
    }

    private void CheckZone()
    {
        List<Collider2D> colliders;

        if (zoneType == ZoneType.Square)
            colliders = Physics2D.OverlapBoxAll(transform.position, size, transform.rotation.z, layerMask).ToList();
        else
            colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask).ToList();

        List<IDamagable> damagables = colliders.ChangeToComponentList<Collider2D, IDamagable>();
        damagables = damagables.ClearDuplicate();

        foreach (var d in damagables)
        {
            if (damageType == DamageType.Continuous)
            {
                if (kill)
                    d.Kill();
                else
                    d.TakeDamage(damage);
            }
            else if (damageType == DamageType.OnEnter)
            {
                if (!damagablesInZone.Contains(d))
                {
                    if (kill)
                        d.Kill();
                    else
                        d.TakeDamage(damage);
                }
            }
        }

        damagablesInZone = damagables;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if(kill)
            Gizmos.color = new Color(0, 0, 0, 0.5f);
        else
            Gizmos.color = new Color(1, 0, 0, 0.5f);

        if (zoneType == ZoneType.Circle)
        {
            if (Selection.activeGameObject == gameObject)
                Gizmos.DrawSphere(transform.position, radius);
            else
                Gizmos.DrawSphere(transform.position, radius);
        }
        else if (zoneType == ZoneType.Square)
        {
            Matrix4x4 matrix = Gizmos.matrix;

            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

            if (Selection.activeGameObject == gameObject)
                Gizmos.DrawCube(Vector3.zero, size);
            else
                Gizmos.DrawWireCube(Vector3.zero, size);

            Gizmos.matrix = matrix;
#endif
        }
    }
}
