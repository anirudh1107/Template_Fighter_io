using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerAnimationAction : MonoBehaviour
{
    [SerializeField] private float jumpForce = 0.05f;
    [SerializeField] private float flipForce = 0.1f;
    [SerializeField] private float HeavyPunchMoveTime = 0.2f;
    [SerializeField] private float HeavyPunchForce = 0.5f;

    private bool isHeavyPunching = false;
    private GameObject playerCapsule;
    private MainPlayerMovement mainPlayerMovement;
    private Rigidbody rb;

    private void Awake() {
        playerCapsule = this.transform.parent.gameObject;
        mainPlayerMovement = playerCapsule.GetComponent<MainPlayerMovement>();
        Debug.Log("Player capsule found: " + playerCapsule.name);
        rb = playerCapsule.GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeavyPunching)
        {
            if (mainPlayerMovement.isFacingRight())
            {
                playerCapsule.transform.Translate(Vector3.right * HeavyPunchForce * Time.deltaTime);
            }
            else
            {
                playerCapsule.transform.Translate(Vector3.left * HeavyPunchForce * Time.deltaTime);
            }
        }
    }

    public void JumpAnimationAction()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on player capsule.");
            return;
        }
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void HeavyPunchAnimationAction()
    {
        if (!isHeavyPunching)
        {
            StartCoroutine(HeavyPunchMove());
        }
    }

    public void FlipAnimationAction()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on player capsule.");
            return;
        }
        rb.AddForce(Vector3.up * flipForce, ForceMode.Impulse);
    }

    public void PunchSoundAction()
    {
        AudioManager.Instance.PlayRandomPunchSound();
    }

    public void KickSoundAction()
    {
        AudioManager.Instance.PlayRandomKickSound();
    }

    IEnumerator HeavyPunchMove()
    {
        isHeavyPunching = true;
        yield return new WaitForSeconds(HeavyPunchMoveTime);
        isHeavyPunching = false;
    }
}
