using UnityEngine;

public class Enemy : Actor
{
    private Animator m_animator;

    [SerializeField]
    private GameObject target;

    protected override void Start()
    {
        base.Start();
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 vec = target.transform.position - this.gameObject.transform.position;
        vec.y = 0;
        if (Vector3.Distance(target.transform.position, this.transform.position) < 10)
        {
            MovementUpdate(vec.normalized);
            RotateUpdate(vec);
        }
        else
        {
            m_animator.SetBool("IsMove", false);
        }
    }

    protected override void RotateUpdate(Vector3 dir)
    {
        base.RotateUpdate(dir);
        this.transform.rotation = Quaternion.LookRotation(dir);
    }

    protected override void MovementUpdate(Vector3 dir)
    {
        base.MovementUpdate(dir);
        m_animator.SetBool("IsMove", true);
        this.gameObject.transform.position += dir * speed * Time.deltaTime;
    }
}
