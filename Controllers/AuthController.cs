using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;


public class AuthController : UmbracoApiController
{
    [HttpPost]
    public async Task<string> Login(FormBody data)
    {
        string soapString = @$"<soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:ICUTech.Intf-IICUTech""><soapenv:Header/><soapenv:Body><urn:Login soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><UserName xsi:type=""xsd:string"">{data.Username}</UserName><Password xsi:type=""xsd:string"">{data.Password}</Password><IPs xsi:type=""xsd:string""></IPs></urn:Login></soapenv:Body></soapenv:Envelope>";

        HttpResponseMessage response = await PostXmlRequest("http://isapi.icu-tech.com/icutech-test.dll/soap/IICUTech", soapString);
        string content = await response.Content.ReadAsStringAsync();

        return content;
    }

    public static async Task<HttpResponseMessage> PostXmlRequest(string baseUrl, string xmlString)
    {
        using (var httpClient = new HttpClient())
        {
            var httpContent = new StringContent(xmlString, Encoding.UTF8, "text/xml");

            return await httpClient.PostAsync(baseUrl, httpContent);
        }
    }

    public class FormBody
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}