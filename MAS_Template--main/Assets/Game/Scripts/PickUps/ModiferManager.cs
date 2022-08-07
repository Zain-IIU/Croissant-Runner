using System;
using UnityEngine;
using DG.Tweening;

    public class ModiferManager : MonoBehaviour
    {
        [SerializeField] private Modifier modifyType;
        [SerializeField] private float startPos;
        [SerializeField] private float endPos;
        [SerializeField] private float delay;

        
        public void TakeAction(Dough dough)
        {
            switch (modifyType)
            {
                case Modifier.Cutter:
                   // if(dough.GETCurState()==DoughState.RolledDough)
                        dough.UpdateState(DoughState.DoughTriangle);
                    TweenHolder();
                    break;
                case Modifier.Flatter:
                   // if(dough.GETCurState()==DoughState.SimpleDough)
                        dough.UpdateState(DoughState.RolledDough);
                    TweenHolder();
                    break;
                case Modifier.CroissantMaker:
                    if(dough.GETCurState()==DoughState.DoughTriangle)
                        dough.UpdateState(DoughState.UnBakedCroissant);
                    break;
                case Modifier.Bake:
                    if(dough.GETCurState()==DoughState.UnBakedCroissant)
                        dough.UpdateState(DoughState.BakedCroissant);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TweenHolder()
        {
            transform.DOLocalMoveY(endPos, delay).OnComplete(() =>
                {
                transform.DOLocalMoveY(startPos, delay / 2);
                });
        }
    }
