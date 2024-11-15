using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class DestructibleTest : MonoBehaviour
{
    private GameObject destructibleObject;
    private Destructible destructible;
    private GameObject itemPrefab;

    [SetUp]
    public void SetUp()
    {
        // Tạo prefab cho item
        itemPrefab = new GameObject("ItemPrefab");

        // Tạo prefab destructible
        destructibleObject = new GameObject("Destructible");
        destructible = destructibleObject.AddComponent<Destructible>();

        // Thiết lập tham số cho destructible
        destructible.destructionTime = 0.1f; // Rút ngắn thời gian để kiểm tra nhanh hơn
        destructible.itemSpawmChance = 1f; // Đảm bảo rằng item sẽ spawn
        destructible.spawnableItems = new GameObject[] { itemPrefab };
    }

    [UnityTest]
    public IEnumerator OnDestroy_ShouldSpawnItem_WhenDestructionOccurs()
    {
        // Tạo một item trước để so sánh
        var initialItems = GameObject.FindObjectsOfType<GameObject>().Length-1;

        // Chờ đến khi đối tượng bị hủy
        yield return new WaitForSeconds(destructible.destructionTime);

        // Kiểm tra xem item có được spawn hay không
        var finalItems = GameObject.FindObjectsOfType<GameObject>().Length;

        Debug.Log($"Initial Item Count: {initialItems}, Final Item Count: {finalItems}");

        Assert.AreEqual(initialItems +1, finalItems, "Item should have spawned after destruction.");
    }


    [UnityTest]
    public IEnumerator OnDestroy_ShouldNotSpawnItem_WhenSpawnChanceNotMet()
    {
        destructible.itemSpawmChance = 0f; // Đặt xác suất spawn là 0

        // Ghi lại số lượng item trước khi hủy
        int initialItemCount = GameObject.FindObjectsOfType<GameObject>().Length-1;

        // Chờ đến khi đối tượng bị hủy
        yield return new WaitForSeconds(destructible.destructionTime);

        // Kiểm tra xem item có được spawn hay không
        int finalItemCount = GameObject.FindObjectsOfType<GameObject>().Length;
        Assert.AreEqual(initialItemCount, finalItemCount, "Item should not have spawned after destruction.");
    }
}
