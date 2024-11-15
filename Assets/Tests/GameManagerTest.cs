using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class GameManagerTest : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private GameObject player4;
    private GameObject victoryImage1;
    private GameObject victoryImage2;
    private GameObject victoryImage3;
    private GameObject victoryImage4;

    [SetUp]
    public void SetUp()
    {
        gameManager = new GameObject("GameManager").AddComponent<GameManager>();

        // Khởi tạo người chơi
        player1 = new GameObject("Player1");
        player2 = new GameObject("Player2");
        player3 = new GameObject("Player3");
        player4 = new GameObject("Player4");

        // Gán hình ảnh chiến thắng
        victoryImage1 = new GameObject("VictoryImagePlayer1");
        victoryImage2 = new GameObject("VictoryImagePlayer2");
        victoryImage3 = new GameObject("VictoryImagePlayer3");
        victoryImage4 = new GameObject("VictoryImagePlayer4");
        gameManager.victoryImagePlayer1 = victoryImage1;
        gameManager.victoryImagePlayer2 = victoryImage2;
        gameManager.victoryImagePlayer3 = victoryImage3;
        gameManager.victoryImagePlayer4 = victoryImage4;

        // Gán trạng thái hoạt động cho người chơi
        gameManager.players = new GameObject[] { player1, player2, player3, player4 };
    }

    [UnityTest]
    public IEnumerator CheckWinState_ShouldIdentifyWinningPlayer()
    {
        player1.SetActive(true); // Chỉ player 1 còn sống
        player2.SetActive(false);
        player3.SetActive(false);
        player4.SetActive(false);

        yield return null; // Đợi frame tiếp theo

        gameManager.CheckWinState();

        // Kiểm tra xem hình ảnh chiến thắng đã được kích hoạt cho player1 hay chưa
        Assert.IsTrue(gameManager.victoryImagePlayer1.activeSelf);
        Assert.IsFalse(gameManager.victoryImagePlayer2.activeSelf);
        Assert.IsFalse(gameManager.victoryImagePlayer3.activeSelf);
        Assert.IsFalse(gameManager.victoryImagePlayer4.activeSelf);
    }

    [UnityTest]
    public IEnumerator CheckWinState_ShouldNotIdentifyWinner_WhenMultiplePlayersAlive()
    {
        player1.SetActive(true);
        player2.SetActive(true); // Cả player 1 và player 2 đều còn sống
        player3.SetActive(false);
        player4.SetActive(false);
        yield return null; // Đợi frame tiếp theo

        gameManager.CheckWinState();

        // Kiểm tra xem không có hình ảnh chiến thắng nào được kích hoạt
        Assert.IsFalse(gameManager.victoryImagePlayer1.activeSelf);
        Assert.IsFalse(gameManager.victoryImagePlayer2.activeSelf);
        Assert.IsFalse(gameManager.victoryImagePlayer3.activeSelf);
        Assert.IsFalse(gameManager.victoryImagePlayer4.activeSelf);
    }

    [UnityTest]
    public IEnumerator CheckWinState_ShouldNotIdentifyWinner_WhenNoPlayersAlive()
    {
        player1.SetActive(false);
        player2.SetActive(false); // Cả hai người chơi đều không sống
        player3.SetActive(false);
        player4.SetActive(false);
        yield return null; // Đợi frame tiếp theo

        gameManager.CheckWinState();

        // Kiểm tra xem không có hình ảnh chiến thắng nào được kích hoạt
        Assert.IsFalse(gameManager.victoryImagePlayer1.activeSelf);
        Assert.IsFalse(gameManager.victoryImagePlayer2.activeSelf);
        Assert.IsFalse(gameManager.victoryImagePlayer3.activeSelf);
        Assert.IsFalse(gameManager.victoryImagePlayer4.activeSelf);
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(gameManager.gameObject);
        Object.Destroy(player1);
        Object.Destroy(player2);
        Object.Destroy(player3);
        Object.Destroy(player4);
        Object.Destroy(victoryImage1);
        Object.Destroy(victoryImage2);
        Object.Destroy(victoryImage3);
        Object.Destroy(victoryImage4);
    }
}
