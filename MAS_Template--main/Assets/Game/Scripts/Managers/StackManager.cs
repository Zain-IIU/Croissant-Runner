using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class StackManager : MonoBehaviour
{
      public static StackManager Instance;

      [SerializeField] private List<Dough> doughs = new List<Dough>();
      [SerializeField] private List<Dough> finalDoughs = new List<Dough>();
      [SerializeField] private Transform doughStackPoint;

      [SerializeField] private GameObject coinPrefab;
      [SerializeField] private Transform endStackPoint;
      [SerializeField] private Transform moneyPoint;
      [SerializeField] private Transform counterStackPoint;
      private bool shouldAttachToCook;
      private void Awake()
      {
            Instance = this;
      }

      private void Start()
      {
            EventsManager.ONConvertedToDough += AddFirstDough;
            EventsManager.ONReachedEnd += ReTransformation;
      }


      private void AddFirstDough()
      {
            Dough newDough = doughStackPoint.GetChild(0).GetComponent<Dough>();
            if (newDough.IsPartOf()) return;
            doughs.Add(newDough);
            newDough.MakePartOfDough();
      }

      public void AddToStack(Dough newDough)
      {
            if (newDough.IsPartOf() || newDough.GETCurState()==DoughState.InComplete) return;
          
            doughs.Add(newDough);
            if (doughs.Count > 1)
            {
                  newDough.UpdateTarget(doughs[doughs.Count-2].transform);
                  newDough.transform.tag = "Dough";
                  newDough.MakePartOfDough();
            }
                  
            StartCoroutine(nameof(PlayDoughAnimation));
      }

      public void RemoveFromStack(int doughID)
      {
            var result = SearchDough(doughID);
            if (result == -1 || result==0) return;
            print("Removing at "+ result);
            EventsManager.CollidedWithObstacle();
            foreach (var dough in doughs)
            {
                  dough.UpdateSpeedAndOffset();
            }
            var multiple = 1;
            
            for (var i = result; i < doughs.Count; i++)
            {
                  print("Removing at "+i);
                  doughs[i].transform.tag = "PickUp";
                  print(doughs[i].transform.tag);
                  doughs[i].UpdateTarget(null);
                  doughs[i].DoughRemoved();
                  doughs[i].transform
                        .DOJump(new Vector3(1* multiple, -0.373f, doughs[i].transform.position.z + 2), 3, 1,
                              .5f);
                  multiple *= -1;
            }
            doughs.RemoveRange(result,doughs.Count-result);
      }

      private void ReTransformation()
      {
            if (doughs.Count > 0)
                  StartCoroutine(nameof(PlaceDoughInEndPoint));
            else
                  EventsManager.GameLose();
      }

      public void PutOnCounter()
      {
            StartCoroutine(nameof(PlaceDoughOnCounter));
      }

      public void PutOnTable(Transform tableStackPoint)
      {
            if(finalDoughs.Count==0) return;
            StartCoroutine(nameof(PlaceDoughOnTable),tableStackPoint);
      }

      #region Coroutines

     
      
      IEnumerator PlayDoughAnimation()
      {
            if (doughs.Count == 0) yield break;

            for (int i = doughs.Count - 1; i >= 0; i--)
            {
                  if(doughs[i])
                        doughs[i].PlayDoughAnimation();
                  yield return new WaitForSeconds(.05f);
            }
            
      }

       IEnumerator PlaceDoughInEndPoint()
      {
            float yPos = 0;
            for (int i = doughs.Count-1 ; i >= 0; i--)
            {
                  if (doughs[i].GETCurState() == DoughState.UnBakedCroissant || doughs[i].GETCurState() == DoughState.BakedCroissant )
                  {
                        finalDoughs.Add(doughs[i]);
                        doughs[i].UpdateTarget(null);
                        doughs[i].DoughRemoved();
                        doughs[i].transform.tag = "PickUp";
                        doughs[i].transform.parent = endStackPoint;
                        var i1 = i;
                        doughs[i].transform.DOLocalJump(new Vector3(0, yPos, 0), 3, 1, .15f).OnComplete(() =>
                        {
                              doughs[i1].transform.DOScale(Vector3.zero, .1f);
                        });
                        yPos += .5f;
                        doughs[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        EventsManager.CoinsPicked();
                        yield return new WaitForSeconds(0.15f);
                  }
                  else
                  {
                        var i1 = i;
                        doughs[i].transform.DOMoveX(-5, .15f).OnComplete(() =>
                        {
                              doughs[i1].gameObject.SetActive(false);
                        });
                  }
            }
            if(finalDoughs.Count==0)
                  EventsManager.GameLose();
            else
            {
                  doughs.Clear();
                  EventsManager.ChefGotDough();
                  EventsManager.CollisionNextLevel();
            }
      }

      IEnumerator PlaceDoughOnCounter()
      {
            for (int i = finalDoughs.Count-1 ; i >= 0; i--)
            {
                  finalDoughs[i].transform.parent = counterStackPoint;
                  finalDoughs[i].transform.DOLocalJump(new Vector3(0, 0, 0),3,1, .15f);
                  //GameObject coin = Instantiate(coinPrefab, moneyPoint, true);
                  //coin.transform.DOLocalJump(new Vector3(Random.Range(-7, 7), 0, Random.Range(-2, 2)), 5,1,.5f);
                  EventsManager.CoinsPicked();
                  yield return new WaitForSeconds(0.25f);
            }
            finalDoughs.Clear();
      }

     
      IEnumerator PlaceDoughOnTable(Transform stackPoint)
      {
            var xVal = -0.75f;
            var zVal = -1.5f;
            for (int i = finalDoughs.Count-1 ; i >= 0; i--)
            {
                  finalDoughs[i].transform.parent = stackPoint;
                  var i1 = i;
                  finalDoughs[i].transform.DOLocalJump(new Vector3(xVal, 1.5f, zVal), 3, 1, .25f);
                  zVal *= -1;
                  xVal *= -1;
                  var coin = Instantiate(coinPrefab, moneyPoint, true);
                  coin.transform.DOLocalJump(new Vector3(Random.Range(-7, 7), 0, Random.Range(-2, 2)), 5,1,.5f);
                  if (i % 2 == 0)
                  {
                        xVal = 0.75f;
                        zVal = -1.5f;
                  }


                  yield return new WaitForSeconds(0.25f);
            }
            finalDoughs.Clear();
            EventsManager.DoughPutToTable();
      }
      
      #endregion
     

      private int SearchDough(int doughID)
      {
            for (var i = 0; i < doughs.Count; i++)
            {
                  if (doughs[i].GetID() == doughID)
                        return i;
            }
            return -1;
      }


      private void OnDestroy()
      {
            EventsManager.ONConvertedToDough -= AddFirstDough;
            EventsManager.ONReachedEnd -= ReTransformation;
      }
}
