using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private float walkStepInterval = 0.5f;
    [SerializeField] private float sprintStepInterval = 0.35f;
    [SerializeField] private float volume = 0.5f;
    
    [Header("Movement Detection")]
    [SerializeField] private float movementThreshold = 0.1f;
    
    private PlayerControl2 playerControl;
    private CharacterController characterController;
    private float stepTimer;
    
    void Start()
    {
        // Get references to existing components
        playerControl = GetComponent<PlayerControl2>();
        characterController = GetComponent<CharacterController>();
        
        // Setup or find AudioSource
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        // Configure AudioSource for 3D sound
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f; // Fully 3D
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.maxDistance = 10f;
    }
    
    void Update()
    {
        // Check if player is grounded
        bool isGrounded = GetPlayerGrounded();
        
        // Only play footsteps when grounded
        if (isGrounded)
        {
            // Check if player is moving
            float horizontalSpeed = new Vector3(
                characterController.velocity.x, 
                0, 
                characterController.velocity.z
            ).magnitude;
            
            bool isMoving = horizontalSpeed > movementThreshold;
            
            if (isMoving)
            {
                // Determine if sprinting
                bool isSprinting = Input.GetKey(KeyCode.LeftShift);
                float currentStepInterval = isSprinting ? sprintStepInterval : walkStepInterval;
                
                stepTimer -= Time.deltaTime;
                
                if (stepTimer <= 0)
                {
                    PlayFootstep();
                    stepTimer = currentStepInterval;
                }
            }
            else
            {
                // Reset timer when not moving
                stepTimer = 0;
            }
        }
        else
        {
            // Reset timer when in air
            stepTimer = 0;
        }
    }
    
    bool GetPlayerGrounded()
    {
        // Access the isGrounded variable from PlayerControl2 using reflection
        var groundedField = typeof(PlayerControl2).GetField("isGrounded", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (groundedField != null)
        {
            return (bool)groundedField.GetValue(playerControl);
        }
        
        // Fallback ground check
        return Physics.CheckSphere(transform.position + Vector3.down * 0.5f, 0.3f);
    }
    
    void PlayFootstep()
    {
        if (footstepSounds != null && footstepSounds.Length > 0 && audioSource != null)
        {
            // Randomize pitch slightly for variety
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            
            // Play random footstep sound
            AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(clip, volume);
        }
    }
}