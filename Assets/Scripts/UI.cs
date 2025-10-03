using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour
{
    public TMP_Dropdown mapDropDown;
    public TMP_InputField mapWidth;
    public TMP_InputField mapHeigth;
    public Slider scaleSlider;
    public Slider octaveSlider;
    public Slider persistenceSlider;
    public Slider lacunaritySlider;
    public TMP_InputField seedField;
    public MapGenerator mapGenerator;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scaleSlider.onValueChanged.AddListener((v) =>
        {
            mapGenerator.noiseScale = v;
            UpdateTerrain();
        });
        
        mapDropDown.onValueChanged.AddListener((index) =>
        {
            mapGenerator.drawMode = (MapGenerator.DrawMode)index;
            UpdateTerrain();
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
