using ImageResizer;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace Movid.App.Infrastructure
{
    public class Vimeo
        {
            public MemoryStream GenerateThumbnail(string videoId, int size)
            {
                try
                {

                    var url = "http://vimeo.com/api/oembed.json?url=http%3A//vimeo.com/" + videoId;

                    var request = WebRequest.Create(url);
                    string text;
                    var response = (HttpWebResponse) request.GetResponse();

                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        text = sr.ReadToEnd();
                    }

                    dynamic job = JObject.Parse(text);

                    var webClient = new WebClient();
                    byte[] imageBytes = webClient.DownloadData(job.thumbnail_url.ToString());

                    //var source = ResizeStream(imageBytes, 300);
                    var ms = new MemoryStream(imageBytes);
                    var dest = new MemoryStream();

                    var r = new ResizeSettings() {};
                    r.MaxWidth = size;
                    r.Format = "jpg";
                    var eee = new ImageResizer.ImageJob(ms, dest, r);
                    eee.Build();
                    return dest;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }

     