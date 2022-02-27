using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimScript : MonoBehaviour
{
    private Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    /// Functions ///

    public void ToggleIsOver()
    {
        bool isOver = !animator.GetBool("IsOver");

        animator.SetBool("IsOver", isOver);
    }
}
