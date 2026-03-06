using UnityEngine;

public class playerCombat : MonoBehaviour
{
    private Animator anim;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    public static int noOfHeavyClicks = 0;
    float lastClickedTime = 0;
    float lastHeavyClickedTime = 0;
    float maxCombodelay = 1f;
    [SerializeField] private Collider weaponCollider;
    void Start()
    {
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(1).IsName("axeAttack1"))
        {
            anim.SetBool("axeAttack1", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(1).IsName("axeAttack2"))
        {
            anim.SetBool("axeAttack2", false);
            noOfClicks = 0;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("heavyUpSwing1"))
        {
            anim.SetBool("heavyUpSwing1", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("heavy360Swing1"))
        {
            anim.SetBool("heavy360Swing1", false);
            noOfHeavyClicks = 0;
        }
        if (Time.time - lastClickedTime > maxCombodelay)
        { noOfClicks = 0; }
        if (Time.time - lastHeavyClickedTime > maxCombodelay)
        { noOfHeavyClicks = 0; }
        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))
            {

                OnClick();


            }
            if (Input.GetMouseButtonDown(1))
            {

                HeavyClick();


            }

        }

        noOfClicks = Mathf.Clamp(noOfClicks, 0, 2);
        if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(1).IsName("axeAttack1"))
        {
            anim.SetBool("axeAttack1", false);
            anim.SetBool("axeAttack2", true);
        }
        noOfHeavyClicks = Mathf.Clamp(noOfHeavyClicks, 0, 2);
        if (noOfHeavyClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("heavyUpSwing1"))
        {
            anim.SetBool("heavyUpSwing1", false);
            anim.SetBool("heavy360Swing1", true);
        }
    }
    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            anim.SetBool("axeAttack1", true);
        }


    }
    void HeavyClick()
    {
        lastHeavyClickedTime = Time.time;
        noOfHeavyClicks++;

        if (noOfHeavyClicks == 1)
        {
            anim.SetBool("heavyUpSwing1", true);
        }

    }

    public void ActivateWeaponCollider()
    {
        weaponCollider.enabled = true;
    }
    public void DeActivateWeaponCollider()
    {
        weaponCollider.enabled = false;
    }
}