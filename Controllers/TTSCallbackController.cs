using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace tts_article_callback_with_csharp.Controllers
{
    public class TTSCallbackController : Controller
    {
        [HttpPost("api/tts-callback")]
        public ActionResult Callback([FromQuery] ArticleInfo articleInfo, [FromBody] TTSCallback ttsCallback)
        {
            if (ttsCallback.type == "nsw-info") return Ok();

            if (ttsCallback.type == "audio-info")
            {
                string id = articleInfo.id;
                string voice = ttsCallback.voice;
                string url = ttsCallback.url;

                // Save url as file
                using (var client = new WebClient())
                {
                    string filename = $"{id}_{voice}.mp3";
                    client.DownloadFile(url, filename);
                }

                return Ok();
            }

            return BadRequest();
        }
    }

    public class ArticleInfo
    {
        public string id { get; set; }
    }

    public class TTSCallback
    {
        public string type { get; set; }
        public string voice { get; set; }
        public string url { get; set; }
    }
}