using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonDetector : MonoBehaviour
{
    bool isCannonIn = false;
    Types Type = Types.up;
    
    public enum Types
    {
        up,
        down,
        left,
        right
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Cannon")
        {
            isCannonIn = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Cannon")
        {
            isCannonIn = false;
        }
    }

    public void Update()
    {
        switch (Type)
        {
            case Types.up:
                MainCharacter.canMoveUp = !isCannonIn;
                break;
            case Types.down:
                MainCharacter.canMoveDown = !isCannonIn;
                break;
            case Types.left:
                MainCharacter.canMoveLeft = !isCannonIn;
                break;
            case Types.right:
                MainCharacter.canMoveRight = !isCannonIn;
                break;
        }
    }

    public void SetType(Types type)
    {
        Type = type;
    }
}
