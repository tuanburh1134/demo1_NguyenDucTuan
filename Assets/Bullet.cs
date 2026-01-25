using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float flySpeed; // Tốc độ bay của đạn

    // Update is called once per frame
    void Update()
    {
        var newPosition = transform.position;
        newPosition.y += Time.deltaTime * flySpeed; // Thay đổi vị trí theo trục Y
        transform.position = newPosition;
    }
}