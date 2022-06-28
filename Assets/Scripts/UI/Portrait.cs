using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portrait : MonoBehaviour
{
    public void SetPortrait(Sprite portrait)
    {
        GetComponent<Image>().sprite = portrait;
    }
}