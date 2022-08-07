using UnityEngine;

public class GateManager : MonoBehaviour
{
      [SerializeField] private GateType gateType;

      private PlayerFillings _playerFillings;
      private void Start()
      {
            _playerFillings=PlayerFillings.Instance; 
      }

      public void TakeAction()
      {
            switch (gateType)
            {
                  case GateType.Flour:
                        _playerFillings.AddFlour();
                        break;
                  case GateType.Sugar:
                        _playerFillings.AddSugar();
                        break;
                  case GateType.Salt:
                        _playerFillings.AddSalt();
                        break;
                  case GateType.Water:
                        _playerFillings.FillWithWater();
                        break;
                  case GateType.Yeast:
                        _playerFillings.AddYeast();
                        break;
                  default:
                       print("Invalid Gate Type");
                        break;
            }
      }

}
