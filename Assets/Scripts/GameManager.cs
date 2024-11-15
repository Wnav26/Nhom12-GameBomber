using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;

    // Tham chiếu đến các hình ảnh chiến thắng của từng nhân vật
    public GameObject victoryImagePlayer1;
    public GameObject victoryImagePlayer2;
    public GameObject victoryImagePlayer3;
    public GameObject victoryImagePlayer4;

    public void CheckWinState()
    {
        int aliveCount = 0;
        GameObject lastAlivePlayer = null;
        List<string> alivePlayerNames = new List<string>(); // Danh sách tên người chơi còn sống

        // Ẩn tất cả hình ảnh chiến thắng trước khi kiểm tra người chơi
        victoryImagePlayer1.SetActive(false);
        victoryImagePlayer2.SetActive(false);
        victoryImagePlayer3.SetActive(false);
        victoryImagePlayer4.SetActive(false);

        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                aliveCount++;
                lastAlivePlayer = player; // Lưu lại người chơi cuối cùng còn sống
                alivePlayerNames.Add(player.name); // Thêm tên người chơi vào danh sách
            }
        }

        // Log tên tất cả người chơi còn sống
        Debug.Log("Alive Players: " + string.Join(", ", alivePlayerNames));
        Debug.Log("Alive Count: " + aliveCount); // Log số lượng người sống

        if (aliveCount == 1 && lastAlivePlayer != null)
        {
            ShowVictoryImage(lastAlivePlayer);
        }
    }

    private void ShowVictoryImage(GameObject winningPlayer)
    {
        Debug.Log("Winning Player: " + winningPlayer.name); // Log tên người chiến thắng

        // Hiển thị hình ảnh chiến thắng cho người chơi thắng
        if (winningPlayer == players[0])
        {
            victoryImagePlayer1.SetActive(true);
            Debug.Log("Victory Image 1 Active");
        }
        else if (winningPlayer == players[1])
        {
            victoryImagePlayer2.SetActive(true);
            Debug.Log("Victory Image 2 Active");
        }
        else if (winningPlayer == players[2])
        {
            victoryImagePlayer3.SetActive(true);
            Debug.Log("Victory Image 3 Active");
        }
        else if (winningPlayer == players[3])
        {
            victoryImagePlayer4.SetActive(true);
            Debug.Log("Victory Image 4 Active");
        }

        // Đợi 3 giây trước khi bắt đầu vòng chơi mới
        Invoke(nameof(NewRound), 3f);
    }

    private void NewRound()
    {
        // Ẩn tất cả hình ảnh chiến thắng trước khi chơi lại
        victoryImagePlayer1.SetActive(false);
        victoryImagePlayer2.SetActive(false);
        victoryImagePlayer3.SetActive(false);
        victoryImagePlayer4.SetActive(false);

        // Chơi lại vòng
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

