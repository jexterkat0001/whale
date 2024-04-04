using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class islandGeneratorScript : MonoBehaviour
{
    public GameObject islandPrefab;

    [SerializeField]
    private int[] thresholds;
    [SerializeField]
    private float spawnChance;
    [SerializeField]
    private float spawnChanceIncrease;
    [SerializeField]
    private float deletionDistance;

    public List<GameObject> islandList = new List<GameObject>();
    public List<Vector2>[] islandRings = {new List<Vector2>(), new List<Vector2>(), new List<Vector2>(), new List<Vector2>()};

    // Start is called before the first frame update
    void Start()
    {
        spawnIslands();

        Debug.Log(islandRings[0].Count);
        Debug.Log(islandRings[1].Count);
        Debug.Log(islandRings[2].Count);
        Debug.Log(islandRings[3].Count);
    }




    // Update is called once per frame
    void Update()
    {


    }


    private void spawnIslands()
    {
        for (int ring = 0; ring < 4; ring++)
        {
            for (int i = thresholds[ring]; i < thresholds[ring + 1]; i++)
            {
                if (Random.Range(0f, 100f) < spawnChance + i * spawnChanceIncrease)
                {
                    float angle = Random.Range(0f, 2 * Mathf.PI);

                    Vector2 islandPosition = new Vector2(i * Mathf.Cos(angle), i * Mathf.Sin(angle));


                    if (checkIslandOverlaps(islandPosition))
                    {
                        spawnIsland(islandPosition);
                        islandRings[ring].Add(islandPosition);
                    }
                }
            }
        }
    }


    private bool checkIslandOverlaps(Vector2 islandPosition)
    {
        for (int i = 0; i < islandList.Count; i++)
        {
            Vector3 islandIPosition = islandList[i].transform.position;
            if (pythagorean(islandPosition, new Vector2(islandIPosition.x, islandIPosition.y)) < deletionDistance)
            {
                return false;
            }
        }

        return true;
    }


    private float pythagorean(Vector2 position1, Vector2 position2)
    {
        return (Mathf.Sqrt(Mathf.Pow((position1.x - position2.x), 2) + Mathf.Pow((position1.y - position2.y), 2)));
    }


    private void spawnIsland(Vector2 location)
    {
        islandList.Add(Instantiate(islandPrefab, new Vector3(location.x, location.y, -1), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)), this.transform) as GameObject);
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
