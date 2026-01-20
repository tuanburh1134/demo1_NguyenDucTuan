using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;

public class ShowLog : MonoBehaviour
{
    // Start chạy 1 lần khi bắt đầu
    void Start()
    {
        Debug.Log("Hello World!");
    }

    // Update chạy liên tục mỗi khung hình
    void Update()
    {
        // Bạn có thể thử dòng này sau
        // Debug.Log("Update called! " + Time.frameCount);
    }
}