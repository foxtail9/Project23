using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{

    public void Dead()
    {
        Destroy(transform.parent.gameObject);
    }
}
