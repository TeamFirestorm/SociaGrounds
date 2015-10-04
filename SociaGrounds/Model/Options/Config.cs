using System;
using System.Diagnostics;
using Windows.Storage;
using Newtonsoft.Json;

namespace SociaGrounds.Model.Options
{
    /// <summary>
    /// Config class to help store all the data of the current and previous run of the application
    /// </summary>
    public class Config
    {
        //fields to store the instance and a path to the config
        private static readonly StorageFolder FOLDER = ApplicationData.Current.LocalFolder;
        private static readonly CreationCollisionOption OPTION = CreationCollisionOption.ReplaceExisting;

        public static Config Instance { get; private set; }

        public static void SetConfig()
        {
            CheckFile();
        }

        //config location [MyPC]
        //C:\Users\Thijs Reeringh\AppData\Local\Packages\06555f44-ea7d-4208-9516-b46db7fde559_vvgn2p0ea0ewy\LocalState\config.json

        private async static void CheckFile()
        {
            //if the instance is already filled, return it
            if (Instance != null) return;

            bool fileExist = true;

            StorageFile file = null;

            try
            {
                file = await FOLDER.GetFileAsync("config.json");
            }
            catch (Exception)
            {
                fileExist = false;
            }
            

            //else, if the path exits, 
            if (fileExist && file != null)
            {
                //convert the json from the path
                string data = await FileIO.ReadTextAsync(file);
                Instance = JsonConvert.DeserializeObject<Config>(data);

                //return the instance
                if (Instance != null) return;

                //if it hasn't received anything, create new config
                Instance = new Config();
                Instance.Save();
#if DEBUG
                Debug.WriteLine(@"new config created;");
#endif
            }
            else
            {
                //create new config
                Instance = new Config();
                Instance.Save();
#if DEBUG
                Debug.WriteLine(@"new config created;");
#endif
            }
        }

        #region Save To Config File
        /// <summary>
        /// Save the config
        /// </summary>
        private async void Save()
        {
            string data = JsonConvert.SerializeObject(Instance, Formatting.Indented);
            // create file 
            StorageFile file = await FOLDER.CreateFileAsync("config.json", OPTION);
            await FileIO.WriteTextAsync(file, data);
        }

        #endregion

        #region Constructor
        /// <summary>
        /// content of the Config file
        /// </summary>
        private Config()
        {
            MutedMusic = false;
        }

        /// <summary>
        /// Get or set the maximum of image pieces
        /// </summary>
        [JsonProperty(PropertyName = "mutedMusic")]
        public bool MutedMusic { get; set; }

        #endregion
    }
}
