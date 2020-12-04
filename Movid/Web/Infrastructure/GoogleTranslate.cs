using Movid.App.Models;

namespace Movid.App.Infrastructure
{
    public static class GoogleTranslate
    {
        //https://www.googleapis.com/language/translate/v2?key=AIzaSyD1dBkVSZ_M3R8kLM2M8Jiwm5ltxrO6yjk&target=es&q=Learn%20how%20to%20take%20advantage%20of%20Movid's%20powerful%20home%20exercise%20program%20and%20marketing%20features%20by%20completing%20the%20following%20tasks.

        public static string Translate(string language, string text)
        {
            var baseUrl = "https://www.googleapis.com/language/translate/v2?key=AIzaSyD1dBkVSZ_M3R8kLM2M8Jiwm5ltxrO6yjk&target={0}&q={1}";

            var url = string.Format(baseUrl, language, text);

            var response = new System.Net.WebClient().DownloadString(url);
            
            dynamic json = JObject.Parse(response);

            var translated = json.data.translations[0].translatedText;

            return translated;

        }

        public static void TranslateExercise(Program program, string language)
        {
            foreach (var exer in program.Exercises)
            {


                exer.Description = Translate(language, exer.Description);
                exer.Name = Translate(language, exer.Name);
            }
        }
    }
}