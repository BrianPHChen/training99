using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropPrototype : MonoBehaviour
{
    public VersusPlayer owner { get; set; }
    public VersusPlayer target { get; set; }
    public VersusGameManager manager { get; set; }
}
