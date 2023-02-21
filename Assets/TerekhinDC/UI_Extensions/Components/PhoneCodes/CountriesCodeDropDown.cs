using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace TerekhinDC.PhoneCodes
{
    
    [Serializable]
    public class CountriesArray
    {
        public Country[] countries;
    }

    [Serializable]
    public class Country
    {
        public string name;
        public string dial_code;
        public string code;
    }
    
    [RequireComponent(typeof(TMP_Dropdown))]
    public class CountriesCodeDropDown : MonoBehaviour
    {
        public TextAsset json;
        private static readonly Dictionary<string, string> PhoneCodes = new Dictionary<string, string>();
        private TMP_Dropdown _countries;
        public TMP_InputField phoneCode;
        public TMP_InputField phoneNumber;
        
        private void Awake ()
        {
            _countries = GetComponent<TMP_Dropdown>();
            
            foreach (var country in JsonUtility.FromJson<CountriesArray>(json.text).countries)
            {
                PhoneCodes.Add(country.name, country.dial_code);
            }
            
            _countries.AddOptions(GetCountriesList());
            _countries.onValueChanged.AddListener(Call);
        }

        private void Call(int value)
        {
            phoneCode.text = GetCountryPhoneCode(_countries.options[value].text);
        }

        private List <string> GetCountriesList ()
        {
            return PhoneCodes.Keys.ToList();
        }

        private static string GetCountryPhoneCode(string country)
        {
            return PhoneCodes[country];
        }
        
    }
}
