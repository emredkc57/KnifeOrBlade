using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerManager : MonoBehaviour
{

    Rigidbody2D physics2D;

    float playerpositionX;

    [SerializeField]
     private int speed  = 700;

    [SerializeField]
    Sprite[] waitAnim;


    [SerializeField]
    private Image GameEndImage;

    float endGameCounter = 0;
    int endGameAlphaCounter = 0;




    float horizontalLeft, horizontalRight;



    SpriteRenderer spriteRenderer;
    float waitAnimTime = 0;

    //Invoking the Health System Manager---------
    HealthSystemManager healthSystemManager;
    int healthCounter = 0;
    //-------------------------------------------


    [SerializeField]
    private Text ScoreText;
    public int Score,highestScore;


    //My Initializations---------------------------------------------------
    bool isHeTouchedOnTheGround = false;


    MenuManager menuManager;

    AudioManager audioManager;

    GameLevelManager gameLevelManager;


    int randomCounterEventHolder;




    private void Awake()
    {
        healthSystemManager = Object.FindObjectOfType<HealthSystemManager>();
        audioManager = Object.FindObjectOfType<AudioManager>();

        menuManager = Object.FindObjectOfType<MenuManager>();

        gameLevelManager = Object.FindObjectOfType<GameLevelManager>();
    }



    // Start is called before the first frame update
    void Start()
    {
        physics2D = transform.GetComponent<Rigidbody2D>();

        playerpositionX = transform.GetComponent<Transform>().position.x;

        spriteRenderer = transform.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(isHeTouchedOnTheGround);
        CharacterControlMovement();
        CharacterAnimation();

        Debug.Log("Horizontal" + horizontalLeft);

     
        if(speed != 700)
        {

            StartCoroutine(FoodCooldown());

        }

     
    }


    void CharacterAnimation()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        int waitAnimCounter = 0;

        if(horizontal == 0)
        {
            waitAnimTime += Time.deltaTime;

            if(waitAnimTime > 0.1f)
            {
                spriteRenderer.sprite = waitAnim[waitAnimCounter++];
                
                if(waitAnimCounter == waitAnim.Length)
                {
                    waitAnimCounter = 0;

                }
                waitAnimTime = 0;

            }



        }


    }


    public void CharacterControlMovement()
    {

 
        //Player goes to Right----
        if (Input.GetKey(KeyCode.RightArrow))
        {
            physics2D.AddForce(new Vector2(playerpositionX + speed, 0) * Time.deltaTime);

            gameObject.transform.localScale = new Vector3(1, 1, 1);

        }
        //-------------------------



        Debug.Log(horizontalLeft);
        //Player goes to Left-----
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            physics2D.AddForce(new Vector2(playerpositionX - speed, 0) * Time.deltaTime);

            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        //------------------------

        //Player jumps----------
        if(Input.GetKey(KeyCode.Space))
        {
            if(isHeTouchedOnTheGround)
            {
                physics2D.AddForce(new Vector2(0, 10 * (speed / 15)));
                isHeTouchedOnTheGround = false;
            }
            else
            {

                Debug.Log(isHeTouchedOnTheGround);
            }

  

        }
        //----------------------



    }

    //It is Required to jump only for one click-------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if(collision.gameObject.CompareTag("rock"))
        {

           healthCounter++;
            HealthIndicator();

            Destroy(collision.gameObject);

            audioManager.audioSource.PlayOneShot(audioManager.playerhurt);
 
        }


        if (collision.transform.CompareTag("knife"))
        {

            audioManager.audioSource.PlayOneShot(audioManager.playerSeriousHurt);

            Destroy(collision.gameObject);
            healthCounter++;
            HealthIndicator();
            healthCounter++;
            HealthIndicator();

        }



        if (collision.gameObject.CompareTag("coin"))
        {

            Score += 10;
           
      
            audioManager.audioSource.PlayOneShot(audioManager.coin);

            ScoreText.text = Score.ToString() + " :Score";

            Destroy(collision.gameObject);

          

        }


        if (collision.gameObject.CompareTag("ground"))
        {
            isHeTouchedOnTheGround = true;

        }



        if (collision.gameObject.CompareTag("diamond"))
        {

            Score += 50;

            if (Score > highestScore)
            {
                highestScore = Score;
                PlayerPrefs.SetInt("score", highestScore);

            }

            audioManager.audioSource.PlayOneShot(audioManager.diamond);

            ScoreText.text = Score.ToString() + ":Score";



            Destroy(collision.gameObject);

        }

        if(collision.gameObject.CompareTag("meat"))
        {
            randomCounterEventHolder = gameLevelManager.counterSomeEvent;


            speed += 200;

            Destroy(collision.gameObject);

    

        }


        if (Score > highestScore)
        {
            highestScore = Score;
            PlayerPrefs.SetInt("score", highestScore);

        }

     


    }
    //------------------------------------------------------------



    void HealthIndicator()
    {


        if (healthCounter == 1)
        {
            healthSystemManager.heart1.SetActive(false);
        }
        else if (healthCounter == 2)
        {
            healthSystemManager.heart2.SetActive(false);
        }
        else if (healthCounter == 3)
        {
            healthSystemManager.heart3.SetActive(false);
        }
        else if (healthCounter == 4)
        {
            healthSystemManager.heart4.SetActive(false);
        }



        if (healthCounter == 4)
        {
            
            


            StartCoroutine(StartTheEndGameAlphaCoroutine());

        
            Debug.Log("Game Over");

        }

     
    }


    IEnumerator StartTheEndGameAlphaCoroutine()
    {


        GameEndImage.GetComponent<CanvasGroup>().alpha = 10;
        yield return new WaitForSeconds(0.5f);
    
        GameEndImage.GetComponent<CanvasGroup>().alpha = 255;
        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene(0);



    }


    IEnumerator FoodCooldown()
    {

    
        yield return new WaitForSeconds(5f);
        speed = 700;

    }






}
