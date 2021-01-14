using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoFactory : Singleton<UfoFactory>
{
    public Ufo CreateUfo(UfoSettings settings)
    {
        var ufo = Instantiate(settings.Prefab);
        ufo.ApplySettings(settings);

        return ufo;
    }
}
