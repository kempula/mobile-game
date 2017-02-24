using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;

public class freddyControl : MonoBehaviour
{


    //Freddyn animator, joka säätelee animaatioita
    Animator animator;

    //LIIKKUMINEN

    // Freddyyn liitetty rigidbody
    private Rigidbody2D freddyRigidBody;

    //Juoksu
    public float movementSpeed; //juoksunopeus

    //Hyppy
    public float jumpHeight; // Hypyn korkeus
    private int jumpCounter = 0; // Hyppylaskuri
    public GameObject jumpIndicator1; // Hyppyindikaattorit:
    public GameObject jumpIndicator2; // kuinka monta hyppyä on jäljellä

    //Tippuminen
    private float previousYPosition;
    private float currentYPosition;

    //Isku
    private float spinTimer = 0; // Iskulaskuri
    public float spinDuration; // Kuinka kauan isku kestää
    public float spinSpeed; // Iskun nopeus
    private float nextSpinTime; // Aika, jolloin voidaan suorittaa seuraava isku.
    public float spinCooldown; // Iskujen välinen aika

    //Hypyn ja iskun PUFFF
    public ParticleSystem playerTrail;

    //BOOLEANIT
    private bool isJumping = false;
    private bool isFalling = false;
    private bool isSpinning = false;
    private bool isDead = false;
    bool WriteToDatabase = true;

    //SCRIPTIT, joihin täytyy päästä käsiksi freddyControllista
    private uiManager uiManagerScript;
    private playerScore playerScoreScript;
    private ExistingDBScript dbScript;

    //ÄÄNIEFEKTIT + AudioSource
    //Audio clipit asetettu Unityssä
    public AudioClip jumpSound;
    public AudioClip spinSound;
    public AudioClip berrySound;
    public AudioClip bearSound;
    public AudioClip deathSound;
    public AudioClip fallingSound;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        //Haetaan Freddyn rigidbody
        freddyRigidBody = GetComponent<Rigidbody2D>();

        //Haetaan animator
        animator = GetComponent<Animator>();

        //Säädetään animatorin parametri touchesGround (juoksu), varmuuden vuoksi trueksi
        animator.SetBool("touchesGround", true);

        WriteToDatabase = true;

        //Haetaan Freddyyn liitetty AudioSource
        audioSource = GetComponent<AudioSource>();

        //Haetaan objekti, jolla on tagi "gameuimanager"
        GameObject gameUiManager = GameObject.FindGameObjectWithTag("gameuimanager");

        //Juuri haetun objektin sisältä haetaan komponentti uiManager, eli uiManager-scripti.
        uiManagerScript = gameUiManager.GetComponent<uiManager>();

        //Samat setit, kuin ylhäällä, mutta haetaan Score Manager ja playerScore-scripti
        GameObject scoreManager = GameObject.FindGameObjectWithTag("scoremanager");
        playerScoreScript = scoreManager.GetComponent<playerScore>();

