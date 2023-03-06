using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterMagnet : MonoBehaviour
{
    [SerializeField] private Character character;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag(CachedString.BRICK))
        {
            Brick brickComponent = other.gameObject.GetComponent<Brick>();
            if (!brickComponent.isGround)
            {
                return;
            }
            else if (brickComponent.materialType == character.materialType || brickComponent.materialType == MaterialType.Grey)
            {
                brickComponent.isGround = false;
                character.EatGroundBrick(brickComponent);
            }
        }
    }
}
