# Client for CDNsun CDN API

SYSTEM REQUIREMENTS

* .NET Standard >= 2.0

CDN API DOCUMENTATION

https://cdnsun.com/knowledgebase/api

BUILD

  To build the .NET client open the corresponding solution from **/cdn-api-client-dotnet/** folder in Visual Studio 2017 or newer and build.
  As an alternative open **/cdn-api-client-dotnet/** folder in CMD tool and run the following command:
```  
dotnet build
```  

CLIENT USAGE

* Create the client

```
var client = new CdnApiClient(username, password);
```

* Get CDN service reports (https://cdnsun.com/knowledgebase/api/documentation/res/cdn/act/reports)

```
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("type", "GB");
        data.Add("period", "4h");
        var relativeUrl = $"cdns/{_serviceid}/reports";
        await client.GetAsync(data, relativeUrl);
```
* Purge CDN service content (https://cdnsun.com/knowledgebase/api/documentation/res/cdn/act/purge)

```
        var data = "{	\"purge_paths\" :  [ \"/path1.img\", \"/path2.img\"]}";
        var relativeUrl = $"cdns/{_serviceid}/purge";

        return await client.PostAsync(data, relativeUrl);
```

NOTES

* The {_serviceid} stands for a CDN service ID, it is an integer number, eg. 123, to find your CDN service ID please visit the Services/How-To (https://cdnsun.com/cdn/how-to) page in the CDNsun CDN dashboard.

TEST USAGE

Run the following command under the **/CdnClientTest/** folder:

```
dotnet run -username YOUR_API_USERNAME -password YOUR_API_PASSWORD -serviceid YOUR_CDN_SERVICE_ID

```

CONTACT

* W: https://cdnsun.com
* E: info@cdnsun.com
