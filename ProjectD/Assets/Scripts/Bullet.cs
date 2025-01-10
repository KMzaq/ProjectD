using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;

    [SerializeField]
    private float speed;

    private float timer;

    void Update()
    {
        this.gameObject.transform.position += this.gameObject.transform.forward * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > lifeTime) Destroy(this.gameObject);
    }
}
