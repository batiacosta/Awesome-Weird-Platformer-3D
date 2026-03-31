using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject starPrefab;
    private enum CollectableType
    {
        Star,
        Heart,
        None
    }

    private CollectableType type;

    private void OnEnable()
    {
        float rand = Random.Range(0f, 100f);

        if (rand < 70f) // 0–50 -> 50% estrella
        {
            type = CollectableType.Star;
            starPrefab.SetActive(true);
            heartPrefab.SetActive(false);
        }
        else if (rand < 80f ) // 50–70 -> 10% corazón
        {
            type = CollectableType.Heart;
            starPrefab.SetActive(false);
            heartPrefab.SetActive(true);
        }
        else // 70–100 -> 30% nada
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            switch (type)
            {
                case CollectableType.Star:
                    GameManager.Instance.AddScore();
                    break;
                case CollectableType.Heart:
                    GameManager.Instance.AddHearts();
                    break;
                case CollectableType.None:
                    break;
            }
        }
        Destroy(gameObject);
    }
}