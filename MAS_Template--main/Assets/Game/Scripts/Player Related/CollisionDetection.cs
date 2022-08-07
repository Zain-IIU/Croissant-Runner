using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
   private StackManager _stackManager;
   private Dough _dough;
  [SerializeField] private PlayerParticles playerParticles;
  [SerializeField] private Transform playerTransform;

  private bool isDoughNow;
   private void Start()
   {
      _stackManager=StackManager.Instance;
      _dough = GetComponent<Dough>();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.TryGetComponent(out GateManager gateManager))
      {
         playerParticles.PlayIngredientVFX();
         gateManager.TakeAction();
         other.enabled = false;
      }
      
      if (other.TryGetComponent(out ModiferManager modiferManager))
      {
         if(GetComponent<Dough>())
         {
            if (GetComponent<Dough>().IsPartOf())
            {
               MASGameEvents.instance.Haptic(HapticTypes.Selection);
               modiferManager.TakeAction(GetComponent<Dough>());
            }
               
            return;
         }
      }

      if (other.TryGetComponent(out ToppingsManager toppingsManager))
      {
         if (_dough.GETCurState() == DoughState.UnBakedCroissant || _dough.GETCurState() == DoughState.BakedCroissant)
         {
            playerParticles.PlayFillingsVFX();
            MASGameEvents.instance.Haptic(HapticTypes.Selection);
            toppingsManager.ApplyTopping(GetComponent<DoughTopping>());
         }
         
            
      }
      
      if (other.TryGetComponent(out Dough dough) && other.gameObject.CompareTag("PickUp") && gameObject.CompareTag("Dough"))
      {
         if (dough.IsPartOf()) return;
         MASGameEvents.instance.Haptic(HapticTypes.RigidImpact);
         _stackManager.AddToStack(dough);
      }

      if (other.gameObject.CompareTag("Trap") && transform.CompareTag("Dough"))
      {
         other.enabled = false;
         other.transform.parent.DOScaleY(-0.5f, 0.5f);
         _stackManager.RemoveFromStack(_dough.GetID());
         MASGameEvents.instance.Haptic(HapticTypes.Warning);
         playerParticles.PlayObstacleVFX();
      }

      if (other.gameObject.CompareTag("Finish"))
      {
         other.enabled = false;
         EventsManager.ReachedEnd();
      }

      if (other.TryGetComponent(out Mixer mixer) )
      {
         if(isDoughNow) return;
         other.enabled = false;
         isDoughNow = true;
         playerTransform.DOMoveX(mixer.transform.position.x, .25f);
         mixer.CreateDoughFromMixture();
      }
   }
}
