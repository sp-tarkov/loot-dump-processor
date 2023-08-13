﻿using System.Text.Json.Serialization;
using LootDumpProcessor.Process.Processor;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model
{
    public class GroupPosition : ICloneable
    {
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonProperty("Weight", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Weight")]
        public int? Weight { get; set; }

        [JsonProperty("Position", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Position")]
        public Vector3? Position { get; set; }

        [JsonProperty("Rotation", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Rotation")]
        public Vector3? Rotation { get; set; }

        public object Clone()
        {
            return new GroupPosition
            {
                Name = this.Name,
                Weight = this.Weight,
                Position = ProcessorUtil.Copy(this.Position),
                Rotation = ProcessorUtil.Copy(this.Rotation)
            };
        }
    }
}