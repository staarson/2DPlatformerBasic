using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpStrength;

    public Rigidbody2D p_rb;
    public LayerMask ground;

    public bool isFacingLeft;
    public Transform playerVisuals;

    public Transform left;

    public bool isGrounded;
    public Transform groundcheck;

    public GameObject bulletPrefab;
    public GameObject newBullet;

    #region Note
    //spieler schiesst in blickrichtung
    //ist die horizontale achse positiv? -> wir schauen nach rechts
    //bool facingRight
    // horizontale achse > 0 -> facingRight = true
    // horizontale achse < 0 -> facingRight = false

    //eulerAngles berechnet den wert in grad

    // isTrigger im collider wird nicht als körper registriert wenns true ist -> für Schüsse, damit sie nichts abstoßen aber collision registrieren(?)
    #endregion

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ghost")
        {
            Destroy(gameObject);
        }



    }

    void FixedUpdate()
    {

        #region Move
        float transformationX = Input.GetAxis("Horizontal") * speed;

        p_rb.AddForce(new Vector2(transformationX, 0), ForceMode2D.Force);
        #endregion

        #region Flip
        if (transformationX > 0)
        {
            isFacingLeft = false;
            playerVisuals.eulerAngles = new Vector3(0, 0, 0);
        }

        if (transformationX < 0)
        {
            isFacingLeft = true;
            playerVisuals.eulerAngles = new Vector3(0, 180, 0);
        }


        #endregion
    }


        void Update()
        {
            #region Jump

            isGrounded = Physics2D.OverlapCircle(groundcheck.position, 0.1f, ground);

            if (isGrounded)
            {

                if (Input.GetButtonDown("Jump"))
                {
                    p_rb.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
                }
            }

            #endregion

            #region Shoot

            if (Input.GetButtonDown("Fire1"))
            {

                if (isFacingLeft == false)
                {
                   newBullet = Instantiate(bulletPrefab, new Vector3(playerVisuals.position.x + 0.2f, playerVisuals.position.y + 1.5f, 1), playerVisuals.rotation);
                }

                if (isFacingLeft == true)
                {
                   
                    newBullet = Instantiate(bulletPrefab, new Vector3(left.position.x - 1, left.position.y, 1), playerVisuals.rotation);
                    newBullet.GetComponent<Shoot>().bulletSpeed *= -1;
                }
            }
        
             #endregion
        }
        }




        





