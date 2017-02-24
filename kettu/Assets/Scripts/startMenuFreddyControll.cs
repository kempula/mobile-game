using UnityEngine;
using System.Collections;

public class startMenuFreddyControll : MonoBehaviour
{

    public Vector2 speed = new Vector2(1f, 1f); // nopeus
    public Vector2 direction = new Vector2(-1, 0); // suunta, eli oikealta vasemmalle

    private Vector2 movement;
    private bool isPausing;
    private int pauseCounter = 0;
    Animator animator;
    public GameObject freddy;
    private Rigidbody2D freddyRB;

    private Vector2 startPosition;

    //TÄTÄ SCRIPTIÄ KÄYTETÄÄN VAIN STARTMENUSSA PYÖRIVÄN FREDDYN LIIKUTTAMISEEN.
    

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        freddyRB = GetComponent<Rigidbody2D>();

        isPausing = false;

        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //Jos Freddy on paikallaan, pysäytetään liike ja vaihdetaan animaatiota.
        if (isPausing == true)
        {

            //Pysäytys
            freddyRB.velocity = new Vector2(0f, 0f);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("startmenu"))
            {

                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
                {
                    pauseCounter++; // Lisätään yksi pause, ettei niitä tule monta peräkkäin
                    isPausing = false; // Asetetaan pausetus falseksi
                    animator.SetBool("pause", false);
                }

            }

        }

        //Jos ei pauseteta, liikutaan normaalisti oikealta vasemmalle.
        else
        {

            movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
            freddyRB.velocity = movement;

            //Pauselaskuri. Käytetään koska muuten pausetuksia tulisi koko ajan, kun tullaan x-akselin miinuspuolelle.
            if (pauseCounter <= 1)
            {
                //Kun Freddy on suurinpiirtein keskellä, tehdään pausetus.
                if (transform.position.x < 1)
                {
                    isPausing = true;
                    animator.SetBool("pause", true);
                }

            }
        }

        //Kun Freddy menee ruudun ulkopuolelle, luodaan uusi Freddy oikealle ja tuhotaan vanha.
        if (transform.position.x < -100)
        {
            Instantiate(freddy, startPosition, Quaternion.identity);
            gameObject.SetActive(false);

        }

    }


}
