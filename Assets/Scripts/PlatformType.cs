using UnityEngine;

public enum PlatformBehavior { Normal, Falling, Moving }

public class PlatformType : MonoBehaviour
{
    public PlatformBehavior behavior;
}
