using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public float rotCamXAxisSpeed = 5;
    public float rotCamYAxisSpeed = 3;

    float limitMinx = -80;
    float limitMaxx = 50;
    float eulerAngleX;
    float eulerAngleY;

    public void UpdateRotate(float mouseX,float mouseY)
    {
        eulerAngleY += mouseX * rotCamYAxisSpeed;
        eulerAngleX -= mouseY * rotCamXAxisSpeed;

        eulerAngleX = ClampAngle(eulerAngleX, limitMinx, limitMaxx);

        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY,0);

    }
        
    float ClampAngle(float angle, float min,float max)
    {
        if (angle <- 360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }


}
