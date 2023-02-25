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
            GameObject brick = other.gameObject;
            Brick brickComponent = brick.GetComponent<Brick>();
            if(brick.transform.position.y - brickComponent.firstPosition.y > 1f) // chi an nhung fallen bricks duoi dat
            {
                return;
            }
            if(brickComponent.isGround)
            if (brickComponent.materialType == character.materialType || brickComponent.materialType == MaterialType.Grey)
            {
                brickComponent.isGround = false;
                character.EatGroundBrick(brick);
            }
        }
    }
}
