using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text;
using System.Net.Http.Json;
using System.Text.Json;
using System.Drawing;

namespace SoccerCitEConsole;
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Soccer Console starting...");

        // Why do I need this part?
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        // Why can't I do anything without the HttpClientHandler?
        HttpClient client = new HttpClient(clientHandler);
        string uri = "https://localhost:7156/api/Customer";

        // Getting our image data from an existing image... User will upload their image OR Logic Layer finds default image
        FileInfo image = new FileInfo("test.png");
        byte[] imageData = new byte[image.Length];

        // Put image data into byte array using filestream
        using(FileStream stream = image.OpenRead()) {
            stream.Read(imageData, 0, imageData.Length);
        }
        
        // Instantiate a customer DTO with the image data, and send it to our API as a POST request (register a customer)
        CustomerDTO testCustomer = new CustomerDTO("email2@email.net", "pleaseworkuser2", "password", imageData);
        var customerContent = new StringContent(JsonSerializer.Serialize(testCustomer), Encoding.UTF8, "application/json");
        var postResponse = await client.PostAsync(uri, customerContent);

        // Can't Deserialize here? Why? Is this just a console app thing? Probably... data transfers okay from API <-> DB
        string jsonString = await postResponse.Content.ReadAsStringAsync();
        Console.WriteLine(jsonString);
        CustomerDTO result = JsonSerializer.Deserialize<CustomerDTO>(jsonString);
        Console.WriteLine($"{result.Email}\n{result.Username}\n{result.Password}\n{result.ImageData}");

        // System.Drawing.Common --version 7.0.0 to do this
        // MemoryStream ms1 = new MemoryStream(result.ImageData);
        // Image resultImage = Image.FromStream(ms1);
        // resultImage.Save("test.png");
    }
}
