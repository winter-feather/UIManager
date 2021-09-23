using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using System;
using Newtonsoft;


public class BestHttpRquest
{
    #region HttpRquest
    static BestHttpRquest instance;
    public static BestHttpRquest Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BestHttpRquest();
            }
            return instance;
        }
    }

    public string baseUrl;
    public bool isShowInfo;

    public void BestHttpGet<T>(string url, Dictionary<string, string> parms, Action<T, HTTPRequest, HTTPResponse> callback, Dictionary<string, string> header = null)
    {
        if (parms != null)
        {
            url += "?";
            int c = 0;
            foreach (var item in parms)
            {
                url += item.Key + "=" + item.Value + "&";
            }
            url = url.TrimEnd(' ', '&');
        }
        Debug.Log("<color=orange>Aaron Tips:BestHttp Request Url=</color>" + url);
        HTTPRequest req = new HTTPRequest(new Uri(url), HTTPMethods.Get);
        req.Callback += (HTTPRequest request, HTTPResponse response) =>
        {
            if (response == null)
            {
                Debug.Log("net is not content");
            }
            else
            {
                if (response.IsSuccess && response.StatusCode == 200)
                {
                    Debug.Log("IsSuccess:" + request.Uri);
                    Debug.Log("response:" + response.DataAsText);
                    T dataObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.DataAsText);
                    callback?.Invoke(dataObject, request, response);
                    //OnReceivedSchoolScene();
                }
                else
                {
                    Debug.Log("failureCode:" + response.StatusCode);
                    Debug.Log("failureUrl:" + request.Uri);
                    Debug.Log("response:" + response.Message);
                }
            }
        };
        req.DisableCache = true;
        if (header != null)
        {
            foreach (var item in header)
            {
                req.AddHeader(item.Key, item.Value);
            }
        }
        req.Send();
    }
    public void BestHttpPost<T>(string url, Dictionary<string, string> parms, Action<T, HTTPRequest, HTTPResponse> callback, Dictionary<string, string> header = null)
    {
        //Debug.Log("<color=orange>Aaron Tips:BestHttp Request Url=</color>" + url);
        HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Post);
        request.Callback += (HTTPRequest req, HTTPResponse response) =>
        {
            if (response == null)
            {
                Debug.Log("net is not content");
            }
            else
            {
                if (response.IsSuccess && response.StatusCode == 200)
                {
                    Debug.Log("success:" + req.Uri);
                    Debug.Log("response:" + response.DataAsText);
                    //
                    T dataObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.DataAsText);
                    callback?.Invoke(dataObject, req, response);
                }
                else
                {
                    Debug.Log("failure:" + response.StatusCode);
                    Debug.Log("failureurl:" + req.Uri);
                    Debug.Log("response:" + response.Message);
                }
            }
        }; ;
        request.DisableCache = true;
        if (header != null)
        {
            foreach (var item in header)
            {
                request.AddHeader(item.Key, item.Value);
            }
        }
        if (parms != null)
        {
            foreach (var item in parms)
            {
                request.AddField(item.Key, item.Value);
            }
        }
        request.Send();
    }
    public void BestHttpPost<T>(string url, Dictionary<string, string> parms, Dictionary<string, byte[]> bparms, Action<T, HTTPRequest, HTTPResponse> callback, Dictionary<string, string> header = null)
    {
        HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Post);
        request.Callback += (HTTPRequest req, HTTPResponse response) =>
        {
            if (response == null)
            {
                Debug.Log("net is not content");
            }
            else
            {
                if (response.IsSuccess && response.StatusCode == 200)
                {
                    Debug.Log("success:" + req.Uri);
                    Debug.Log("response:" + response.DataAsText);
                    T dataObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.DataAsText);
                    callback?.Invoke(dataObject, req, response);
                }
                else
                {
                    Debug.Log("failure:" + response.StatusCode);
                    Debug.Log("success:" + req.Uri);
                    Debug.Log("response:" + response.Message);
                }
            }
        }; 
        request.DisableCache = true;

        foreach (var item in bparms)
        {
            request.AddBinaryData(item.Key, item.Value);
        }
        foreach (var item in header)
        {
            request.AddHeader(item.Key, item.Value);
        }
        foreach (var item in parms)
        {
            request.AddField(item.Key, item.Value);
        }
        request.Send();
    }
    public void BestHttpPost<T>(string url, string parms, Action<T, HTTPRequest, HTTPResponse> callback, Dictionary<string, string> header = null)
    {
        HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Post);
        request.Callback += (HTTPRequest req, HTTPResponse response) =>
        {
            if (response == null)
            {
                Debug.Log("net is not content");
            }
            else
            {
                if (response.IsSuccess && response.StatusCode == 200)
                {
                    Debug.Log("success:" + req.Uri);
                    Debug.Log("p:" + System.Text.Encoding.UTF8.GetString(req.RawData));
                    Debug.Log("response:" + response.DataAsText);
                    T dataObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.DataAsText);
                    callback?.Invoke(dataObject, req, response);
                }
                else
                {
                    Debug.Log("failure:" + response.StatusCode);
                    Debug.Log("failure:" + req.Uri);
                    Debug.Log("p:" +System.Text.Encoding.UTF8.GetString(req.RawData));
                    Debug.Log(":" + req.Uri);
                    Debug.Log("response:" + response.Message);
                }
            }
        }; 
        request.DisableCache = true;
        if (header!=null)
        {
            foreach (var item in header)
            {
                request.AddHeader(item.Key, item.Value);
            }
        }
      
        request.RawData = System.Text.Encoding.UTF8.GetBytes(parms);
        request.Send();
    }
    #endregion
}

public class StringResult {
    int code;
    string ok;
    string result;
}
