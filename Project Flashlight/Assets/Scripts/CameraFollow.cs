using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float followSpeed = 2f;

    void LateUpdate()
    {
        float interpolation = followSpeed * Time.deltaTime;
        Vector3 position = transform.position;
        position.x = Mathf.Lerp(position.x, player.transform.position.x, interpolation);
        position.y = Mathf.Lerp(position.y, player.transform.position.y, interpolation);
        transform.position = position;
    }
}
