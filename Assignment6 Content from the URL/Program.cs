using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            // Input: URL
            Console.Write("Enter the URL: ");
            string url = Console.ReadLine();

            // Logic: Read content from URL
            string content = await ReadMainContentFromUrlAsync(url);

            // Output: Write content to file asynchronously
            await WriteContentToFileAsync(content);

            Console.WriteLine("Content has been written to A.txt successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static async Task<string> ReadMainContentFromUrlAsync(string url)
    {
        using (WebClient client = new WebClient())
        {
            string html = await client.DownloadStringTaskAsync(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Adjust the XPath expression to target the main content element
            HtmlNode mainContentNode = doc.DocumentNode.SelectSingleNode("//div[@class='main-content']");
            return mainContentNode?.InnerText.Trim(); // Extract inner text of the main content element
        }
    }

    static async Task WriteContentToFileAsync(string content)
    {
        using (StreamWriter writer = File.CreateText("A.txt"))
        {
            await writer.WriteAsync(content);
        }
    }
}
