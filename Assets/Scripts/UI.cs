using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
public class UI : MonoBehaviour
{
    public MapGenerator mapGenerator;

    public TMP_Dropdown mapDropDown;
    public TMP_InputField mapWidth;
    public TMP_InputField mapHeight;
    public UnityEngine.UI.Slider scaleSlider;
    public UnityEngine.UI.Slider octaveSlider;
    public UnityEngine.UI.Slider persistenceSlider;
    public UnityEngine.UI.Slider lacunaritySlider;
    public TMP_InputField seedField;

    public TMP_Text scaleLabel;
    public TMP_Text octaveLabel;
    public TMP_Text persistenceLabel;
    public TMP_Text lacunarityLabel;

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
            UpdateLabels();
        });

        octaveSlider.onValueChanged.AddListener((v) =>
        {
            mapGenerator.octaves = Mathf.RoundToInt(v);
            UpdateTerrain();
            UpdateLabels();
        });

        persistenceSlider.onValueChanged.AddListener((v) =>
        {
            mapGenerator.persistance = v;
            UpdateTerrain();
            UpdateLabels();
        });

        lacunaritySlider.onValueChanged.AddListener((v) =>
        {
            mapGenerator.lacunarity = v;
            UpdateTerrain();
            UpdateLabels();
        });

        seedField.onEndEdit.AddListener((text) =>
        {
            if (int.TryParse(text, out int seed))
            {
                mapGenerator.seed = seed;
                UpdateTerrain();
            }
        });

        UpdateLabels();
    }

    // Update is called once per frame
    private void UpdateLabels()
    {
        if (scaleLabel) scaleLabel.text = $"Scale: {scaleSlider.value:F1}";
        if (octaveLabel) octaveLabel.text = $"Octaves: {octaveSlider.value}";
        if (persistenceLabel) persistenceLabel.text = $"Persistence: {persistenceSlider.value}";
        if (lacunarityLabel) lacunarityLabel.text = $"Lacunarity: {lacunaritySlider.value:F1}";
    }
    private void UpdateTerrain()
    {
        if (mapGenerator != null)
            mapGenerator.GenerateMap();
    }
}
