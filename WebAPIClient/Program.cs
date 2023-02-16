using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

private static readonly HttpClient client = new HttpClient();

class genderName
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("count")]
    public int Amount { get; set;}

    [JsonProperty("gender")]
    public string Gender {get; set;}

    [JsonProperty("probability")]
    public double Probability {get; set;}
}

static async Task Main(string[] args)
{
    await ProcessRepositories();
}

private static async Task ProcessRepositories()
{
    try
    {
        while (true)
        {
            Console.WriteLine("What name do you want to analyze? Leave name blank to exit");
            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                break;
            }

            var result = await client.GetAsync("https://api.genderize.io?name=") + name.ToLower();
            var result_read = await result.Content.ReadAsStringAsync();

            Console.WriteLine("=== RESULT ===");
            if (name.Amount == 1)
            {
                Console.WriteLine("There is one person named " + name.Name + ".");
            }
            else
            {
                Console.WriteLine("There are " + name.Amount + " people named " + name.Name + ".");
            }
            Console.WriteLine("The most likely gender is " + name.Gender + ".");
            Console.WriteLine("The chances are " + name.Probability + ".");
        }
    }

    catch (Exception)
    {
        Console.WriteLine("Name doesn't exist. Write another one.");
    }
    
}