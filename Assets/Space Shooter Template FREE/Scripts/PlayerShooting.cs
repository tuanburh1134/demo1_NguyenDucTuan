using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class chứa các bộ phận súng để quản lý trong Inspector
[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun;
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX;
}

public class PlayerShooting : MonoBehaviour
{

    [Tooltip("Tốc độ bắn. Số càng lớn bắn càng nhanh")]
    public float fireRate = 5f; // Đặt mặc định là 5

    [Tooltip("Prefab viên đạn")]
    public GameObject projectileObject;

    // Thời gian cho lần bắn tiếp theo
    [HideInInspector] public float nextFire;

    [Tooltip("Sức mạnh hiện tại của vũ khí (1-4)")]
    [Range(1, 4)]
    public int weaponPower = 1;

    public Guns guns;

    [HideInInspector] public int maxweaponPower = 4;
    public static PlayerShooting instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        // Tự động tìm ParticleSystem nếu có (Sử dụng TryGetComponent để tránh lỗi nếu không có hiệu ứng)
        if (guns.leftGun) guns.leftGun.TryGetComponent(out guns.leftGunVFX);
        if (guns.rightGun) guns.rightGun.TryGetComponent(out guns.rightGunVFX);
        if (guns.centralGun) guns.centralGun.TryGetComponent(out guns.centralGunVFX);
    }

    private void Update()
    {
        // Thêm điều kiện Input.GetMouseButton(0) để chỉ bắn khi nhấn chuột trái
        if (Input.GetMouseButton(0))
        {
            if (Time.time > nextFire)
            {
                MakeAShot();
                nextFire = Time.time + 1f / fireRate; // Công thức tính thời gian hồi chiêu
            }
        }
    }

    // Hàm thực hiện bắn
    void MakeAShot()
    {
        switch (weaponPower)
        {
            case 1:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                guns.centralGunVFX?.Play(); // Dấu ? nghĩa là: nếu có hiệu ứng thì Play, không thì thôi (tránh lỗi)
                break;
            case 2:
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, Vector3.zero);
                guns.leftGunVFX?.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, Vector3.zero);
                guns.rightGunVFX?.Play();
                break;
            case 3:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                guns.leftGunVFX?.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                guns.rightGunVFX?.Play();
                break;
            case 4:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                guns.leftGunVFX?.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                guns.rightGunVFX?.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 15));
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -15));
                break;
        }
    }

    // Hàm tạo viên đạn
    void CreateLazerShot(GameObject lazer, Vector3 pos, Vector3 rot)
    {
        Instantiate(lazer, pos, Quaternion.Euler(rot));
    }
}