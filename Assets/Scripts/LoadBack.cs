using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("Load", 1f);
        Load();
    }

    private void Load()
    {
        Loader.LoadBack();
    }
}
