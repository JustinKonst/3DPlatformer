using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event Action OnCollected;
    public static int total;

    [SerializeField] private AudioSource CollectionSoundEffect;
    void Awake() => total++;
    void Update()
    {
        transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectionSoundEffect.Play();
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
