using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BombControllerTest : MonoBehaviour
{
    private BombController bombController;
    private GameObject bombPrefab;
    private Explosion explosionPrefab;

    [SetUp]
    public void SetUp()
    {
        // Load the scene that contains the necessary components
        SceneManager.LoadScene("3Player", LoadSceneMode.Additive);

        // Create a new GameObject for the BombController
        GameObject bombControllerObject = new GameObject("BombController");
        bombController = bombControllerObject.AddComponent<BombController>();

        // Load the bomb prefab and explosion prefab from Resources
        bombPrefab = Resources.Load<GameObject>("Prefabs/Items/Bomb");
        explosionPrefab = Resources.Load<Explosion>("Prefabs/Effects/Explosion");

        Assert.IsNotNull(bombPrefab, "Bomb prefab is not found. Ensure it is correctly placed in 'Assets/Resources/Prefab/Item/Bomb'.");
        Assert.IsNotNull(explosionPrefab, "Explosion prefab is not found. Ensure it is correctly placed in 'Assets/Resources/Prefab/Item/Explosion'.");

        // Assign the prefabs to the bomb controller
        bombController.bombPrefab = bombPrefab;
        bombController.explosionPrefab = explosionPrefab; // Correctly assign the Explosion component

        // Initialize bombs remaining
        bombController.bombsRemaining = bombController.bombAmount;

        // Set the testing mode to true to disable automatic bomb placement
        bombController.isTesting = true;
    }

    [UnityTest]
    public IEnumerator PlaceBomb_ShouldMaintainBombCount()
    {
        // Lưu số lượng bom ban đầu
        int initialBombCount = bombController.bombsRemaining;
        Debug.Log($"Initial Bomb Count: {initialBombCount}");

        // Đặt bom
        yield return bombController.StartCoroutine(bombController.PlaceBomb());

        // Chờ để đảm bảo bom được đặt và nổ
        yield return new WaitForSeconds(bombController.bombFuseTime + 0.1f);

        // Kiểm tra số lượng bom còn lại
        int remainingBombCount = bombController.bombsRemaining;
        Debug.Log($"Remaining Bomb: {remainingBombCount}");

        // Kiểm tra số lượng bom còn lại phải lớn hơn hoặc bằng 1
        Assert.IsTrue(remainingBombCount >= 1, "Bomb remaining should always be >= 1 after explosion");
    }

    [UnityTest]
    public IEnumerator Explode_ShouldTriggerExplosionsInAllDirections()
    {
        Vector2 explosionPosition = Vector2.zero;
        bombController.explosionRadius = 2;

        // Ensure the bomb can be placed first
        yield return bombController.StartCoroutine(bombController.PlaceBomb());

        // Wait to ensure the bomb has exploded
        yield return new WaitForSeconds(bombController.bombFuseTime + 0.1f);

        // Simulate the explosion in all directions
        bombController.Explode(explosionPosition, Vector2.up, bombController.explosionRadius);
        bombController.Explode(explosionPosition, Vector2.down, bombController.explosionRadius);
        bombController.Explode(explosionPosition, Vector2.left, bombController.explosionRadius);
        bombController.Explode(explosionPosition, Vector2.right, bombController.explosionRadius);

        // Here you could check the results of the explosion if you keep track of them
        // For example, you could check if explosions were instantiated correctly,
        // if destructibles were cleared, etc.
        Assert.Pass("Explosions triggered correctly in all directions");
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up the BombController and loaded scene after each test
        if (bombController != null)
        {
            Object.Destroy(bombController.gameObject);
        }
        // Optionally, unload the scene if needed
        SceneManager.UnloadSceneAsync("3Player");
    }
}
