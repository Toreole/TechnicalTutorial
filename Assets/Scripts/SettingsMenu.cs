using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

namespace Tutorial
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField]
        protected PlayerController player;

        public void UpdateCameraInversion(bool invert)
        {
            //send this to the player to fix rotation on X axis (up and down)
            //PlayerPrefs. Save this
            Debug.Log(invert);
            player.InvertXRotation = invert;
        }

        private void OnDisable()
        {
            player.enabled = true;
        }

        //Save the scenes manually, and ONLY the scenes. 
        public void SaveScenes()
        {
            string[] loadedScenes = CustomSceneManager.GetLoadedScenes();
            if(loadedScenes.Length == 0)
            {
                loadedScenes = new string[] { "hello", "world", "12345Scene" };
            }
            //the file path in appdata/locallow/company/product
            string filePath = Path.Combine(Application.persistentDataPath, "saveFile.sf");
            //Open or override the file 
            FileStream stream = File.Open(filePath, FileMode.Create);

            foreach (string scene in loadedScenes)
            {
                byte[] buffer = Encoding.UTF8.GetBytes($"{scene}|");
                stream.Write(buffer, 0, buffer.Length);
            }
            
            stream.Flush();
            stream.Dispose();
        }

        //save all of our games data in one go.
        public void SaveJson()
        {
            //get the loaded scenes from the scene manager.
            string[] loaded = CustomSceneManager.GetLoadedScenes();
            if (loaded.Length == 0) //if we have no scenes loaded, just set some random ones for testing purposes.
            {
                loaded = new string[] { "hello", "world", "12345Scene" };
            }

            //set up some fake player data to save.
            PlayerSaveData data = new PlayerSaveData()
            {
                loadedScenes = loaded,
                x = 1,
                y = 2,
                z = 5
            };

            //get the JSON string, looks something like this:
//            {
//                "loadedScenes": [
//                    "hello",
//                    "world",
//                    "12345Scene"
//                  ],
//                  "x": 1.0,
//                  "y": 2.0,
//                  "z": 5.0
//              }
            string save = JsonUtility.ToJson(data, true);

            //get the file path. this is at CurrentUser/AppData/LocalLow/Company/Product/saveFile.sf
            //Company and Product are set by us in the Project Settings / Player.
            string filePath = Path.Combine(Application.persistentDataPath, "saveFile.sf");
            //Open the File: Create a new one
            FileStream stream = File.Open(filePath, FileMode.Create);

            //We need a byte array to actually write anything to our FileStream, we get this from standard UTF8 encoding
            //? What is UTF8? https://en.wikipedia.org/wiki/UTF-8
            //! A lot of modern websites use UTF-8 Encoding since it has support for a pretty wide variety of characters :)
            byte[] buffer = Encoding.UTF8.GetBytes(save);

            //Write the entire buffer array to the stream.
            stream.Write(buffer, 0, buffer.Length);
            
            //Flush the stream. This is essential to "dumping" all the data from the RAM that C# uses into the storage on the harddrive.
            stream.Flush();
            //Dispose the stream, this can be simplified when utilizing a using block: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-statement
            stream.Dispose();
        }

        //Reverse the Json string to something we can use for our game.
        public void LoadJson()
        {
            //Again, just get the file path.
            string filePath = Path.Combine(Application.persistentDataPath, "saveFile.sf");
            //Check if the file actually exists, if not we would run into problems later, so just stop this method call.
            if (!File.Exists(filePath))
                return;

            //Open the file.
            FileStream stream = File.Open(filePath, FileMode.Open);
            //initialize some buffer.
            byte[] buffer = new byte[1024];
            //Read all contents of the stream to the buffer.
            stream.Read(buffer, 0, (int)stream.Length);
            //we can close the stream already, we wont need it anymore.
            stream.Dispose();

            //Reverse the UTF8 encoding we did earlier.
            string fileContent = Encoding.UTF8.GetString(buffer);

            //Let Unity handle getting our player data from the fileContent string, this just works.
            PlayerSaveData saveData = JsonUtility.FromJson<PlayerSaveData>(fileContent);
            //yay
            Debug.Log("yay");
            
        }

        //Manually loading the scenes from the file, this would be annoying to add more to.
        //!Not commented.
        public void LoadScenes()
        {
            string filePath = Path.Combine(Application.persistentDataPath, "saveFile.sf");
            if (!File.Exists(filePath))
                return;

            FileStream stream = File.Open(filePath, FileMode.Open);
            byte[] buffer = new byte[1024];
            stream.Read(buffer, 0, (int)stream.Length);
            stream.Dispose();

            string fileContent = Encoding.UTF8.GetString(buffer);
            Debug.Log(fileContent);

            string[] scenes = fileContent.Split('|');
            foreach (string scene in scenes)
                Debug.Log(scene);
        }

        //Just delete the existing save file.
        public void DeleteSave()
        {
            string filePath = Path.Combine(Application.persistentDataPath, "saveFile.sf");
            File.Delete(filePath);
        }
    }

    //Some arbitrary data storage class for the data relevant for our game.
    //This could include things like health, inventory, chests opened, etc....
    [System.Serializable]
    public class PlayerSaveData
    {
        public string[] loadedScenes;
        public float x, y, z;
    }
}