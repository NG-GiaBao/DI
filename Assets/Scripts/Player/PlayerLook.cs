using System;
using UnityEngine;

[Serializable]
public class PlayerLook
{
    [SerializeField] private float sensitivity; // độ nhạy chuột
    [SerializeField] private float yaw; // xoay ngang
    [SerializeField] private float pitch; // xoay dọc
    [SerializeField] private float playerViewAngle; // góc nhìn của người chơi


    public void Look(Vector2 input, Transform player , Transform camera)
    {
        if(player == null || camera == null)
        {
            Debug.LogError("Player or Camera transform is null!");
            return;
        }
        yaw += input.x * sensitivity;
        yaw = Mathf.Repeat(yaw, 360f);
        pitch -= input.y * sensitivity;
        pitch = Mathf.Clamp(pitch, -playerViewAngle, playerViewAngle);
        player.localRotation = Quaternion.Euler(0, yaw, 0);
        camera.localRotation = Quaternion.Euler(-pitch, 0, 0);

    }
}
