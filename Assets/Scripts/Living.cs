using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : MonoBehaviour
{
    //플레이어에게 데미지를 주는 클래스

    private GameObject shield;
    private GameObject swordTr;
    private GameObject sword;   
    private CircleCollider2D swordCircle;
    private GameObject Sshield;
    private GameObject iceEffect; //얼음 저주
    
    private void Start()
    {
        shield = GameObject.Find("Shield");       
        Sshield = GameObject.Find("SShield");       
    }

    private void OnEnable()
    {
        swordTr = GameObject.Find("SwordObjectTr");
        sword = swordTr.transform.Find("Sword").gameObject;
        swordCircle = sword.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            Debug.Log("어택");
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if(iceEffect == null)
            {
                iceEffect = playerController.iceCurse;
            }
            if (playerController != null && playerController.shield.active == false && playerController.Super.active == false && playerController.Ishield.active == false && swordCircle.enabled == false && iceEffect.active == false)
            {
                SoundManager.instance.PlaySE("PlayerHit");
                StartCoroutine("ShackeCamera");
                
                if (gameObject.tag == "SnowWave" && iceEffect.active == false)
                {
                    SoundManager.instance.PlaySE("CreateIce");
                    iceEffect.SetActive(true);
                    playerController.hp -= 1;
                    playerController.onDamage();
                    playerController.damageMotion();
                    playerController.Super.SetActive(true);
                }
                else
                {

                    playerController.hp -= 1;
                    playerController.onDamage();
                    playerController.damageMotion();
                    playerController.Super.SetActive(true);
                }
            }
            
        }
    }

    IEnumerator ShackeCamera()
    {
        if (PlayerCamera.shakeTimer <= 0f)
        {
            PlayerCamera.shakeTimer = 0.1f; //흔들림 효과를 위해 선언
            PlayerCamera.shakeAmount = 0.05f;
            PlayerCamera.isBossAppear = true;
            yield return new WaitForSeconds(0.1f);

        }
    }
}
