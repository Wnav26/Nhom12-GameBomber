
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite idleSprite;
    public Sprite[] animationSprites;


    public float animationTime = 0.25f;
    private int animationFrame;

    public bool loop = true;
    public bool idle = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void Start()
    {
        //Debug.Log("Bat dau hoat anh voi thoi gian khung hinh: " + animationTime + " giay.");
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }

    private void NextFrame()
    {
        animationFrame++;

        if (loop && animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
            //Debug.Log("Da lap lai hoat anh.");
        }

        if (idle)
        {
            spriteRenderer.sprite = idleSprite;
            //Debug.Log("Dang hien thi sprite dung yen.");
        }
        else if (animationFrame >= 0 && animationFrame < animationSprites.Length)
        {
            spriteRenderer.sprite = animationSprites[animationFrame];
            //Debug.Log("Dang hien thi sprite hoat anh o khung hinh: " + animationFrame);
        }
    }


}
