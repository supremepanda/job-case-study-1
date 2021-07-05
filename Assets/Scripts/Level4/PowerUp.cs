using Common;
using UnityEngine;

namespace Level4
{
    public class PowerUp : MonoBehaviour
    {
        private Ball _ball;

        private void Start()
        {
            _ball = FindObjectOfType<Ball>();
            _ball.OnPowerUp += PowerUpBall;
        }

        /// <summary>
        /// When powerup event triggered, this function select correct powerup
        /// and call correct function from ball.
        /// </summary>
        private void PowerUpBall(FoodType foodType)
        {
            switch (foodType)
            {
                case FoodType.Cherry:
                    StartCoroutine(_ball.SpeedUp());
                    break;
                case FoodType.Banana:
                    StartCoroutine(_ball.PaddleLengthUp());
                    break;
                case FoodType.Hamburger:
                    _ball.PaddleControlReverse();
                    break;
                case FoodType.CheeseSlice:
                    StartCoroutine(_ball.WobblingBall());
                    break;
                case FoodType.Watermelon:
                    StartCoroutine(_ball.BiggerBall());
                    break;
            }
        }
    }
}