        // --,,-- Highscore Manager, ExistingDBScript
        GameObject highscoreManager = GameObject.FindGameObjectWithTag("highscoremanager");
        dbScript = highscoreManager.GetComponent<ExistingDBScript>();


    }

    // Update is called once per frame
    void Update()
    {


        // Jos pause ei ole päällä
        if (Time.timeScale != 0f)
        {

            // Hyppyindikaattorien näyttäminen
            if (jumpCounter == 0)
            {
                jumpIndicator1.SetActive(true);
                jumpIndicator2.SetActive(true);
            }
            if (jumpCounter == 1)
            {
                jumpIndicator1.SetActive(true);
                jumpIndicator2.SetActive(false);
            }
            if (jumpCounter == 2)
            {
                jumpIndicator1.SetActive(false);
                jumpIndicator2.SetActive(false);
            }

            //KUOLEMA :(
            if (isDead == true)
            {
                //Pysäytetään Freddy kokonaan ja näytetään räjähdysanimaatio.
                freddyRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                animator.SetBool("touchesGround", false);
                animator.SetBool("jump", false);
                animator.SetBool("spin", false);
                animator.SetBool("fall", false);
                animator.SetBool("dead", true);

                //Annetaan räjähdysäänen mennä loppuun, ennenkuin avataan lopetuspaneeli
                if (!audioSource.isPlaying)
                {
                    uiManagerScript.EndGame();
                }

                // Lisätään tietokantaan tulos
                if (WriteToDatabase == true)
                {
                    dbScript.AddHighscore("Freddyy", playerScoreScript.GetScore());
                    WriteToDatabase = false;
                }
            }
            else
            {
                // ISKU
                if (isSpinning == true)
                {

                    animator.SetBool("spin", true); //Säädetään animatorin spin-parametri trueksi
                    isJumping = false;
                    animator.SetBool("jump", false);

                    //Kun laskuri on pienempi kuin hypyn kesto
                    if (spinTimer < spinDuration)
                    {
                        //Lisätään laskuriin edelliseen frameen käytetty aika sekunteina
                        spinTimer += Time.deltaTime;

                        //Liikutetaan freddyä suoraan x-akselilla
                        freddyRigidBody.velocity = new Vector2(spinSpeed, 1);

                    }
                    else
                    {
                        //Lopetetaan isku
                        isSpinning = false;
                        spinTimer = 0;
                        animator.SetBool("spin", false);
                    }

                }
                else // Jos ei isketä, liikutetaan Freddyä normaalisti (juostaan suoraan eteenpäin tai hypätään)
                {
                    
                    freddyRigidBody.velocity = new Vector2(movementSpeed, freddyRigidBody.velocity.y);

                    //INPUT
                    if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                    {

                        //Jos kosketus/hiiren klikkaus on ruudun vasemmalla puolella, suoritetaan hyppy
                        if (Input.mousePosition.x < Screen.width / 2)
                        {

                            // Hyppylaskurin täytyy olla alle kahden, jotta voidaan hypätä.
                            if (jumpCounter < 2)
                            {

                                playerTrail.Play(); // Hypyn PUFF/partikkeli efekti

                                //Hypyn liike
                                freddyRigidBody.velocity = new Vector2(freddyRigidBody.velocity.x, jumpHeight);

                                //Animaattorin parametrit
                                animator.SetBool("jump", true);
                                animator.SetBool("touchesGround", false);
                                animator.SetBool("fall", false);
                                animator.SetBool("spin", false);

                                jumpCounter++; // Lisätään laskuriin 1
                                previousYPosition = transform.position.y; //Tallennetaan Freddyn positio muuttujaan, jotta voidaan seurata koska lähdetään tulemaan alas

                                isJumping = true;

                                PlaySound(jumpSound); //Hyppyääni


                            }



                        }

                        //Jos kosketus/hiiri on ruudun vasemmalla puolella suoritetaan hyppy
                        else if (Input.mousePosition.x > Screen.width / 2)
                        {

                            //Iskussa on cooldown. Jos aika on suurempi, kuin seuraavalle iskulle sallittu aika, suoritetaan isku
                            if (Time.time >= nextSpinTime)
                            {

                                //Seuraavan iskun sallittu ajankohta. Aika + iskun cooldown.
                                nextSpinTime = Time.time + spinCooldown;
                                playerTrail.Play();
                                isSpinning = true;
                                PlaySound(spinSound);

                            }

                        }

                    }

                    // HYPPY
                    if (isJumping == true)
                    {

                        //Päivitetään Freddyn nykyinen sijainti
                        currentYPosition = transform.position.y;

                        //Jos nykyinen sijainti on pienempi kuin edellinen == Freddy tippuu.
                        if (currentYPosition < previousYPosition)
                        {

                            animator.SetBool("jump", false);
                            animator.SetBool("fall", true); //Putoamisanimaatio
                            isFalling = true;
                            isJumping = false;

                        }
                        else
                        {
                            //Päivitetään edellinen sijainti nykyiseksi
                            previousYPosition = currentYPosition;

                        }
                    }
                }

                // Jos Freddy tippuu ruudun ulkopuolelle, tulee kuolema.
                if (transform.position.y < -50)
                {
                    PlaySound(fallingSound);
                    isDead = true;
                }

            }
        }
    }

    //COLLISIONIT (platformeihin)

    void OnCollisionEnter2D(Collision2D other)
    {

        //Aina kun Freddyn collider osuu platformien box collidereihin, 
        //asetetaan animaatio juoksuksi (lähetetään animatorille boolean,
        //jonka perusteella animaattori vaihtaa animaation juoksuksi (transitionit)).

        animator.SetBool("touchesGround", true);

        // Jos Freddy on tulossa alas hypystä ja osuu platformin
        // yläpuoliseen polygon collideriin, siirrytään juoksuun + nollataan hyppylaskuri
        if (isFalling == true)
        {
            if (other.gameObject.tag == "ground")
            {
                animator.SetBool("touchesGround", true);
                animator.SetBool("fall", false);
                animator.SetBool("jump", false);
                isJumping = false;
                isFalling = false;
                jumpCounter = 0;
            }
        }

        // Jos Freddy osuu platformin alareunan edge collideriin, tulee kuolema.
        if (other.gameObject.tag == "platformBottom")
        {

            isDead = true;
            PlaySound(deathSound);


        }

    }

    //TRIGGER COLLISIONIT (karhuihin ja marjoihin)

    void OnTriggerEnter2D(Collider2D other)
    {

        //Jos Freddy osuu karhuihin, jos spinni/dash on päällä, karhu tuhoutuu
        //Jos dash ei ole päällä, tulee kuolema

        if (other.gameObject.tag == "enemy")
        {
            if (isSpinning == true)
            {
                other.gameObject.SetActive(false);
                PlaySound(bearSound);
            }
            else
            {
                isDead = true;
                PlaySound(deathSound);
            }

        }


        //Jos Freddy osuu marjoihin

        if (other.gameObject.tag == "berry")
        {
            // Kutsutaan playerScore-scriptin metodia ja lähetetään sille marjan sijainti
            playerScoreScript.AddBerryPoints(new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.y));
            other.gameObject.SetActive(false);
            PlaySound(berrySound);

        }
    }

    //ÄÄNIEFEKTIEN SOITTO. Parametrina audioklippi (alustettu public AudioClip ....)
    void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }





}
