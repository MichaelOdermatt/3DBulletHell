using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireRaycast : MonoBehaviour
{
    private float range = 100f;
    private float damage = 25f;
    private float bulletSpeed = 7;

    private float shootDelay = 0.1f;
    private float lastShootTime;

    public TrailRenderer bulletTrail;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (lastShootTime + shootDelay > Time.time)
        {
            return;
        }
        lastShootTime = Time.time;

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.forward, out hit, range);

        TrailRenderer trail = Instantiate(bulletTrail, transform.position, Quaternion.identity);
        if (hit.collider != null)
        {
            StartCoroutine(spawnTrailThenDamage(trail, hit));
        } else
        {
            Vector3 hitPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + range);
            StartCoroutine(spawnTrailToPoint(trail, hitPoint));
        }
    }

    private IEnumerator spawnTrailThenDamage(TrailRenderer trail, RaycastHit hit)
    {
        yield return spawnTrailToPoint(trail, hit.point);

        // Make sure the object hasn't been destroyed by another coroutine before potentially destroying it.
        if (hit.transform != null)
        {
            causeDamageIfKillable(hit);
        }
    }

    private IEnumerator spawnTrailToPoint(TrailRenderer trail, Vector3 point)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, point, time);
            time += bulletSpeed * Time.deltaTime;

            yield return null;
        }
        trail.transform.position = point;

        Destroy(trail.gameObject, trail.time);
    }

    private void causeDamageIfKillable(RaycastHit hit)
    {
        Killable killable = hit.transform.GetComponent<Killable>();
        if (killable != null)
        {
            killable.TakeDamage(damage);
        }
    }
}
