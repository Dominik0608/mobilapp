using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class autoload : MonoBehaviour
{
    void Start()
    {
        // indításkor kiüríti a panelek szövegeit
        StartCoroutine(GetRequest("https://www.danko.pro/mobilapp?city=NoCity"));
    }

    public void UpdateCity()
    {
        // keresés gombra kattintva az input boxban lévő szöveg (város) alapján kéri le a weboldalomról az adatokat
        string city = GameObject.Find("CitySearch").GetComponent<Text>().text;
        StartCoroutine(GetRequest("https://www.danko.pro/mobilapp?city=" + city));
    }

    // webrequest (adatok lekérése weboldalról)
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                MakeUI(webRequest.downloadHandler.text);
            }
        }
    }

    public void MakeUI(string content)
    {
        // panelekben lévő szövegek felülírása a dátummal és hőmérsékletekkel
        WeatherInfo weather = JsonUtility.FromJson<WeatherInfo>(content);
        for(int i = 0; i < 12; i++)
        {
            if (weather == null) {
                GameObject.Find("Text (" + i + ")").GetComponent<Text>().text = "";
            } else {
                GameObject.Find("Text (" + i + ")").GetComponent<Text>().text = weather.date[i] + "\n" + weather.max[i].ToString() + "°\n" + weather.min[i].ToString() + "°";
            }
            // kinézet módosítás
            GameObject.Find("Text (" + i + ")").GetComponent<Text>().fontSize = 60;
            GameObject.Find("Text (" + i + ")").GetComponent<Text>().lineSpacing = 1.2f;
        }
    }
    
    // osztály az json-höz
    [System.Serializable]
    public class WeatherInfo
    {
        public int[] max;
        public int[] min;
        public string[] date;
    }
}
