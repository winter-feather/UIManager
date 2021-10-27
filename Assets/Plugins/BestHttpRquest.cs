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
    public void BestHttpGet(string url, Dictionary<string, string> parms, OnRequestFinishedDelegate callback, Dictionary<string, string> header = null)
    {
        if (parms != null)
        {
            url += "?";
            foreach (var item in parms)
            {
                url += item.Key + "=" + item.Value + "&";
            }
            url = url.TrimEnd(' ', '&');
        }
        Debug.Log("<color=orange>Aaron Tips:BestHttp Request Url=</color>" + url);
        HTTPRequest req = new HTTPRequest(new Uri(url), HTTPMethods.Get);
        req.Callback += MessgeTips;
        req.Callback += callback;
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
    public void BestHttpGet(string url, Dictionary<string, string> parms, Dictionary<string, byte[]> bparms, OnRequestFinishedDelegate callback, Dictionary<string, string> header = null)
    {
        if (parms != null)
        {
            url += "?";
            foreach (var item in parms)
            {
                url += item.Key + "=" + item.Value + "&";
            }
            url = url.TrimEnd(' ', '&');
        }
        Debug.Log("<color=orange>Aaron Tips:BestHttp Request Url=</color>" + url);
        HTTPRequest req = new HTTPRequest(new Uri(url), HTTPMethods.Get);
        req.Callback += MessgeTips;
        req.Callback += callback;
        req.DisableCache = true;
        if (header != null)
            foreach (var item in header)
            {
                req.AddHeader(item.Key, item.Value);
            }
        if (bparms != null)
            foreach (var item in bparms)
            {
                req.AddBinaryData(item.Key, item.Value);
            }
        if (parms != null)
            foreach (var item in parms)
            {
                req.AddField(item.Key, item.Value);
            }
        req.Send();
    }
    public void BestHttpGet(string url, string parms, OnRequestFinishedDelegate callback, Dictionary<string, string> header = null)
    {
        Debug.Log("<color=orange>Aaron Tips:BestHttp Request Url=</color>" + url);
        HTTPRequest req = new HTTPRequest(new Uri(url), HTTPMethods.Get);
        req.Callback += MessgeTips;
        req.Callback += callback;
        req.DisableCache = true;
        if (header != null)
        {
            foreach (var item in header)
            {
                req.AddHeader(item.Key, item.Value);
            }
        }
        req.RawData = System.Text.Encoding.UTF8.GetBytes(parms);
        req.Send();
    }
    public void BestHttpPost(string url, Dictionary<string, string> parms, OnRequestFinishedDelegate callback, Dictionary<string, string> header = null)
    {
        //Debug.Log("<color=orange>Aaron Tips:BestHttp Request Url=</color>" + url);
        HTTPRequest req = new HTTPRequest(new Uri(url), HTTPMethods.Post);
        req.Callback += MessgeTips;
        req.Callback += callback;
        req.DisableCache = true;
        if (header != null)
        {
            foreach (var item in header)
            {
                req.AddHeader(item.Key, item.Value);
            }
        }
        if (parms != null)
        {
            foreach (var item in parms)
            {
                req.AddField(item.Key, item.Value);
            }
        }
        req.Send();
    }
    public void BestHttpPost(string url, Dictionary<string, string> parms, Dictionary<string, byte[]> bparms, OnRequestFinishedDelegate callback, Dictionary<string, string> header = null)
    {
        HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Post);
        request.Callback += MessgeTips;
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
    public void BestHttpPost(string url, string parms, OnRequestFinishedDelegate callback, Dictionary<string, string> header = null)
    {
        HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Post);
        request.Callback += MessgeTips;
        request.Callback += callback;
        request.DisableCache = true;
        if (header != null)
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

    #region Message
    public T JsonConvert<T>(string v)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(v);
    }
    public void MessageNotContent(HTTPRequest req, HTTPResponse respon)
    {
        Debug.Log("net is not content");
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    CEditorManager.GetAndoridInstance().Call("showUnityToast", @"无网络");
        //}
    }
    public void MessageNetFailure(HTTPRequest req, HTTPResponse respon)
    {
        Debug.Log("failure:" + respon.StatusCode);
        Debug.Log("success:" + req.Uri);
        Debug.Log("response:" + respon.Message);
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    CEditorManager.GetAndoridInstance().Call("showUnityToast", @"网络异常");
        //}
    }
    public void MessageSuccess(HTTPRequest req, HTTPResponse respon)
    {
        Debug.Log("IsSuccess:" + req.Uri);
        Debug.Log("response:" + respon.DataAsText);
    }
    public void MessgeTips(HTTPRequest req, HTTPResponse respon)
    {
        if (respon == null)
        {
            MessageNotContent(req, respon);
        }
        else
        {
            if (respon.IsSuccess && respon.StatusCode == 200)
            {
                Debug.Log("success:" + req.Uri);
                Debug.Log("response:" + respon.DataAsText);
            }
            else
            {
                Debug.Log("failure:" + respon.StatusCode);
                Debug.Log("success:" + req.Uri);
                Debug.Log("response:" + respon.Message);
            }
        }
    }
    #endregion
}
