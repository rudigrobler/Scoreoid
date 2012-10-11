using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Scoreoid
{
    public class ScoreoidClient
    {
        private const string response = "XML"; // Currently only support XML
        private static readonly Regex ErrorRegex = new Regex(@"<error\b[^>]*>(.*?)</error>", RegexOptions.Singleline);

        private static readonly Regex SuccessRegex = new Regex(@"<success\b[^>]*>(.*?)</success>",
                                                               RegexOptions.Singleline);

        private readonly string api_key;
        private readonly string game_id;

        public ScoreoidClient(string api_key, string game_id)
        {
            this.api_key = api_key;
            this.game_id = game_id;
        }

        public async Task<string> CreateScoreAsync(string username, int score)
        {
            var post_data = new Dictionary<string, string>();
            post_data["api_key"] = api_key;
            post_data["game_id"] = game_id;
            post_data["response"] = response;
            post_data["username"] = username;
            post_data["score"] = score.ToString();

            const string uri = "https://www.scoreoid.com/api/createScore";
            string post_response = await ScoreoidPostAsStringAsync(uri, post_data);

            Match successRegex = SuccessRegex.Match(post_response);
            if (successRegex.Success)
            {
                return successRegex.Value.TrimResponse("success");
            }

            throw new ScoreoidException();
        }

        public async Task<string> CreatePlayerAsync(player player)
        {
            var post_data = new Dictionary<string, string>();
            post_data["api_key"] = api_key;
            post_data["game_id"] = game_id;
            post_data["response"] = response;

            post_data.Inject(player);

            const string uri = "https://www.scoreoid.com/api/createPlayer";
            string post_response = await ScoreoidPostAsStringAsync(uri, post_data);

            Match successRegex = SuccessRegex.Match(post_response);
            if (successRegex.Success)
            {
                return successRegex.Value.TrimResponse("success");
            }

            throw new ScoreoidException();
        }

        public async Task<players> GetPlayerAsync(string username, string password = null, string email = null)
        {
            var post_data = new Dictionary<string, string>();
            post_data["api_key"] = api_key;
            post_data["game_id"] = game_id;
            post_data["response"] = response;
            post_data["username"] = username;
            if (!string.IsNullOrEmpty(password))
                post_data["password"] = password;
            if (!string.IsNullOrEmpty(email))
                post_data["email"] = email;

            const string uri = "https://www.scoreoid.com/api/getPlayer";

            return await ScoreoidPostAsync<players>(uri, post_data);
        }

        public async Task<scores> GetBestScoresAsync(string order_by = "score", string order = "desc", int limit = 10)
        {
            var post_data = new Dictionary<string, string>();
            post_data["api_key"] = api_key;
            post_data["game_id"] = game_id;
            post_data["response"] = response;
            post_data["order_by"] = order_by;
            post_data["order"] = order;
            post_data["limit"] = limit.ToString();

            const string uri = "https://www.scoreoid.com/api/getBestScores";

            return await ScoreoidPostAsync<scores>(uri, post_data);
        }

        #region POST

        private async Task<T> ScoreoidPostAsync<T>(string uri, Dictionary<string, string> post_data)
        {
            var serializer = new XmlSerializer(typeof (T));
            return (T) serializer.Deserialize(await ScoreoidPostAsStreamAsync(uri, post_data));
        }

        private async Task<Stream> ScoreoidPostAsStreamAsync(string uri, Dictionary<string, string> post_data)
        {
            var handler = new HttpClientHandler {Proxy = WebRequest.DefaultWebProxy};
            var client = new HttpClient(handler);

            HttpResponseMessage responseMessage = await client.PostAsync(uri, new FormUrlEncodedContent(post_data));
            Match error = ErrorRegex.Match(await responseMessage.Content.ReadAsStringAsync());
            if (error.Success)
            {
                throw new ScoreoidException(error.Value.TrimResponse("error"));
            }

            return await responseMessage.Content.ReadAsStreamAsync();
        }

        private async Task<string> ScoreoidPostAsStringAsync(string uri, Dictionary<string, string> post_data)
        {
            var handler = new HttpClientHandler {Proxy = WebRequest.DefaultWebProxy};
            var client = new HttpClient(handler);

            HttpResponseMessage responseMessage = await client.PostAsync(uri, new FormUrlEncodedContent(post_data));
            string responseMessageContent = await responseMessage.Content.ReadAsStringAsync();

            Match errorRegex = ErrorRegex.Match(responseMessageContent);
            if (errorRegex.Success)
            {
                throw new ScoreoidException(errorRegex.Value.TrimResponse("error"));
            }

            return responseMessageContent;
        }

        #endregion
    }
}