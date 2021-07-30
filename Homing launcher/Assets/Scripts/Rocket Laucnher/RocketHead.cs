using System.Collections;
using UnityEngine;

public class RocketHead : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private float damage;

    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private Transform Target;
    
    public bool smartRocket;
    private bool rotateTo;
    private bool AssingedTarget = false;

    private void Update()
    {
        if (smartRocket)
        {
            GetTarget();
        }

        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        if (rotateTo == true)
        {
            Vector3 relativePos = Target.position - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.forward);
            transform.rotation = rotation;
            GetComponent<Rigidbody>().velocity = (transform.forward * 500) * Time.fixedDeltaTime;
        }
    }

    void GetTarget()
    {
        if (AssingedTarget) return;
        if (GameObject.FindGameObjectWithTag("Target") == null) return;
        Target = GameObject.FindGameObjectWithTag("Target").transform.parent;
        AssingedTarget = true;
        StartCoroutine(StartRotate(.4f));
    }

    IEnumerator StartRotate(float Time)
    {
        yield return new WaitForSeconds(Time);
        rotateTo = true;
    }

    
    private void OnCollisionEnter(Collision other)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider col in hitColliders)
        {
            if (col.tag == "Enemy")
            {
                col.GetComponent<health>().TakeDamage(damage);
            }
        }

        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        Vector3 SpawnPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        ParticleSystem rocket = Instantiate(explosion, SpawnPos, Quaternion.identity);
        var main = rocket.main;
        main.startSize = explosionRadius;
    }
}
