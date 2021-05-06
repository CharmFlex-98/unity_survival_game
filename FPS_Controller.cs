using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Cinemachine;



public class FPS_Controller : MonoBehaviour
{
    public static FPS_Controller instance;
    public float WalkSpeed;
    public Animator animator;
    public Camera camera;
    public Transform RightHandIndex3;
    Vector3 StringOrigin;
    GameObject String;
    bool HoldString;
    bool isShooting;
    public float FocusSpeed;
    public GameObject arrow;
    public float ShootRange;
    bool shot = false;
    public float RotateVerticalSpeed;
    bool freezerotate = true;
    float x;
    public float BowRotateSpeed, CharRotateSpeed, VerticalSpeed;
    public GameObject[] Weapon;
    GameObject ActiveWeapon;
    public Transform OriginalPos;
    public FixedJoystick joystick;
    [HideInInspector] public bool AttackPress = false, ShootPress = false;

    public Transform RayCastPoint;
    public float Raylength;
    Rigidbody rb;
    public LayerMask Ground;
    public float MaxGroundAngle;
    float groundAngle;
    Vector3 forward;
    RaycastHit Hit;
    public Transform FocusPoint;
    bool ReadyToShoot = false;
    public LayerMask layer;
    [HideInInspector]public bool Playerdied = false;
    
   
    
    

    






    private void Start()
    {
        GameManager.instance.ItemUpdated = false;
        rb = GetComponent<Rigidbody>();
        GameManager.instance.ArrowPooling();
        AudioManager.instance.BackgroundMusic.Stop();


    }


   
    void Update()
    {
        for (int i = 0; i < Weapon.Length; i++)
        {
            if (GameManager.instance.ItemUpdated == false)
            {
                if (Weapon[i].GetComponent<MyWeaponInfo>().WeaponInfo.OnEquip == true)
                {
                    Weapon[i].SetActive(true);
                    ActiveWeapon = Weapon[i];
                    String = Weapon[i].GetComponent<WeaponComponent>().StringRoot;
                    StringOrigin = Weapon[i].GetComponent<WeaponComponent>().StringRoot.transform.localPosition;




                }
                else
                {
                    Weapon[i].SetActive(false);

                }
                if (i == (Weapon.Length - 1))
                {
                    GameManager.instance.ItemUpdated = true;
                }
            }
        }

        CalculateGroundAngle();
        ControllerPad();

        if (Input.GetKey(KeyCode.E))
        {
            AttackPress = true;
        }
       
        if (AttackPress==true)
        {
            AimOn();
        }
        if (AttackPress==false)
        {

            AimOff();

        }

        if (ReadyToShoot==true)
        {
            if (ShootPress==true)
            {
                shot = true;
                animator.speed = 1;
                ReadyToShoot = false;
                ShootPress = false;

            }
        }
       
        if (HoldString == true)
        {
            String.transform.position = RightHandIndex3.position;
        }
        else
        {
            String.transform.localPosition = StringOrigin;
        }


    }

    void Walk()
    {
        Vector3 CurrentRotation = transform.eulerAngles;
        CurrentRotation.y = camera.transform.eulerAngles.y;
        Quaternion currentRotation = Quaternion.Euler(CurrentRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, CharRotateSpeed * Time.deltaTime);

        transform.Translate(0, 0, WalkSpeed * Time.deltaTime);
        animator.SetBool("isWalking", true);
    }

    void Walkbackward()
    {
        Vector3 CurrentRotation = transform.eulerAngles;
        CurrentRotation.y = camera.transform.eulerAngles.y + 180;
        Quaternion currentRotation = Quaternion.Euler(CurrentRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, CharRotateSpeed * Time.deltaTime);
        transform.Translate(0, 0, WalkSpeed * Time.deltaTime);
        animator.SetBool("isWalking", true);
    }

    void WalkLeft()
    {
        Vector3 CurrentRotation = transform.eulerAngles;
        CurrentRotation.y = camera.transform.eulerAngles.y - 90;
        Quaternion currentRotation = Quaternion.Euler(CurrentRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, CharRotateSpeed * Time.deltaTime);
        transform.Translate(0, 0, WalkSpeed * Time.deltaTime);
        animator.SetBool("isWalking", true);
    }
    void WalkRight()
    {
        Vector3 CurrentRotation = transform.eulerAngles;
        CurrentRotation.y = camera.transform.eulerAngles.y + 90;
        Quaternion currentRotation = Quaternion.Euler(CurrentRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, CharRotateSpeed * Time.deltaTime);
        transform.Translate(0, 0, WalkSpeed * Time.deltaTime);
        animator.SetBool("isWalking", true);
    }

    void MovementController()
    {
        
        
        Vector3 CurrentRotation = transform.eulerAngles;
        if (joystick.Horizontal > 0 && joystick.Vertical > 0)
        { 
            CurrentRotation.y = camera.transform.eulerAngles.y + (90 - Mathf.Rad2Deg * (Mathf.Atan(Mathf.Abs(joystick.Vertical) /Mathf.Abs(joystick.Horizontal)))); 
        }
        else if (joystick.Horizontal < 0 && joystick.Vertical < 0)
        {
            CurrentRotation.y = camera.transform.eulerAngles.y - (90 + Mathf.Rad2Deg * (Mathf.Atan(Mathf.Abs(joystick.Vertical) / Mathf.Abs(joystick.Horizontal))));
        }
        else if (joystick.Horizontal < 0 && joystick.Vertical > 0)
        {
            CurrentRotation.y = camera.transform.eulerAngles.y - (90 - Mathf.Rad2Deg * (Mathf.Atan(Mathf.Abs(joystick.Vertical) / Mathf.Abs(joystick.Horizontal))));
        }
        else
        {
            CurrentRotation.y = camera.transform.eulerAngles.y + (90 + Mathf.Rad2Deg * (Mathf.Atan(Mathf.Abs(joystick.Vertical) / Mathf.Abs(joystick.Horizontal))));
        }

        Quaternion currentRotation = Quaternion.Euler(CurrentRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, CharRotateSpeed * Time.deltaTime);
        if (groundAngle <= MaxGroundAngle)
        {
            return;
        }
        else
        {
            
            transform.position += forward * WalkSpeed * Time.deltaTime; ;
            animator.SetBool("isWalking", true);
        }
        


    }


