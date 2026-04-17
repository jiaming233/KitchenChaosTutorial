using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position = Player.Instance.transform.position;
    }
}
