using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    Transform CarPos;
    Transform CameraFollowerPos;
    Rigidbody CarRb;
    [Tooltip("The larger the value, the more it will approach target.")]
    public float FollowRate;
    [Tooltip("The larger the value, the more it will approach target.")]
    public float RotationThreshold;
    public float RotationSpeed;
  
   



    private void Awake()
    {
        CameraFollowerPos = this.transform;
        CarPos = CameraFollowerPos.parent;
        CarRb = CarPos.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        CameraFollowerPos.parent = null;
    }

    private void FixedUpdate()
    {
        //position
        Vector3 PosUpdate = Vector3.Lerp(CameraFollowerPos.position, CarPos.position, FollowRate * Time.fixedDeltaTime);
        CameraFollowerPos.position = PosUpdate;
        //Rotation
        Quaternion RotUpdate;
        if (CarRb.velocity.magnitude < RotationThreshold)
        {
            RotUpdate = Quaternion.LookRotation(CarPos.forward);
        }
        else
        {
            RotUpdate = Quaternion.LookRotation(CarRb.velocity.normalized);

        }
        CameraFollowerPos.rotation = Quaternion.Slerp(CameraFollowerPos.rotation, RotUpdate, RotationSpeed * Time.fixedDeltaTime);
       
    }
}
