using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    private Animator anim;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();      
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool(IS_WALKING, player.IsWalking);
    }
}
