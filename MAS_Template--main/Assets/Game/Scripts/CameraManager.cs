using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
   [SerializeField] private GameObject startCam;
   [SerializeField] private GameObject followCamPlayer;
   [SerializeField] private GameObject followCamChef;
   [SerializeField] private GameObject endCamera;

   private void Start()
   {
      EventsManager.ONGameStart += StartFollowCamera;
      EventsManager.ONReachedEnd += EnableChefCamera;
      EventsManager.ONCollisionNextLevel += EnableEndCamera;
   }

   private void StartFollowCamera()
   {
      startCam.SetActive(false);
      followCamPlayer.SetActive(true);
   }

   private void EnableChefCamera()
   {
      followCamChef.SetActive(true);
      followCamPlayer.SetActive(false);
   }

   private void EnableEndCamera()
   {
      endCamera.SetActive(true);
      followCamChef.SetActive(false);
   }

   private void OnDestroy()
   {
      EventsManager.ONGameStart -= StartFollowCamera;
      EventsManager.ONReachedEnd -= EnableChefCamera;
      EventsManager.ONCollisionNextLevel -= EnableEndCamera;
   }
}
