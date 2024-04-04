using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class islandScript : MonoBehaviour
{
    public List<GameObject> islands;

    private int islandType;

    // Start is called before the first frame update
    void Start()
    {
        islandType = Random.Range(0, 10);
        name = transform.GetComponentInParent<islandGeneratorScript>().getName();
        load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("load")]
    public void load()
    {
        Instantiate(islands[islandType], transform, false);
    }

    [ContextMenu("unload")]
    public void unload()
    {
        Transform islandTransform = gameObject.GetComponentsInChildren<Transform>()[2];
        Destroy(islandTransform.gameObject);
    }

    private List<string> islandNameList = new List<string>()
    {
        "Banana",
        "Easter Island",
        "Rumble Tumble",
        "Britain",
        "Kansas",
        "Zeeland",
        "Mars",
        "Integral",
        "Limaçon",
        "The Tower",
        "Airstrip One",

        "Diver Island",
        "Monohansett Island",
        "Steins Gate",
        "Silver Key Reef",
        "Sleeper Island",
        "Olamon Island",
        "Tripp Ledge",
        "Tichenor Rock",
        "Beacon Island",

        "Basswood",
        "Canoe Island",
        "Finland",
        "East Harbor Key",
        "Black Hammock",
        "Fanning Island",
        "Topekas Isle",
        "Liffy Point",
        "Old Kelick Island",

        "Qochyax",
        "Creswell Point",
        "North Huckleberry Point",
        "Vick",
        "Soward",
        "Redin",
        "Chino",
        "Star Isle",
        "Key Way",

        "Canary Bar",
        "Finger",
        "Little Sheep",
        "Skagg",
        "Goodluck Sedge",
        "Old Man Bar",
        "Magnuson Point",
        "Bearhead Island",

        "Le Baron Point",
        "Fifth Island",
        "Bloody Bluff",
        "The Pyramid",
        "Bathhouse",
        "Outside Rock",
        "Curlew’s Towhead",
        "Cline’s Point",
        "Little Mandeville",

        "Huron",
        "Saint Margarette’s Island",
        "Galberry Rock",
        "Oskenonton",
        "Little Summer Bar",
        "Cudjoe Key",
        "Dad Rock",
        "Floater",

        "Boyd Point",
        "Balls Rock",
        "Deer Island",
        "Grandpap Island",

        "Harrington Reef",
        "Skipper",
        "Yellowhead",
        "Kalispell",
        "Campobello",
        "Fort Farm",
        "Old Skunk Isle",
        "Beaver",

        "Big Tim",
        "Sheen Rock",
        "Bragdene Point",
        "Cacaway",
        "Kettle Point",
        "Keaoi",
        "Planet Island",
        "Whitney Island",

        "Narrow Reef",
        "Pennyworth Island",
        "Polygon",
        "Caesar Point",

        "Nye Ledge",
        "Geneva",
        "Lime Bar",
        "Grass Island",
        "Fischer Island",
        "Mixon",
        "Shippingport Reef",

        "Pixtaway",
        "Trouble Brewing Bar",
        "Dreamland",
        "Nokomis",
        "Rocknose",

        "The Cube",
        "Island of Unity",
        "Octahedron",
        "Codeland",
        "Purpleland",
        "Legoland",
        "Calculos",
        "Kragle",
        "Stone Rock",
        "Wet Cell",
        "Metaphor",
        "Eggman Refuge",

        "Rose Dhu",
        "Fourche",
        "Sturgeon Bar",
        "Blanchard",

        "Philo",
        "Ribeyre",
        "Ramsay Island",
        "Westlake",
        "Cottrell Key",
        "Big Grass",

        "Bread",
        "Chair",
        "Acacia",
        "Meidoorn",
        "Colgate",
        "Shed",
        "Jambe",
        "Enleva",
        "The Orb",
        "Point of Inflection",

        "Derivative",
        "Tangent Isle",
        "Logarithm",
        "Quaternion",
        "Radian",
        "Pi Island",


        "Wallace",
        "Knapp Isle",
        "Boeuf",
        "Eastern Point",
        "Blackbeard Bar",
        "Cayuga Island",

        "Crowley Towhead",
        "Woolsey Reef",
        "Barnum Point",
        "Large Island",
        "Weed Reef",
        "Skidaway Island",
        "Skibidi",
        "Caswell Island",
        "Owen’s Reef",

        "Fork Watch",
        "Beer Quarter",
        "Ice Pigeon",
        "Vague Undertake",
        "Copper Ferry",
        "Tropical Trial",
        "Peanut Subway",
        "Vegetation District",
        "Dairy Maze",
        "Hens’ Exile",
        "Grandmother Asylum",
        "The Path",
        "Letterhead",

        "Boltzmann Island",
        "Mike’s House",
        "Virtual Island",
        "Imaginary Island",
        "Greg",
        "Floortile",
        "Sumatra",
        "Fries land",
        "Mayonnaise",
        "Banana 2",

        "Aruna",
        "Frog Springs",
        "Kokomo",
        "Palawan",
        "Corfu",
        "Caprisun",
        "Monopoli",
        "Parenthesis",
        "Semicolon",

        "Sourdough Island",
        "Reed Bird Island",
        "Box Island",
        "The Good Island",
        "Isle of the Mohawks",


        "Weepecket Island",
        "Powder Isle",
        "Orange Harbor Island",
        "Cucumber Island",
        "Kalaemamo Point",
        "Massie Island"
    };
}
