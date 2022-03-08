using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
//Mostly taken from Brackeys vid https://youtu.be/XOjd_qU2Ido

public static class SaveSystem
{
    public static void SaveAllData(CharacterMovement player, ObservatoryMaster observatoryMaster, AlmanacProgression alm, TutorialManagerScript tutorials, NewInventory inventory, PlantsData plants)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/squimbus.progress";
        FileStream fs = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, observatoryMaster, alm, tutorials, inventory, plants);

        try
        {
            formatter.Serialize(fs, data);
        }
        catch (System.Exception)
        {
            fs.Close();
            Debug.LogError("Save failed, exception");
            throw;
        }
        fs.Close();
        Debug.Log("Save complete to " + path);
    }

    public static PlayerData LoadAllData()
    {
        string path = Application.persistentDataPath + "/squimbus.progress";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            PlayerData data;
            try
            {
                data = formatter.Deserialize(fs) as PlayerData;

            }
            catch (System.Exception)
            {
                fs.Close();
                Debug.LogError("Load failed, exception");
                throw;
            }

            fs.Close();
            Debug.Log("Load complete from " + path);

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
                return null;
        }
    }
}
