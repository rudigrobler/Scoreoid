﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Reflection;

namespace Scoreoid
{
    public class ScoreoidClient
    {
        private string api_key;
        private string game_id;
        private string response = "XML"; // Currently only support XML

        public ScoreoidClient(string api_key, string game_id)
        {
            this.api_key = api_key;
            this.game_id = game_id;
        }
        
        private static readonly Regex ErrorRegex = new Regex(@"<error\b[^>]*>(.*?)</error>", RegexOptions.Singleline);
        private static readonly Regex SuccessRegex = new Regex(@"<success\b[^>]*>(.*?)</success>", RegexOptions.Singleline);
        private static readonly Regex PlayersRegex = new Regex(@"<players\b[^>]*>(.*?)</players>", RegexOptions.Singleline);

        public async Task<string> CreateScore(string username, int score)
        {
            var post_data = new Dictionary<string, string>();
            post_data["api_key"] = api_key;
            post_data["game_id"] = game_id;
            post_data["response"] = response;
            post_data["username"] = username;
            post_data["score"] = score.ToString();

            string uri = "https://www.scoreoid.com/api/createScore";
            string post_response = await ScoreoidPostAsString(uri, post_data);

            var successRegex = SuccessRegex.Match(post_response);
            if (successRegex.Success)
            {
                return successRegex.Value.TrimResponse("success");
            }

            throw new ScoreoidException();
        }

        public async Task<string> CreatePlayer(player player)
        {
            var post_data = new Dictionary<string, string>();
            post_data["api_key"] = api_key;
            post_data["game_id"] = game_id;
            post_data["response"] = response;

            post_data.Inject<player>(player);

            string uri = "https://www.scoreoid.com/api/createPlayer";
            string post_response = await ScoreoidPostAsString(uri, post_data);

            var successRegex = SuccessRegex.Match(post_response);
            if (successRegex.Success)
            {
                return successRegex.Value.TrimResponse("success");
            }

            throw new ScoreoidException();
        }

        public async Task<players> GetPlayer(string username, string password = null, string email = null)
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

            string uri = "https://www.scoreoid.com/api/getPlayer";

            return await ScoreoidPost<players>(uri, post_data);
        }

        public async Task<scores> GetBestScores(string order_by = "score", string order = "desc", int limit = 10)
        {
            var post_data = new Dictionary<string, string>();
            post_data["api_key"] = api_key;
            post_data["game_id"] = game_id;
            post_data["response"] = response;
            post_data["order_by"] = order_by;
            post_data["order"] = order;
            post_data["limit"] = limit.ToString();

            string uri = "https://www.scoreoid.com/api/getBestScores";

            return await ScoreoidPost<scores>(uri, post_data);
        }

        #region POST

        private async Task<T> ScoreoidPost<T>(string uri, Dictionary<string, string> post_data) 
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(await ScoreoidPostAsStream(uri, post_data));
        }

        private async Task<Stream> ScoreoidPostAsStream(string uri, Dictionary<string, string> post_data)
        {
            HttpClient client = new HttpClient();

            var responseMessage = await client.PostAsync(uri, new FormUrlEncodedContent(post_data));

            var error = ErrorRegex.Match(await responseMessage.Content.ReadAsStringAsync());
            if (error.Success)
            {
                throw new ScoreoidException(error.Value.TrimResponse("error"));
            }

            return await responseMessage.Content.ReadAsStreamAsync();
        }

        private async Task<string> ScoreoidPostAsString(string uri, Dictionary<string, string> post_data)
        {
            HttpClient client = new HttpClient();

            var responseMessage = await client.PostAsync(uri, new FormUrlEncodedContent(post_data));
            var responseMessageContent = await responseMessage.Content.ReadAsStringAsync();
        
            var errorRegex = ErrorRegex.Match(responseMessageContent);
            if (errorRegex.Success)
            {
                throw new ScoreoidException(errorRegex.Value.TrimResponse("error"));
            }

            return responseMessageContent;
        }

        #endregion
    }
}
