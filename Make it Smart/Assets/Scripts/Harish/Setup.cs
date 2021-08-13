using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    [SerializeField] public Transform[] Buildings;
    [SerializeField] private Sprite psUpgrade;
    [SerializeField] private Sprite hospUpgrade;
    [SerializeField] private Sprite gridUpgrade;
    [SerializeField] private Sprite indsUpgrade;
    [SerializeField] private Sprite offUpgrade;
    [SerializeField] private Sprite MunicipalityUpgrade;
    [SerializeField] private Sprite slumsUpgrade;

    public Sprite[] psUpgradeButtons;
    public Sprite[] hospUpgradeButtons;
    public Sprite[] gridUpgradeButtons;
    public Sprite[] indsUpgradeButtons;
    public Sprite[] offUpgradeButtons;
    public Sprite[] MunicipalityUpgradeButtons;
    public Sprite[] slumsUpgradeButtons;

    public Dictionary<string,Sprite> sprite;
    public Dictionary<string, string[]> upgradeList;
    public Dictionary<string, Sprite[]> upgradeSprite;//If required to fill images to all UpgradeButtons
    public Dictionary<string, int[]> upgradeScores;
    public Dictionary<string, float[]> upgradeTime;
    public Dictionary<string, int[]> upgradeCost;
    public Dictionary<string, int[]> upgradeLevel;
    public Dictionary<string, Boolean[]> isButtonDisabled;
    void Start()
    {
        sprite = new Dictionary<string, Sprite>();
        upgradeList = new Dictionary<string, string[]>();
        upgradeSprite = new Dictionary<string, Sprite[]>();
        upgradeScores = new Dictionary<string, int[]>();
        upgradeTime = new Dictionary<string, float[]>();
        upgradeCost = new Dictionary<string, int[]>();
        upgradeLevel = new Dictionary<string, int[]>();
        isButtonDisabled = new Dictionary<string, Boolean[]>();
        foreach (Transform Building in Buildings)
        {
            if (Building.tag == "Untagged")
                Building.tag = "Building";
        }
        initialize();
    }
    void initialize()
    {
        sprite.Add("PoliceStation", psUpgrade);
        sprite.Add("Hospital", hospUpgrade);
        sprite.Add("Grid", gridUpgrade);
        sprite.Add("Industry", indsUpgrade);
        sprite.Add("Office", offUpgrade);
        sprite.Add("Municipality", MunicipalityUpgrade);
        sprite.Add("Slums", slumsUpgrade);

        //Add Upgrades for remaining Buildings similarly
        upgradeList.Add("Hospital", new string[] { "Multi Super-Specialty Hospital(Cost: 7000, Required Level: 3)",
            "Modernize Medical Equipment(Cost: 1000, Required Level: 1)",
            "e-Healthcare Initiatives(Cost: 1500, Required Level: 2)",
            "Organize Health Awareness Campaigns(Cost: 400, Required Level: 1)",
            "Ensure Proper Biomedical Waste Treatment(Cost: 800, Required Level: 2)" });
        
        upgradeList.Add("PoliceStation", new string[] { "Virtual Police Station(Cost: 2800, Required Level: 3)",
            "Modernize Police Equipment(Cost: 1400, Required Level: 1)",
            "CCTV Monitoring in City(Cost: 1750, Required Level: 2)",
            "Eco-friendly Traffic Awareness Campaigns(Cost: 450, Required Level: 1)",
            "Digitalize Police Records(Cost: 500, Required Level: 1)", "Cyber Security Cell(Cost: 1000, Required Level: 2)" });
        
        upgradeList.Add("Grid", new string[] { "Construct Solar Farm(Cost: 5000, Required Level: 3)",
            "Introduce Electric Cars(Cost: 1800, Required Level: 2)",
            "Smart Grid(Cost: 5000, Required Level: 3)", "Construct Windmills(Cost: 2000, Required Level: 2)" });
       //solar farm  //windmill
        upgradeList.Add("Industry", new string[] { "Industry 4.0 Technologies (Cost: 6000, Required Level: 3)",
            "Smart Supply Chain(Cost: 1500, Required Level: 1)",
            "Waste Treatment(Cost: 1800, Required Level: 1)", "Pollution Tower(Cost: 2000, Required Level: 2)" });
        //pollution tower
        upgradeList.Add("Office", new string[] { "Infrastructure Upgrades(Cost: 1000, Required Level: 1) ",
            "High Speed Network(Cost: 1500, Required Level: 1)",
            "Organize Tech Expo(Cost: 2000, Required Level: 2)",
            "Training Programs for Employees(Cost: 1800, Required Level: 2)",
            "Startup Incubators(Cost: 1300, Required Level: 1)", 
            "Improve e-Commerce facilities(Cost: 2200, Required Level: 1)" });
        
        upgradeList.Add("Municipality", new string[] { "Waste Sorting at Source(Cost: 1200, Required Level: 1)",
            "Smart Bins(Cost: 2400, Required Level: 2)", "Revamp Sewage System(Cost: 4000, Required Level: 3)",
            "Improve Water Treatment Plant Capacity(Cost: 3000, Required Level: 2)",
            "Increase Tax(Cost: 600, Required Level: 2)",
            "Transit Apps for Public Transportation(Cost: 1500, Required Level: 1)",
            "Intelligent Transportation Systems(Cost: 3500, Required Level: 2)",
            "Road Maintaince(Cost: 1000, Required Level: 1)",
            "Maintenance of Public Buildings(Cost: 800, Required Level: 1)",
            "Improve E-Transport Initiatives(Cost: 1500, Required Level: 1) ",
            "Eco-Toilets(Cost: 1800, Required Level: 2)",
            "Strengthen rules for Pollution Clearance of Vehicles(Cost: 1400, Required Level: 2)"});
        //waste bin color change
        upgradeList.Add("Slums", new string[] { "Revamp Housing(Cost: 5500, Required Level: 2)",
            "Clean Water Supply(Cost: 1800, Required Level: 1)",
            "Improve Sanitation Facilities(Cost: 1300, Required Level: 1)",
            "Provide Electricity at Reasonable Costs(Cost: 1500, Required Level: 2)",
            "Provide Health Facilities(Cost: 1500, Required Level: 1)",
            "Revamp Road Network(Cost: 2000, Required Level: 2)",
            "Welfare Schemes for Children(Cost: 2500, Required Level: 1" });
        //sprite change for slums

        upgradeTime.Add("Hospital", new float[] { 30f, 11f, 13f, 8f, 14f});
        upgradeTime.Add("PoliceStation", new float[] { 15f, 10f, 10f, 10f, 10f, 15f });
        upgradeTime.Add("Grid", new float[] { 25f, 15f, 25f, 12f });
        upgradeTime.Add("Industry", new float[] { 25f, 10f, 12f, 15f });
        upgradeTime.Add("Office", new float[] { 11f, 11f, 11f, 12f, 12f, 12f });
        upgradeTime.Add("Municipality", new float[] { 9f, 15f, 20f, 20f, 5f, 10f, 18f, 7f, 7f, 11f, 10f, 12f });
        upgradeTime.Add("Slums", new float[] { 25f, 12f, 12f, 12f, 12f, 12f, 10f });


        //default value of Boolean is false
        isButtonDisabled.Add("Hospital", new Boolean[5]);
        isButtonDisabled.Add("PoliceStation", new Boolean[6]);
        isButtonDisabled.Add("Grid", new Boolean[4]);
        isButtonDisabled.Add("Industry", new Boolean[4]);
        isButtonDisabled.Add("Office", new Boolean[6]);
        isButtonDisabled.Add("Municipality", new Boolean[12]);
        isButtonDisabled.Add("Slums", new Boolean[7]);

        upgradeCost.Add("Hospital", new int[] { 7000, 1000, 1500, 400, 800 });
        upgradeCost.Add("PoliceStation", new int[] {2800, 1400, 1750, 450, 500, 1000 });
        upgradeCost.Add("Grid", new int[] { 5000, 1800, 5000, 2000 });
        upgradeCost.Add("Industry", new int[] { 6000, 1500, 1800, 2000 });
        upgradeCost.Add("Office", new int[] { 1000, 1500, 2000, 1800, 1300, 2200 });
        upgradeCost.Add("Municipality", new int[] { 1200, 2400, 4000, 3000, 600, 1500, 3500, 1000,800, 1500, 1800, 1400 });
        upgradeCost.Add("Slums", new int[] { 5500, 1800, 1300, 1500, 1500, 2000, 2500 });

        upgradeSprite.Add("PoliceStation",psUpgradeButtons);
        upgradeSprite.Add("Hospital",hospUpgradeButtons);
        upgradeSprite.Add("Grid", gridUpgradeButtons);
        upgradeSprite.Add("Industry", indsUpgradeButtons);
        upgradeSprite.Add("Office", offUpgradeButtons);
        upgradeSprite.Add("Municipality", MunicipalityUpgradeButtons);
        upgradeSprite.Add("Slums", slumsUpgradeButtons);

        upgradeScores.Add("Hospital", new int[] { 700, 100, 150, 50, 75 });
        upgradeScores.Add("PoliceStation", new int[] { 125, 150, 175, 50, 50, 100 });
        upgradeScores.Add("Grid", new int[] { 500, 150, 500, 200 });
        upgradeScores.Add("Industry", new int[] { 550, 125, 150, 150 });
        upgradeScores.Add("Office", new int[] { 150, 100, 200, 100, 100, 150 });
        upgradeScores.Add("Municipality", new int[] { 100, 200, 450, 250, 0, 100, 350, 75, 75, 100, 150, 150 });
        upgradeScores.Add("Slums", new int[] { 550, 200, 150, 125, 150, 125, 200 });

        upgradeLevel.Add("Hospital", new int[] { 3,1,2,1,2});
        upgradeLevel.Add("PoliceStation", new int[] { 3,1,2,1,1,2});
        upgradeLevel.Add("Grid", new int[] {3,2,3,2 });
        upgradeLevel.Add("Industry", new int[] { 3,1,1,2 });
        upgradeLevel.Add("Office", new int[] { 1,1,2,2,1,1 });
        upgradeLevel.Add("Municipality", new int[] { 1,2,3,2,2,1,2,1,1,1,2,2 });
        upgradeLevel.Add("Slums", new int[] { 2,1,1,2,1,2,1 });
    }
}