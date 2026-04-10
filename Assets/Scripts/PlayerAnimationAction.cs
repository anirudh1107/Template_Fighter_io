using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerAnimationAction : MonoBehaviour
{
    [SerializeField] private float jumpForce = 0.05f;
    [SerializeField] private float flipForce = 0.1f;
    private GameObject playerCapsule;
    private Rigidbody rb;

    private void Awake() {
        playerCapsule = this.transform.parent.gameObject;
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

    public void FlipAnimationAction()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on player capsule.");
            return;
        }
        rb.AddForce(Vector3.up * flipForce, ForceMode.Impulse);
    }
}
