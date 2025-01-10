using UnityEngine;
using System.Collections;

public class Character : Actor
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject springArm;
    [SerializeField]
    private GameObject hand;
    [SerializeField]
    private GameObject model;


    private Camera mainCamera;

    private Vector2 ScreenCenterPosition;

    private bool IsRotateUpdate;


    protected override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
        ScreenCenterPosition = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        IsRotateUpdate = true;
    }

    
    void Update()
    {
        InputUpdate();
        AttackUpdate();
        ViewUpdate();
    }

    private void ViewUpdate()
    {
        Vector3 cameraPosition = new Vector3(Input.mousePosition.x - ScreenCenterPosition.x, 0, Input.mousePosition.y - ScreenCenterPosition.y);

        cameraPosition.x /= Screen.width;
        cameraPosition.z /= Screen.height;

        springArm.transform.localPosition = cameraPosition * 2.0f;
    }

    private void InputUpdate()
    {
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection.z = -1;
        }
        moveDirection.Normalize();

        MovementUpdate(moveDirection);

        //방향
        if (IsRotateUpdate)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                Vector3 vec = hit.point - this.gameObject.transform.position;
                vec.y = 0;

                RotateUpdate(vec);
            }
        }
    }

    protected override void RotateUpdate(Vector3 dir)
    {
        base.RotateUpdate(dir);
        model.transform.rotation = Quaternion.LookRotation(dir);
    }

    protected override void MovementUpdate(Vector3 dir)
    {
        float curspeed = speed;

        if (Input.GetKey(KeyCode.LeftShift)) curspeed *= 1.5f;

        this.gameObject.transform.position += dir * curspeed * Time.deltaTime;
    }

    protected override void AttackUpdate()
    {
        if (attackCoolTime < attackCoolDown)
        {
            attackCoolTime += Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bullet, this.gameObject.transform.position, model.transform.rotation);
            attackCoolTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(MeleeAttack());
            attackCoolTime = 0;
        }
    }

    IEnumerator MeleeAttack()
    {
        float timer = 0;
        IsRotateUpdate = false;
        while (true)
        {
            float f = model.transform.eulerAngles.y + (-130 * timer * 5);

            hand.transform.rotation = Quaternion.Euler(0, f, 0);


            //-130 까지 1초동안
            timer += Time.deltaTime;
            if (timer > 0.2f) break;
            yield return null;
        }
        yield return new WaitForSeconds(0.15f);

        IsRotateUpdate = true;
        hand.transform.localRotation = Quaternion.Euler(0, 0, 0);
        yield break;
    }
}
