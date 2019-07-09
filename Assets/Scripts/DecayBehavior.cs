using UnityEngine;
using System.Collections;
using System;

public class DecayBehavior : MonoBehaviour {

    [Header("Config")]
    public float Decay;

    [Header("Hud")]
    public GameObject WaterLevelIndicatorObj;

    [Header("Geo Groups:")]
    public GameObject Grp_TreesAObj;
    public GameObject Grp_TreesBObj;
    public GameObject Grp_TreesCObj;
    public GameObject Grp_GrassAObj;
    public GameObject Grp_GrassBObj;
    public GameObject Grp_GrassCObj;
    public GameObject Grp_BushesAObj;
    public GameObject Grp_BushesBObj;
    public GameObject Grp_BushesCObj;
    public GameObject Grp_GroundObj;

    [Header("Water:")]
    public GameObject Grp_WaterObj;
    Vector3 WaterPosition;
    public float Water_Low;
    public float Water_High;
    
   

    [Header("Light:")]
    public GameObject LightObject;
    public float Light_Intensity_Min;
    public float Light_Intensity_Max;


    [Header("Driven keys properties:")]
    public Animator DrivenKeysAnimator;
    public GameObject WaterDriverObj;
    public GameObject WaterBaseColorDriver;
    public GameObject WaterReflectionColorDriver;
    public GameObject LightDriverObj;
    public GameObject TreesADriverObj;
    public GameObject TreesAColorDriverObj;
    public GameObject TreesBDriverObj;
    public GameObject TreesBColorDriverObj;
    public GameObject TreesCDriverObj;
    public GameObject TreesCColorDriverObj;
    public GameObject GrassADriverObj;
    public GameObject GrassAColorDriverObj;
    public GameObject GrassBDriverObj;
    public GameObject GrassBColorDriverObj;
    public GameObject GrassCDriverObj;
    public GameObject GrassCColorDriverObj;
    public GameObject BushesADriverObj;
    public GameObject BushesAColorDriverObj;
    public GameObject BushesBDriverObj;
    public GameObject BushesBColorDriverObj;
    public GameObject BushesCDriverObj;
    public GameObject BushesCColorDriverObj;
    public GameObject GroundCrossFadeDriverObj;

    [Header("Fx properties:")]
    public GameObject DustObj;
	public GameObject FireObj;
    bool DustFlag = false;
	bool FireFlag = false;

    Renderer rend;
    Color Col;
    Light lightSource;
    Color BaseColor;
    Color ReflectionColor;

    // int Frame = 0;
    // bool HalfFpsFlag = false;

    ParticleSystem DustSystem;
    ParticleSystem.EmissionModule DustModule;

    // ----------------------------------
    void Start() {
        DustSystem = DustObj.GetComponent<ParticleSystem>();
        DustModule = DustSystem.emission;

        rend = Grp_GroundObj.GetComponent<Renderer>();
        rend.material.SetFloat("_CrossFade", 0);

        lightSource = LightObject.GetComponent<Light>();
        lightSource.intensity = 1.5f;

        WaterLevelIndicatorObj.transform.localPosition = new Vector3(0, (-Decay + 0.5f)*5f, 0);
    }

    // ----------------------------------
    void Update() {

        UpdateDrivenKeysAnimator();

       /*
        if (HalfFpsFlag)
        {
            Frame++;
            Application.CaptureScreenshot("Screenshot"+Frame.ToString()+".jpg");
        }
        HalfFpsFlag = !HalfFpsFlag;
        */

    }

    // ----------------------------------
    void LateUpdate()
    {
    }

    // ----------------------------------
    void UpdateDrivenKeysAnimator()
    {
        if (Decay < 0) Decay = 0;
        if (Decay >= 1f) Decay = 1f;

		DrivenKeysAnimator.Play("DrivenKeys", 0, Decay);
    }

    // ----------------------------------
    void UpdateWaterLevel()
    {
        WaterPosition = Grp_WaterObj.transform.localPosition;
        WaterPosition.y = Water_Low + (WaterDriverObj.transform.localPosition.y * ( Mathf.Abs(Water_Low - Water_High)  ) );
        Grp_WaterObj.transform.localPosition = WaterPosition;

        rend = Grp_GroundObj.GetComponent<Renderer>();
        rend.material.SetFloat("_CrossFade", GroundCrossFadeDriverObj.transform.localPosition.y);
    }
    
    // ----------------------------------
    void UpdateLight()
    {
        lightSource.intensity = Light_Intensity_Min + (LightDriverObj.transform.localPosition.y * (Mathf.Abs(Light_Intensity_Min - Light_Intensity_Max)));
    }

    // ----------------------------------
    void UpdateWaterColor()
    {
        UpdateWaterMaterials
            (
            Grp_WaterObj.transform,
            WaterBaseColorDriver.transform.localPosition.x,
            WaterBaseColorDriver.transform.localPosition.y,
            WaterBaseColorDriver.transform.localPosition.z,
            WaterBaseColorDriver.transform.localEulerAngles.y,
            WaterReflectionColorDriver.transform.localPosition.x,
            WaterReflectionColorDriver.transform.localPosition.y,
            WaterReflectionColorDriver.transform.localPosition.z,
            WaterReflectionColorDriver.transform.localEulerAngles.y
            );
    }

    // ----------------------------------
    void UpdateTrees()
    {
        UpdateTreeMaterials(Grp_TreesAObj.transform, 1 - TreesADriverObj.transform.localPosition.y, TreesAColorDriverObj.transform.localPosition.y);
        UpdateTreeMaterials(Grp_TreesBObj.transform, 1 - TreesBDriverObj.transform.localPosition.y, TreesBColorDriverObj.transform.localPosition.y);
	    UpdateTreeMaterials(Grp_TreesCObj.transform, 1 - TreesCDriverObj.transform.localPosition.y, TreesCColorDriverObj.transform.localPosition.y);
		
    }

