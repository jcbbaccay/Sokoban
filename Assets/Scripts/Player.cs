using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool Move(Vector2 direction)
    {
        Debug.Log("Player is moving");
        if (Mathf.Abs(direction.x) < 0.5)
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }
        direction.Normalize();
        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            return true;
        }
    }

    public bool Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        GameObject[] modules = GameObject.FindGameObjectsWithTag("Module");
        foreach (var module in modules)
        {
            if (module.transform.position.x == newPos.x && module.transform.position.y == newPos.y)
            {
                Module mod = module.GetComponent<Module>();
                if (mod && mod.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }
}
