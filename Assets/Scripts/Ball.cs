using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private float speed = 30f;
    private Rigidbody2D rb2D;
    private int rightScore, leftScore;
    [SerializeField] private Text rightScoreText, leftScoreText;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = Vector2.right * speed;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if ((rightScore >= 11 || leftScore >= 11) && Mathf.Abs(rightScore - leftScore) > 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        speed *= 1.2f;
        col.gameObject.GetComponent<SpriteRenderer>().color = randomColor();
        gameObject.GetComponent<SpriteRenderer>().color = randomColor();

        if (col.gameObject.name == "WallLeft")
        {
            speed = 30f;
            rightScore++;
            rightScoreText.text = rightScore.ToString();
            transform.position = Vector3.zero;
        } else if (col.gameObject.name == "WallRight")
        {
            speed = 30f;
            leftScore++;
            leftScoreText.text = leftScore.ToString();
            transform.position = Vector3.zero;
        } 

        if (col.gameObject.name == "RacketLeft")
        {
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length = 1 via .normalized
            Vector2 dir = new Vector2(1, y).normalized;

            // Set Velocity with dir * speed
            rb2D.velocity = dir * speed;
        }

        // Hit the right Racket?
        if (col.gameObject.name == "RacketRight")
        {
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length = 1 via .normalized
            Vector2 dir = new Vector2(-1, y).normalized;

            // Set Velocity with dir * speed
            rb2D.velocity = dir * speed;
        }
        
    }

    Color randomColor()
    {
        Color color = new Color();

        switch (Random.Range(0, 6))
        {
            case 0:
                color.r = 1f;
                color.g = 0f;
                color.b = Random.value;
                break;
            case 1:
                color.r = 1f;
                color.b = 0f;
                color.g = Random.value;
                break;
            case 2:
                color.b = 1f;
                color.g = 0f;
                color.r = Random.value;
                break;
            case 3:
                color.b = 1f;
                color.r = 0f;
                color.g = Random.value;
                break;
            case 4:
                color.g = 1f;
                color.r = 0f;
                color.b = Random.value;
                break;
            case 5:
                color.g = 1f;
                color.b = 0f;
                color.r = Random.value;
                break;
            default:
                break;
        }
        color.a = 1f;
        return color;
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket

        return (ballPos.y - racketPos.y) / racketHeight;
    }

}
