using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace TerekhinDC.UI_Extensions.Components
{
    [AddComponentMenu("Terekhin DC/UI Extensions")]
    [RequireComponent(typeof(TMP_Dropdown))]
    public class EditableSearchDropDown : MonoBehaviour
    {
        private TMP_Dropdown _dropdown;
        private string _searchResult;
        private List<TMP_Dropdown.OptionData> _original;

        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
        }

        private IEnumerator Start()
        {
            while (_dropdown.options.Count == 0)
            {
                yield return null;
            }
            
            _original = _dropdown.options.ToList();
        }

        private void Update()
        {
            if (!Input.anyKeyDown || 
                Input.GetMouseButtonDown(0) ||
                Input.GetMouseButtonDown(1) ||
                Input.GetMouseButtonDown(2)) return;
            
            if (Input.GetKeyDown(KeyCode.Backspace) && !string.IsNullOrEmpty(_searchResult)) 
                _searchResult = _searchResult.TrimEnd(_searchResult[^1]);
            else if (!Input.GetKeyDown(KeyCode.Backspace)) _searchResult+=Input.inputString;

            if (!string.IsNullOrEmpty(_searchResult) && !char.IsUpper(_searchResult[0]))
                _searchResult = _searchResult[0].ToString().ToUpper() + _searchResult[1..];
            Debug.Log(_searchResult);

            var query = from word in _original
                where word.text.Contains(_searchResult)
                select word;
            
            _dropdown.options = string.IsNullOrEmpty(_searchResult) ? _original : query.ToList();
            
            _dropdown.enabled = false;
            _dropdown.enabled = true;
            _dropdown.Show();
        }

        public static GameObject Create()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<EditableSearchDropDown>();
            return gameObject;
        }
    }
}