    // ----------------------------------
    void UpdateGrasses()
    {
        UpdateGrassMaterials(Grp_GrassAObj.transform, 1 - GrassADriverObj.transform.localPosition.y, GrassAColorDriverObj.transform.localPosition.y);
        UpdateGrassMaterials(Grp_GrassBObj.transform, 1 - GrassBDriverObj.transform.localPosition.y, GrassBColorDriverObj.transform.localPosition.y);
        UpdateGrassMaterials(Grp_GrassCObj.transform, 1 - GrassCDriverObj.transform.localPosition.y, GrassCColorDriverObj.transform.localPosition.y);
    }

    // ----------------------------------
    void UpdateBushes()
    {
        UpdateTreeMaterials(Grp_BushesAObj.transform, 1 - BushesADriverObj.transform.localPosition.y, BushesAColorDriverObj.transform.localPosition.y);
        UpdateTreeMaterials(Grp_BushesBObj.transform, 1 - BushesBDriverObj.transform.localPosition.y, BushesBColorDriverObj.transform.localPosition.y);
        UpdateTreeMaterials(Grp_BushesCObj.transform, 1 - BushesCDriverObj.transform.localPosition.y, BushesCColorDriverObj.transform.localPosition.y);
    }

    // ----------------------------------
    void UpdateWaterMaterials(Transform Parent, float BaseColorR,float BaseColorG, float BaseColorB, float BaseColorA, float ReflectionColorR, float ReflectionColorG, float ReflectionColorB, float ReflectionColorA)
    {
        foreach (Transform U in Parent)
        {
            foreach (Transform T in U)
            {
                Renderer rend = T.GetComponent<Renderer>();
                if (rend != null)
                {
                    //Base Color
                    Color c = rend.material.GetColor("_BaseColor");
                    BaseColor.r = BaseColorR;
                    BaseColor.g = BaseColorG;
                    BaseColor.b = BaseColorB;
                    BaseColor.a = BaseColorA;
                    rend.material.SetColor("_BaseColor", BaseColor);

                    //Reflection Color
                    Color d = rend.material.GetColor("_ReflectionColor");
                    ReflectionColor.r = ReflectionColorR;
                    ReflectionColor.g = ReflectionColorG;
                    ReflectionColor.b = ReflectionColorB;
                    ReflectionColor.a = ReflectionColorA;
                    rend.material.SetColor("_ReflectionColor", ReflectionColor);

                }
            }
        }
    }

    // ----------------------------------
    void UpdateTreeMaterials(Transform Parent, float AlphaValue, float MainColor)
    {
        foreach (Transform U in Parent)
        {
            foreach (Transform T in U)
            {
                rend = T.GetComponent<Renderer>();
                if (rend != null)
                {
                    string[] splitString = rend.material.name.Split(new string[] { "_" }, StringSplitOptions.None);
                        for (int i = 0; i < rend.materials.Length; i++)
                        {
                            if (splitString[0] == "Leaves" || splitString[0] != "FacingLeaves")
                            {
                                Color c = rend.materials[i].color;
                                Col.r = MainColor;
                                Col.g = MainColor;
                                Col.b = MainColor; 
                                Col.a = AlphaValue;
                                rend.sharedMaterials[i].color = Col;
                            }
                            else{
                                Col.r = MainColor;
                                Col.g = MainColor;
                                Col.b = MainColor;
                                rend.sharedMaterials[i].color = Col;
                            }
						 }
                }
            }
        }
    }

    // ----------------------------------
    void UpdateGrassMaterials(Transform Parent, float AlphaValue, float MainColor)
    {
        foreach (Transform U in Parent)
        {
            foreach (Transform T in U)
            {
                rend = T.GetComponent<Renderer>();
                if (rend != null)
                {
                        for (int i = 0; i < rend.materials.Length; i++)
                        {
                        Color c = rend.materials[i].color;
                        Col.r = MainColor;
                        Col.g = MainColor;
                        Col.b = MainColor;
                        Col.a = AlphaValue;
                        rend.sharedMaterials[i].color = Col;
                        }
                }
            }
        }
    }

    // ----------------------------------
    public void SetDecay(float Value)
    {
        Decay = Value;

        if (Decay < 0) Decay = 0;
        if (Decay >= 1f) Decay = 0.99f;

        if (Decay > 0.5f && DustFlag == false)
        {
            DustFlag = true;
            DustModule.rateOverTime = 20;
        }
        if (Decay < 0.5f && DustFlag == true)
        {
            DustFlag = false;
            DustModule.rateOverTime = 0;
        }
		if(Decay > 0.2f && FireFlag == false)
		{
			FireFlag = true;
			//FireObj.SetActive(true);
		}
		if(Decay < 0.8f && FireFlag == true)
		{
			FireFlag = false;
			//FireObj.SetActive(false);
		}
        RefreshAssets();

        Resources.UnloadUnusedAssets();
    }

    // ----------------------------------
    void RefreshAssets()
    {
        WaterLevelIndicatorObj.transform.localPosition = new Vector3(0, (-Decay + 0.5f) * 5f, 0);

        UpdateWaterLevel();
        UpdateWaterColor();
        UpdateLight();
        UpdateTrees();
        UpdateGrasses();
        UpdateBushes();
    }

    // ----------------------------------
    public float GetDecay()
    {
        return Decay;
    }


}
