using System.Web.Http;
using Pathfinder.Serializers.Json;

namespace Pathfinder.Api
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);

			var formatters = GlobalConfiguration.Configuration.Formatters;
			var jsonFormatter = formatters.JsonFormatter;

			var libraryFactory = new LibraryFactory();
			
			var raceLibrary = libraryFactory.GetRaceLibrary();
			var skillLibrary = libraryFactory.GetSkillLibrary();
			var classLibrary = libraryFactory.GetClassLibrary();

			var characterJsonSerializer = 
				new CharacterJsonSerializer(
					raceLibrary,
					skillLibrary,
					classLibrary);
			jsonFormatter.SerializerSettings.Converters.Add(characterJsonSerializer);
		}
	}
}
