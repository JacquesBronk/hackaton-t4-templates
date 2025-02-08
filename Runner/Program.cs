// See https://aka.ms/new-console-template for more information

using System.Text.Json;

var JsonDir = ("./Maker/Definition.json");
var Json = System.IO.File.ReadAllText(JsonDir);
var configurationData =  JsonDocument.Parse(Json).RootElement;
var ProjectName = configurationData.GetProperty("AppName").ToString();  