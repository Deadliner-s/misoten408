using UnityEngine;

public class LaneAni : MonoBehaviour
{
    // �R���|�[�l���g�֘A
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void LaneChangeAni(int num)
    {
        switch (num)
        {
            case 0:
                animator.SetTrigger("left");
                break;
            case 1:
                animator.SetTrigger("right");
                break;
            case 2:
                animator.SetTrigger("jump");
                break;
        }
    }
}
