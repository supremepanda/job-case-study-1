using System.Collections;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level4
{
    public class Ball : MonoBehaviour
    {
        public delegate void PowerUpEvent(FoodType foodType);
        public event PowerUpEvent OnPowerUp;
        
        [SerializeField] private float speed;
        
        private Rigidbody _rigidbody;
        private Paddle _paddle;
        private Progress _progress;

        private int _speedUpCount;
        private int _paddleLengthUpCount;
        private int _wobblingBallCount;
        private int _biggerBallCount;

        private static float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth) 
        {
            return (ballPos.x - racketPos.x) / racketWidth;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Paddle"))
            {
                float x = HitFactor(transform.position, other.transform.position, other.collider.bounds.size.x);
                Vector3 direction = new Vector3(x, 0, 1).normalized;
                _rigidbody.velocity = direction * speed;
            }
            else if (other.gameObject.CompareTag("Death"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (other.gameObject.CompareTag("Brick"))
            {
                OnPowerUp?.Invoke(other.gameObject.GetComponent<Food>().FoodType);
                Destroy(other.gameObject);
                _progress.IncreaseProgress();
            }
            else if (other.gameObject.CompareTag("SideWall"))
            {
                var velocity = _rigidbody.velocity;
                velocity = new Vector3(velocity.x, 0, velocity.z - 0.1f);
                _rigidbody.velocity = velocity;
            }
        }

        #region PowerUp Functions

        public IEnumerator SpeedUp()
        {
            _speedUpCount++;
            int count = _speedUpCount;
            speed = Level4Constants.SpeedUpSpeed;
            
            yield return new WaitForSeconds(2f);

            if (_speedUpCount == count)
            {
                speed = Level4Constants.DefaultSpeed;
            }
        }

        public IEnumerator PaddleLengthUp()
        {
            _paddleLengthUpCount++;
            int count = _paddleLengthUpCount;
            _paddle.PaddleLengthUp();
            
            yield return new WaitForSeconds(2f);

            if (_paddleLengthUpCount == count)
            {
                _paddle.PaddleLengthDown();
            }
        }

        public void PaddleControlReverse()
        {
            _paddle.direction *= -1;
        }

        public IEnumerator WobblingBall()
        {
            _wobblingBallCount++;
            int count = _wobblingBallCount;
            // toDo: wobbling yapcan

            yield return new WaitForSeconds(2f);

            if (_wobblingBallCount == count)
            {
                // toDO: wobbling disable
            }
        }

        public IEnumerator BiggerBall()
        {
            _biggerBallCount++;
            int count = _biggerBallCount;
            float targetSize = Level4Constants.BiggerBall;
            transform.localScale = new Vector3(targetSize, targetSize, targetSize);

            yield return new WaitForSeconds(2f);

            if (_biggerBallCount == count)
            {
                float defaultSize = Level4Constants.DefaultBall;
                transform.localScale = new Vector3(defaultSize, defaultSize, defaultSize);
            }
        }
        
        #endregion

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _paddle = FindObjectOfType<Paddle>();
            _progress = FindObjectOfType<Progress>();
            _rigidbody.velocity = new Vector3(0, 0, -1) * speed;
        }
    }
}
