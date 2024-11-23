using UnityEngine;

public static class QuaternionExtensions
{
    public static Quaternion WithX(this Quaternion q, float x) =>  
        Quaternion.Euler(x, q.eulerAngles.y, q.eulerAngles.z);
    public static Quaternion WithY(this Quaternion q, float y) =>  
        Quaternion.Euler(q.eulerAngles.x, y, q.eulerAngles.z);
    public static Quaternion WithZ(this Quaternion q, float z) =>  
        Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, z);

}