using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers{
    //round the vector 2 to the nearest int
    public static Vector2 RoundVector(Vector2 value){
        return new Vector2Int(
            Mathf.RoundToInt(value.x),
            Mathf.RoundToInt(value.y)
        );
    }
}
