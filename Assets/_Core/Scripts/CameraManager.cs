using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider)), RequireComponent(typeof(Rigidbody))]
public class CameraManager : MonoBehaviour
{
   private Transform playerCamera;
   
   /// <summary>
   /// Position et rotation de la caméra après un changement.
   /// </summary>
   public Transform AfterCameraPosition;
   public float Speed;

   private void Start()
   {
      playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
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
   }
}
