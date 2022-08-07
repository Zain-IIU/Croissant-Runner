using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
        public static event Action ONGameStart;
        public static event Action ONGameLose;
        public static event Action ONGameWin;
        public static event Action ONReachedEnd;
        public static event Action ONChefGotDough;
        public static event Action ONDoughPutToCounter;
        public static event Action ONDoughPutToTable;




        public static event Action ONCollidedWithObstacle;
        public static event Action ONConvertedToDough;
        public static event Action ONCoinsPicked;
        public static event Action ONCollisionNextLevel;

        public static void GameStart()
        {
                ONGameStart?.Invoke();
        }

        public static void GameLose()
        {
                ONGameLose?.Invoke();
        }

        public static void ConvertedToDough()
        {
                ONConvertedToDough?.Invoke();
        }

        public static void CollidedWithObstacle()
        {
                ONCollidedWithObstacle?.Invoke();
        }

        public static void ReachedEnd()
        {
                ONReachedEnd?.Invoke();
        }

        public static void GameWin()
        {
                ONGameWin?.Invoke();
        }

        public static void CoinsPicked()
        {
                ONCoinsPicked?.Invoke();
        }

        public static void CollisionNextLevel()
        {
                ONCollisionNextLevel?.Invoke();
        }

        public static void ChefGotDough()
        {
                ONChefGotDough?.Invoke();
        }

        public static void DoughPutToCounter()
        {
                ONDoughPutToCounter?.Invoke();
        }

        public static void DoughPutToTable()
        {
                ONDoughPutToTable?.Invoke();
        }
}
