using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [SerializeField] private GunBullet bulletPrefab;
    [SerializeField] private int poolSize = 20;

    private Queue<GunBullet> bulletPool = new Queue<GunBullet>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GunBullet bullet = Instantiate(bulletPrefab, transform);
            bullet.gameObject.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public GunBullet GetBullet()
    {
        if (bulletPool.Count == 0)
        {
            GunBullet bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(false);
            bulletPool.Enqueue(bullet);
        }

        GunBullet bulletToUse = bulletPool.Dequeue();
        bulletToUse.gameObject.SetActive(true);
        return bulletToUse;
    }

    public void ReturnBullet(GunBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
