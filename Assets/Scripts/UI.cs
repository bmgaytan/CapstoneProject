using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
public class UI : MonoBehaviour
{
    public TMP_Dropdown mapDropDown;
    public TMP_InputField mapWidth;
    public TMP_InputField mapHeight;
    public Slider scaleSlider;
    public Slider octaveSlider;
    public Slider persistenceSlider;
    public Slider lacunaritySlider;
    public TMP_InputField seedField;
    public MapGenerator mapGenerator;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        mapDropDown.onValueChanged.AddListener((index) =>
        {
            mapGenerator.drawMode = (MapGenerator.DrawMode)index;
            UpdateTerrain();
        });

        mapWidth.onEndEdit.AddListener((text) =>
        {
            if (int.TryParse(text, out int width))
            {
                mapGenerator.mapWidth = width;
                UpdateTerrain();
            }
        });

        mapHeight.onEndEdit.AddListener((text) =>
        {
            if (int.TryParse(text, out int height))
            {
                mapGenerator.mapHeight = height;
                UpdateTerrain();
            }
        });

        scaleSlider.onValueChanged.AddListener((v) =>
        {
            mapGenerator.noiseScale = v;
            UpdateTerrain();
        });

        octaveSlider.onValueChanged.AddListener((v) =>
        {
            mapGenerator.octaves = Mathf.RoundToInt(v);
            UpdateTerrain();
        });

        persistenceSlider.onValueChanged.AddListener((v) =>
        {
            mapGenerator.persistance = v;
            UpdateTerrain();
        });

        lacunaritySlider.onValueChanged.AddListener((v) =>
        {
            mapGenerator.lacunarity = v;
            UpdateTerrain();
        });
        
        seedField.onEndEdit.AddListener((text) =>
        {
            if (int.TryParse(text, out int seed))
            {
                mapGenerator.seed = seed;
                UpdateTerrain();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void UpdateTerrain()
    {
        if (mapGenerator != null)
            mapGenerator.GenerateMap();
    }
}
