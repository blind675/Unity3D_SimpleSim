using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject InfoPannel;
    public TextMeshProUGUI TileText;
    public TextMeshProUGUI HeightText;
    public TextMeshProUGUI SloapText;
    public TextMeshProUGUI ErosionText;
    public TextMeshProUGUI HumidityText;
    

    public static UIController Instance { get; protected set; }
    public Tile SelectedTile;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple World Controllers");
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedTile != null)
        {
            InfoPannel.SetActive(true);
            TileText.text = SelectedTile.type.ToString();
            HeightText.text = "Height: " + SelectedTile.Height;
            SloapText.text = "Sloap: " + SelectedTile.SloapOrientation.Direction + " - " + SelectedTile.SloapValue;
            ErosionText.text = "Erosion: " + SelectedTile.ErosionCoefficient.ToString("F08");
            HumidityText.text = "-";
        }
        else {
            InfoPannel.SetActive(false);
        }
    }

}
