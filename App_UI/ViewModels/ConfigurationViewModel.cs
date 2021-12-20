using App_UI.Commands;
using Microsoft.Extensions.Configuration;
using OpenWeatherAPI;
using System;

namespace App_UI.ViewModels
{
    public class ConfigurationViewModel : BaseViewModel
    {
        private string apiKey;

        public IWindDataService WindDataService { get; set; }
        public string ApiKey
        {
            get => apiKey;
            set
            {
                apiKey = value;
                OnPropertyChanged();
                TestConfigurationCommand?.RaiseCanExecuteChanged();
            }
        }

        private string testResult;

        public string TestResult
        {
            get { return testResult; }
            set { 
                testResult = value;
                OnPropertyChanged();
            }
        }

        private static IConfiguration configuration;
        public DelegateCommand<string> SaveConfigurationCommand { get; set; }
        public DelegateCommand<string> TestConfigurationCommand { get; set; }


        public ConfigurationViewModel()
        {
            Name = GetType().Name;

            ApiKey = GetApiKey();

            SaveConfigurationCommand = new DelegateCommand<string>(SaveConfiguration);
            TestConfigurationCommand = new DelegateCommand<string>(TestConfiguration, CanTest);
            
        }

        /// <summary>
        /// Permet de tester si la clé API fonctionne
        /// </summary>
        /// <param name="obj"></param>
        private async void TestConfiguration(string obj)
        {
            ApiHelper.InitializeClient();

            /// TODO 05 : Tester que l'appli est capable de récupérer la clé api
            /// et faire une appel à l'OpenWeatherProcessor avec GetOneCallAsync
            /// Copier le string du resultat dans TestResult
            /// 

            

            var result = "";


            //result =  await OpenWeatherProcessor.GetOneCallAsync();

            TestResult = result == null ? "Not working" : result.ToString();
        }
        public static string GetValue(string key)
        {
            if (configuration == null)
            {
                initConfig();
            }
            return configuration.GetValue<string>(key);
        }
        private static void initConfig()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddUserSecrets("f4f5746e-d24c-4697-b6cd-935a16b16033").Build();
        }

        private bool CanTest(string obj)
        {
            return !string.IsNullOrEmpty(ApiKey);
        }

        private void SaveConfiguration(string obj)
        {

            Properties.Settings.Default.apiKey = ApiKey;
            Properties.Settings.Default.Save();
        }

        private string GetApiKey()
        {
            return Properties.Settings.Default.apiKey;
        }

    }
}
