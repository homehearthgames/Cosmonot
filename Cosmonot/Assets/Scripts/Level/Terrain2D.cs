using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class Terrain2D : MonoBehaviour{

    // seed of the world
    public int seed;
    // size of the map squared
    public int size;
    // biomes of the game
    public List<Biome> biomes = new List<Biome>();
    // grab the tilemap component from our child game object
    Tilemap terrain;

    void Start() {
        terrain = GetComponentInChildren<Tilemap>();
        if(size <= 0) size = 10;
        GetComponent<Terrain2D>().Generate(seed, size);
    }

    // Start the Generation presses with provided seed and size of the world
    public void Generate(int seed, int size){
        // offset the tile map so the player starts in the center
        terrain.transform.localPosition = -(Vector2.one * (size / 2)) - Vector2.one * 0.5f;
        //combine the biome color maps and begin building the world 
        StartCoroutine(BuildTerrain(CombinedColorMaps(biomes)));
    }

    //build the terrain with the supplied color map
    IEnumerator BuildTerrain(Color[] combined_biomes){
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                //grab the current biome based on the color in the color map
                var current_biome = GetType (combined_biomes[y * size + x], biomes);

                // grab the vegetation associated (if any) with this biomes cell color
                var current_vegetation = GetType (current_biome.vegetation_map[y * size + x], current_biome.vegetation);

                if(current_biome != null)
                //set the tilemap tile to the current biome tile
                terrain.SetTile (
                    new Vector3Int (
                        x,
                        y,
                        0
                    ),
                    current_biome.tile
                );
                
                // if the biome cell contains vegetation
                if(current_vegetation!=null){
                    //spawn the prefab/game object
                    GameObject vegetation_obj = Instantiate(current_vegetation.prefab);
                    //set the newly spawn objects name 
                    vegetation_obj.name = $"{current_vegetation.prefab.name}";
                    //set the newly spawn objects parent the the tilemap 
                    vegetation_obj.transform.parent = terrain.transform;
                    //set the newly spawn objects position offset
                    vegetation_obj.transform.localPosition = new Vector2(x, y) + Vector2.one * 0.5f;
                }
            }

            yield return null;
        }
    }

    // Create a combined color array from a terrainType( Biome, Vegetation )
    Color[] CombinedColorMaps<T>(List<T> types) where T : TerrainType{
        //create a new blank color array of black
        var combined_types = new Color[size * size];
        // loop through all the types
        for (int i = 0; i < types.Count; i++) {
            // create a new array of colors form perlin noise using our types color as the base
            Color[] new_biome = Perlin(types[i], size, seed);

            // loop through the blank array
            for (int x = 0; x < combined_types.Length; x++) {
                // if the new color array of colors at x isn't black
                if (new_biome[x] != Color.black) {
                    //replace the black with the new color
                    combined_types[x] = new_biome[x];
                }
            }
        }

        // return the now combined color array;
        return combined_types;
    }

    // Get ( Biome, Vegetation ) from base TerrainType.
    T GetType<T> (Color color, List<T> terrain_type) where T : TerrainType {
        //loop through those generic types
        foreach (var type in terrain_type) {
            // if the color of that type matches the color we're looking for return that type
            if (type.color == color) {
                return type;
            }
        }
        //if that list of types doesn't have our desired color return a null type
        return null;
    }

    Color[] Perlin(TerrainType terrain_type, int size, int seed){

        if(terrain_type as Biome != null){
            Biome biome = terrain_type as Biome;
            biome.vegetation_map = CombinedColorMaps(biome.vegetation);
        }

        Color[] pixels = new Color[size * size];

        for(int y = 0; y < size; y++){
            for(int x = 0; x < size; x++){
                
                System.Random rng = new  System.Random(seed);
                float offset = rng.Next(-100000, 100000);

                float sample_x = (x - (size / 2) + (offset)) / terrain_type.scale;
                float sample_y = (y - (size / 2) + (offset)) / terrain_type.scale;
 
                float perlin_noise = Mathf.PerlinNoise(sample_x , sample_y);

                Color is_biome = perlin_noise <= terrain_type.occurrence? terrain_type.color : Color.black;
                
                pixels[y * size + x] = is_biome;
            }
        }

        return pixels;
    }
}


[System.Serializable]
public class TerrainType {
    public Color color;
    public float scale;
    [Range(0, 1)]public float occurrence;
}

[System.Serializable]
public class Biome : TerrainType {
    public TileBase tile;
    [HideInInspector]public Color[] vegetation_map;
    public List<Vegetation> vegetation;
}

[System.Serializable]
public class Vegetation : TerrainType{
    public GameObject prefab;
}