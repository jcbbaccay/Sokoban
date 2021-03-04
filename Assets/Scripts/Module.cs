using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Module : MonoBehaviour
{
    public bool onGoal;
    public bool Move(Vector2 direction)
    {
        if (BoxBlocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            TransformForGoal();
            return true;
        }
    }
    public bool BoxBlocked(Vector3 position, Vector2 direction)
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
                    return true ;
                }
            }
        }
        return false;
    }
    private void TransformForGoal()
    {
        GameObject[] Goals = GameObject.FindGameObjectsWithTag("Goal");
        foreach (var goal in Goals)
        {
            if (transform.position.x == goal.transform.position.x && transform.position.y == goal.transform.position.y)
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
                onGoal = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        onGoal = false;
    }


}
