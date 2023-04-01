using TMPro;
using UnityEngine;

public class GameStatistics : MonoBehaviour
{

    
    public int score;
    public int bestPlayerPosition;
    private Rigidbody2D player;

    public TextMeshProUGUI scoreText;

    //Вывод Score на экран
    //void OnGUI()
    //{
    //    GUI.Label(new Rect(460, 100, 150, 100), "Score: " + score);
    //}

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        score = 0;
        bestPlayerPosition = (int)player.position.y;
    }
    // Score зависит от позиции игрока
    private void Update()
    {
        if (bestPlayerPosition < (int)player.position.y)
        {
            bestPlayerPosition = (int)player.position.y;
            score++;
        }
        scoreText.text = score.ToString();
    }

}
