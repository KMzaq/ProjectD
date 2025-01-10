using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float attackCoolDown;
    protected float attackCoolTime;


    protected virtual void Start()
    {
        attackCoolTime = attackCoolDown;
    }

    protected virtual void RotateUpdate(Vector3 dir)
    {

    }

    protected virtual void MovementUpdate(Vector3 dir)
    {

    }

    protected virtual void AttackUpdate()
    {

    }
}
