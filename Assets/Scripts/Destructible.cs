using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;

    [Range(0f, 1f)]
    public float itemSpawmChance = 0.1f;
    public GameObject[] spawnableItems;


    private void Start()
    {
        Destroy(gameObject, destructionTime);
       // Debug.Log("Doi tuong se bi pha huy sau: " + destructionTime + " giay.");
    }

    private void OnDestroy()
    {
        if (spawnableItems.Length > 0 && Random.value < itemSpawmChance)
        {
            int randomIndex = Random.Range(0, spawnableItems.Length);
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);
            //Debug.Log("Item da duoc spawn tai vi tri: " + transform.position);
        }
    }
}
  