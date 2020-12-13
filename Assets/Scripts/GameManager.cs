using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string currentLevel;
    public string nextLevel;
    public WeaponManager weaponManager;
    public PlayerStatus playerStatus;
    public UnityEvent onInit;

    void Start()
    {   
        PlayerPrefs.SetString("LEVEL",currentLevel);
        onInit.Invoke();
        loadGame();
        //saveGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openScene(string sceneName){
         SceneManager.LoadScene(sceneName);
    }

    public void saveString(string key,string val){
        PlayerPrefs.SetString(key,val);
    }

    public void saveInt(string key,int val){
        PlayerPrefs.SetInt(key,val);
    }

    public void RestartLevel(){
        openScene(currentLevel);
    }

    public void OpenNextLevel(){
        openScene(nextLevel);
    }

    public void saveGame(){
        string[] weapons = new string[weaponManager.myWeapons.Count];
        for (int i = 0; i < weaponManager.myWeapons.Count; i++)
        {
            var mw = weaponManager.myWeapons[i];
            weapons[i] = mw.weapon.weaponName+"|"+mw.weaponAmmo;
        }
        PlayerPrefsX.SetStringArray ("MyWeapons",weapons);
        saveInt("PlayerHealth",(int)playerStatus.playerHealth);
        
    }

    public void loadGame(){
        var weapons = PlayerPrefsX.GetStringArray ("MyWeapons");
        
        for (int i = 0; i < weapons.Length; i++)
        {
            string[] s = ((string)weapons[i]).Split(char.Parse("|"));
            var wi = weaponManager.findWeaponByName(s[0]);
            if(wi >= 0){
                weaponManager.giveWeapon((int)wi,int.Parse(s[1]));
            }
        }
        playerStatus.playerHealth = (float)PlayerPrefs.GetInt("PlayerHealth",100);
    }

    public void resetData(){
        PlayerPrefs.SetString("LEVEL",null);
        PlayerPrefs.SetInt("PlayerHealth",100);
        PlayerPrefsX.SetStringArray ("MyWeapons",new string[0]);
    }

    public void exitGame(){
        Application.Quit();
    }
}
