using UnityEngine;

namespace Level1
{
    public class OliveCollect : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("olive"))
            {
                other.GetComponent<Olive>().Collected();
            }
        }
    }
}