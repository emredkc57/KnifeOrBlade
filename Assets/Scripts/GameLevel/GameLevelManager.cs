using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLevelManager : MonoBehaviour
{



    [SerializeField]
    private GameObject rock;

    [SerializeField]
    private GameObject[] rocks,knifeArray;

    [SerializeField]
    private Sprite[] explosions;

    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private GameObject[] coinsArray;

    [SerializeField]
    private GameObject diamond;

    private GameObject[] diamondsArray;

    //Food for boost SPEED !!
    [SerializeField]
    private GameObject meat;

    private GameObject[] meatsArray;

    Rigidbody2D meatRigid;



    //MORE POWERFULL ENEMY------------------------
    [SerializeField]
    private GameObject knife;
    //-------------------------------------------





    //My Initializations------
    int howmanyRocks = 5;
    public int counter,counterSomeEvent;
    float changingTime,explosionTime;

    //Rigids Are defined here ----------------
    Rigidbody2D rockRigids,coinRigids,knifeRigid,diamondRigid;
    //--------------------------------------


    SpriteRenderer spriteRenderer;


    int configureBladeChance= 13;



    int coinCounter,diamondCounter = 0;

    


    //Other Classes Defined HERE !!!!!!!-------------------------------------
    AudioManager audioManager;
    PlayerManager playerManager;
    //--------------------------------------------------------------------



    private void Awake()
    {
        playerManager = Object.FindObjectOfType<PlayerManager>();
        audioManager = Object.FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rocks = new GameObject[5];

        coinsArray = new GameObject[20];

        knifeArray = new GameObject[5];

        diamondsArray = new GameObject[5];

        meatsArray = new GameObject[10];

        spriteRenderer = rock.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        EnemyCreator();


    }




    void ExplosionAnimation()
    {
        explosionTime = Time.deltaTime;
        int counterExp = 0;


        if (explosionTime > 0.1f)
        {
            spriteRenderer.sprite = explosions[counterExp++];

            if (explosionTime == explosions.Length)
            {
                explosionTime = 0;

            }
            explosionTime = 0;



        }

    }



    void EnemyCreator()
    {


        if(playerManager.Score > 50)
        {
            configureBladeChance = 10;
        }

        changingTime += Time.deltaTime;

        Debug.Log("Counter ===  " + counter);
        if (changingTime > 1f)
        {
            diamondCounter++;

            if (diamondCounter > 5 & counterSomeEvent % 10 == 0 ) diamondCounter = 0;

            if (playerManager.Score > 10 & diamondCounter == 4)
            {
                diamondsArray[diamondCounter] = Instantiate(diamond, new Vector2(0, 15), Quaternion.identity);
                diamondRigid = diamondsArray[diamondCounter].AddComponent<Rigidbody2D>();
                diamondRigid.gravityScale = 0.3f;

                float xAxisforDiamond = Random.Range(-10, 10);
                diamondsArray[diamondCounter].transform.position = new Vector2(xAxisforDiamond, 15);

                //Declaring foods in order to rare as diamonds
                FoodGenerator();


            }


            changingTime = 0;
            counterSomeEvent++;

          
            if (counter % 15 == 0)
            {

                RewardGenerator();

            }
            else if (counterSomeEvent % configureBladeChance == 0)
            {
                knifeArray[counter] = Instantiate(knife, new Vector2(0, 17), Quaternion.EulerAngles(0, 0, 89.906f));
                knifeRigid = knifeArray[counter].AddComponent<Rigidbody2D>();
                knifeRigid.gravityScale = 0.3f;

                float knifeXaxis = Random.RandomRange(-10, 10);
                knifeArray[counter].transform.position = new Vector3(knifeXaxis, 15);

            }

            rocks[counter] = Instantiate(rock, new Vector2(0, 15), Quaternion.identity);
            rockRigids = rocks[counter].AddComponent<Rigidbody2D>();
            rockRigids.gravityScale = 0.40f;

            rocks[counter].transform.tag = "rock";



            float rockXaxis = Random.RandomRange(-10, 10);
            rocks[counter].transform.position = new Vector3(rockXaxis, 15);
           rocks[counter].GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            counter++;


        

            if (counter == rocks.Length)
            {

                counter = 0;


            }

            
        }


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.CompareTag("rock"))
        {

            ExplosionAnimation();

            Destroy(collision.gameObject, 0.5f);

            audioManager.audioSource.PlayOneShot(audioManager.rockCollisionAudio);

           

        }

        if(collision.transform.CompareTag("coin"))
        {
            audioManager.audioSource.PlayOneShot(audioManager.coinCollisionAudio);
            Destroy(collision.gameObject, 3f);
         

        }

        if(collision.transform.CompareTag("knife"))
        {
            audioManager.audioSource.PlayOneShot(audioManager.knifeCollisionAudio);
            Destroy(collision.gameObject, 0.5f);

        }

        if(collision.transform.CompareTag("diamond"))
        {

            Destroy(collision.gameObject, 0.08f);

        }

        if(collision.gameObject.CompareTag("meat"))
        {
            Destroy(collision.gameObject, 0.2f);
        }
        
    


    }



    void RewardGenerator()
    {

       

            coinsArray[counter] = Instantiate(coin, new Vector2(0, 15), Quaternion.identity);
            coinRigids = coinsArray[counter].AddComponent<Rigidbody2D>();
            coinRigids.gravityScale = 0.35f;

        float coinXaxis = Random.RandomRange(-10, 10);
        coinsArray[counter].transform.position = new Vector3(coinXaxis, 15);

        coinCounter++;
        
       

    }

    void FoodGenerator()
    {

        meatsArray[diamondCounter] = Instantiate(meat, new Vector2(0, 15), Quaternion.identity);

        meatRigid = meatsArray[diamondCounter].AddComponent<Rigidbody2D>();
        meatRigid.gravityScale = 0.34f;

        float meatXaxis = Random.RandomRange(-10, 10);
        meatsArray[diamondCounter].transform.position = new Vector2(meatXaxis, 15);





    }



}
