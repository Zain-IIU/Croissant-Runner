using UnityEngine;


public class DoughTopping : MonoBehaviour
{
        [Header("Topping Section")] 
        [SerializeField] private Transform chocoTopping;
        [SerializeField] private Transform creamTopping;
        [SerializeField] private Transform icingTopping;

        public void EnableChocolate()
        { 
           chocoTopping.gameObject.SetActive(true);
        }
        
        public void EnableCream()
        { 
            creamTopping.gameObject.SetActive(true);
            chocoTopping.gameObject.SetActive(false);
        }

        public void EnableIcing()
        {
            creamTopping.gameObject.SetActive(false);
            icingTopping.gameObject.SetActive(true);
        }
        
        
        

}
