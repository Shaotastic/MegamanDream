using UnityEngine;
using System.Collections;
using System;

public class Bullet : Gun
{
    int bulletDamage;
    int range;
    float speed;
    public Vector3 startPosition;

    //bool del;
    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsPaused)
            return;

        if (Vector3.Distance(startPosition, transform.position) > range)
            Destroy(this.gameObject);

        if(IsFacingRight())
            transform.position += transform.right * Time.deltaTime * speed;
        else
            transform.position += -transform.right * Time.deltaTime * speed;

    }

    public void SetBulletSpeed(float num)
    {
        speed = num;
    }

    public void SetBulletDamage(int v)
    {
        bulletDamage = v;
    }

    public void SetBulletRange(int num)
    {
        range = num;
    }

    public void SetDirection(bool res)
    {
        FacingRight(res);
    }

    public int GetBulletDamage()
    {
        return bulletDamage;
    }
}