    void HoldStringActivate()
    {
        HoldString = true;
    }

    void StringBack()
    {
        if (shot == true)
        {
            HoldString = false;
            AudioManager.instance.ArrowSound.Play();
            GameObject ArrowPrefab = GameManager.instance.DrawArrowFromPool();
            ArrowPrefab.transform.position = arrow.transform.position;
            ArrowPrefab.transform.rotation = arrow.transform.rotation;    
            ArrowPrefab.GetComponent<WeaponDamage>().Damage = ActiveWeapon.GetComponent<MyWeaponInfo>().WeaponInfo.Damage;
            arrow.SetActive(false);
            DetectHit(ArrowPrefab);
            shot = false;
        }
        else
        {
            return;
        }
        freezerotate = true;


    }

    

    void Aim()
    {
        animator.speed = 0;
        ReadyToShoot = true;
        
    }

    void TakeArrow()
    {
        arrow.SetActive(true);
    }
    void CameraAllowRotate()
    {
        freezerotate = false;
    }
    void DetectHit(GameObject _arrow)
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, ShootRange,layer))
        {
            _arrow.SetActive(true);
            _arrow.GetComponent<WeaponDamage>().Shot(hit.transform, hit.point);
         
        }
        
        

    }

    public void Back2Origin()
    {
        transform.position = OriginalPos.position;
        transform.rotation = OriginalPos.rotation;
    }

    void ControllerPad()
    {
        if (AttackPress == true)
        {
            return;
        }
        if (Mathf.Sqrt(Mathf.Pow(joystick.Horizontal, 2f) + Mathf.Pow(joystick.Vertical, 2f)) >= 0.15)
        {
            MovementController();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            Walk();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Walkbackward();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            WalkLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            WalkRight();
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isBack", false);

        }
    }

    void AimDirection()
    {
        if (x > 180)
        {
            animator.SetFloat("Up", Mathf.Lerp(animator.GetFloat("Up"), (360 - x) / 30, VerticalSpeed * Time.deltaTime));
            animator.SetFloat("Up", Mathf.Clamp(animator.GetFloat("Up"), -1, 1));
            animator.SetLayerWeight(1, Mathf.Abs(animator.GetFloat("Up")));
        }
        if (x < 180)
        {
            animator.SetFloat("Up", Mathf.Lerp(animator.GetFloat("Up"), (-x) / 30, VerticalSpeed * Time.deltaTime));
            animator.SetFloat("Up", Mathf.Clamp(animator.GetFloat("Up"), -1, 1));
            animator.SetLayerWeight(1, Mathf.Abs(animator.GetFloat("Up")));
        }
        if (x == 0 || x == 360)

        {
            animator.SetFloat("Up", Mathf.Lerp(animator.GetFloat("Up"), 0, VerticalSpeed * Time.deltaTime));
            animator.SetLayerWeight(1, Mathf.Abs(animator.GetFloat("Up")));
        }
    }

    void AimOn()
    {
        
        x = camera.transform.eulerAngles.x;
        Vector3 CurrentRotation = transform.eulerAngles;
        CurrentRotation.y = camera.transform.eulerAngles.y;
        Quaternion currentRotation = Quaternion.Euler(CurrentRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, BowRotateSpeed * Time.deltaTime);
        animator.SetBool("PullString", true);
        if (freezerotate == false)
        {
            AimDirection();
        }
    }

    void AimOff()
    {
        if (AudioManager.instance.BowPullingSound.isPlaying)
        {
            AudioManager.instance.BowPullingSound.Stop();
        }
        arrow.SetActive(false);
        animator.SetBool("PullString", false);
        animator.speed = 1;
        ReadyToShoot = false;
        HoldString = false;
        animator.SetLayerWeight(1, 0);
        animator.SetFloat("Up", 0);
        freezerotate = true;
    }
    
   
    void CalculateGroundAngle()
    {
        if (Physics.Raycast(RayCastPoint.position, -Vector3.up, out Hit, Raylength, Ground))
        {
           
            forward = Vector3.Cross(Hit.normal, -RayCastPoint.right);
            groundAngle = Vector3.Angle(RayCastPoint.up, forward);
            
        }
        else
        {
           
            return;
        }
    }

 
   




    public void PlayerDie()
    {
        if (Playerdied == false)
        {
            Playerdied = true;
            animator.SetBool("Die", true);
            GameObject[] AICamera = GameObject.FindGameObjectsWithTag("AICamera");
            AICamera[0].transform.GetChild(0).gameObject.SetActive(false);
            AudioManager.instance.DyingSound.Play();
        }
    }

    public void BowPullingSound()
    {
        AudioManager.instance.BowPullingSound.Play();
    }
    
    public void FootStep()
    {
        AudioManager.instance.FootStep.Play();
    }

}
  

  








