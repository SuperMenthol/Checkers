using Assets.Scripts.Infrastructure.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class APIConfigurator
    {
        private readonly string _configurationPath;
        private Rootobject _configurationFile;

        private string _baseAddress;

        public APIConfigurator()
        {
            _configurationPath = Path.Combine(Application.dataPath, "Resources", "APIConfig");
            _configurationPath = Path.Combine(_configurationPath, Directory.GetFiles(_configurationPath, "*.json").FirstOrDefault());

            using (var sr = new StreamReader(_configurationPath))
            {
                var reader = new JsonTextReader(sr);
                reader.Read();

                JsonSerializer b = new JsonSerializer();
                _configurationFile = b.Deserialize<Rootobject>(reader);
            }

            _baseAddress = _configurationFile.APIConnection.BaseAddress;
        }

        public string CreatePlayer(string playerName) => 
            string.Format("{0}{1}{2}"
                , _baseAddress
                , _configurationFile.APIConnection.Methods.CreatePlayer
                , playerName);

        public string Login(string playerName) =>
            string.Format("{0}{1}{2}"
                , _baseAddress
                , _configurationFile.APIConnection.Methods.Login
                , playerName);

        public string PostMatchResult() =>
            string.Format("{0}{1}"
                , _baseAddress
                , _configurationFile.APIConnection.Methods.PostMatchResult);
    }
}