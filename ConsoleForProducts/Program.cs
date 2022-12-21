using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;

namespace ConsoleForProducts;
class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("SoccerCitE: Starting product console...");

        // get image data
        List<byte[]> data = new List<byte[]>{
            GetImageData("shoe.jpg"),
            GetImageData("ball.jpg"),
            GetImageData("AregentinaJersey.jpg"),
        };

        // create product objects, put them in a list
        ProductDTO soccerShoes = new ProductDTO(8, "Nike Soccer Cleats", "Can make you run fast.", 174.99, 120, ProductCategory.SHOES, data[0]);
        ProductDTO soccerBall = new ProductDTO(9, "White Soccer Ball", "Good ball.", 14.99, 100, ProductCategory.BALLS, data[1]);
        ProductDTO soccerJersey = new ProductDTO(10, "2022 Argentina Jersey", "Messi may have worn this shirt.", 34.99, 100, ProductCategory.JERSEYS, data[2]);
        List<ProductDTO> products = new List<ProductDTO>{
            soccerShoes, soccerBall, soccerJersey
        };

        // create handler
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        // create client, get the uri
        HttpClient client = new HttpClient(handler);
        string uri = "https://localhost:7156/api/Product";

        // for each element in the list, do a post request
        foreach(ProductDTO product in products) {
            try {
                var productContent = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
                var postResponse = await client.PostAsync(uri, productContent);
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }

    // From microsoft docs; get the bytes from an image
    public static byte[] GetImageData(string filePath) {
        FileStream stream = new FileStream(  
        filePath, FileMode.Open, FileAccess.Read);  
        BinaryReader reader = new BinaryReader(stream);  
        
        byte[] imageData = reader.ReadBytes((int)stream.Length);  
        
        reader.Close();  
        stream.Close();  
        
        return imageData;
    }
}
