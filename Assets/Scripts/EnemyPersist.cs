using UnityEngine;
using System.Collections.Generic;

public class EnemyPersist : MonoBehaviour
{
    public static EnemyPersist currentOBJ;

    void Awake()
    {
        if (currentOBJ == null)
        {
            currentOBJ = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }



}
