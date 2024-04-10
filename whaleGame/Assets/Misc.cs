using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misc : MonoBehaviour
{
    public bool testMode;

    public float pythagorean(Vector2 position1, Vector2 position2)
    {
        return (Mathf.Sqrt(Mathf.Pow((position1.x - position2.x), 2) + Mathf.Pow((position1.y - position2.y), 2)));
    }

    public float pythagorean(GameObject go1, GameObject go2)
    {
        return (Mathf.Sqrt(Mathf.Pow((go1.transform.position.x - go2.transform.position.x), 2) + Mathf.Pow((go1.transform.position.y - go2.transform.position.y), 2)));
    }

    public string getName()
    {
        string name = islandNameList[Random.Range(0, islandNameList.Count - 1)];
        islandNameList.Remove(name);
        return name;
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

        "Peanut Subway",
        "Vegetation District",

        "Hens’ Exile",
        "Grandmother Asylum",
        "The Path",

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
