using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public Sprite[] LoadGameObjects(string spriteName)
    {
        return Resources.LoadAll<Sprite>(spriteName);
    }
}
