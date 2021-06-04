using Newtonsoft.Json;
using System;

namespace TakeRepositoriesApi.Model
{
    public class CodeRepository
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }
        public string Description { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        public string Language { get; set; }
    }
}