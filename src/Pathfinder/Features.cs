using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using Pathfinder.Utilities;

namespace Pathfinder
{
	internal class FeatureStore {
		private readonly Dictionary<string, bool> _features;
		public FeatureStore()
		{
			_features = new Dictionary<string, bool>();
			var fromConfig = ConfigurationManager.GetSection(nameof(Features)?.ToLower()) as Hashtable;
			if (fromConfig == null)
			{
				return;
			}

			foreach (DictionaryEntry feature in fromConfig)
			{
				var key = feature.Key.ToString();
				var value = feature.Value.ToString();
				LogTo.Debug($"Found Feature '{key}': {value}");

				_features[key] = bool.TryParse(value, out var enabled) && enabled;
			}
		}

		public bool this[string pFeatureName]
        {
            get => _features.TryGetValue(pFeatureName, out var enabled) && enabled;
            internal set => _features[pFeatureName] = value;
        }
    }

	public static class Features
	{
		private static readonly Lazy<FeatureStore> _instance = new Lazy<FeatureStore>(() => new FeatureStore());
		private static FeatureStore Instance => _instance.Value;

		public static bool LogQueries
        {
            get => Instance[nameof(LogQueries)];
            internal set => Instance[nameof(LogQueries)] = value;
        }
    }
}
