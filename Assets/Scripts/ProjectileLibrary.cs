using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectileLibrary
{

    public static void CalculatePathFromLaunchToTarget(Vector3 target, Vector3 launch, out Vector3 normalizedDirection, out float height, out float v0, out float time, out float angle)
    {
        Vector3 direction = ProjectileLibrary.GetDirection(target, launch);
        Vector3 groundDirection = ProjectileLibrary.GetGroundDirection(direction);
        Vector3 targetPos = ProjectileLibrary.GetTargetPosition(target, launch);
        height = ProjectileLibrary.GetHeight(target, launch);
        normalizedDirection = groundDirection.normalized;
        CalculatePathWithHeight(targetPos, height, out v0, out time, out angle);
    }

    public static void CalculatePathWithHeight (Vector3 targetPos, float h, out float v0, out float time, out float angle)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float b = Mathf.Sqrt(2 * g * h);
        float a = (-0.5f * g);
        float c = -yt;

        float tplus = QuadraticEquation(a, b, c, 1);
        float tmin = QuadraticEquation(a, b, c, -1);
        time = tplus > tmin ? tplus : tmin;
        angle = Mathf.Atan(b * time / xt);
        v0 = b / Mathf.Sin(angle);
    }
    
    public static Vector3 GetDirection (Vector3 target, Vector3 launch)
    {
        return target - launch;
    }

    public static Vector3 GetGroundDirection(Vector3 direction)
    {
        return new Vector3(direction.x, 0, direction.z);
    }

    public static Vector3 GetTargetPosition(Vector3 target, Vector3 launch)
    {
        Vector3 direction = GetDirection(target, launch);
        Vector3 groundDirection = GetGroundDirection(direction);
        return new Vector3(groundDirection.magnitude, direction.y, 0);
    }

    public static float GetHeight(Vector3 target, Vector3 launch)
    {
        Vector3 targetPos = GetTargetPosition(target, launch);
        return Mathf.Max(0.01f, targetPos.y + targetPos.magnitude / 2f);
    }

    public static Vector3 GetPositionAtTime(Vector3 origin, Vector3 direction, float v0, float angle, float t)
    {
        float x = v0 * t * Mathf.Cos(angle);
        float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
        return origin + direction * x + Vector3.up * y;
    }

    private static float QuadraticEquation(float a, float b, float c, float sign)
    {
        return (-b + sign * Mathf.Sqrt(b*b - 4 * a * c)) / (2 * a);
    }
}
