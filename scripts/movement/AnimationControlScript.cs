using UnityEngine;

public class AnimationControlScript : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
         animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }



    void OnMouseDown()
    {
        // Set the trigger to play the one-time animation
       animator.Play("bounce");
    }
}
