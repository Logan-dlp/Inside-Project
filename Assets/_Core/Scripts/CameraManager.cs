using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider)), RequireComponent(typeof(Rigidbody))]
public class CameraManager : MonoBehaviour
{
   private Transform playerCamera;
   private Camera camera;
   
   [Header("Camera settings")]
   public Transform AfterCameraPosition;
   public float Speed;
   public float SizeCam = 5;

   private void Start()
   {
      playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
      camera = playerCamera.GetComponent<Camera>();
   }

   private void OnTriggerStay(Collider _collider)
   {
      if (_collider.gameObject.layer == LayerMask.NameToLayer("Player"))
      {
         if (playerCamera != AfterCameraPosition)
         {
            MoveCamera();
         }
      }
   }

   void MoveCamera()
   {
      playerCamera.position = Vector3.Lerp(playerCamera.position, AfterCameraPosition.position, Time.deltaTime * Speed);
      playerCamera.rotation = Quaternion.Lerp(playerCamera.rotation, AfterCameraPosition.rotation, Time.deltaTime * Speed);
      camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, SizeCam, Time.deltaTime * Speed);
   }
}
