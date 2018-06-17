using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using NUnit.Framework;
using Pathfinder.Api.Controllers;
using Pathfinder.Enums;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Api.Tests.Spells
{
    [TestFixture]
    public class SpellTests
    {
        [SetUp]
        public void SetUp()
        {
            Features.LogQueries = true;
            LogTo.ChangeLogLevel("Debug");
        }

        [Test]
        public void FetchesExpectedForSearchText()
        {
            var spellBookController = new SpellBookController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var requested = new SearchCriteria {SearchText = "Acid Arrow"};
            var response = spellBookController.Search(requested);

            Assert.That(response.SearchText, Is.EqualTo(requested.SearchText));
            Assert.That(response.Facets.Count(), Is.EqualTo(2));
            Assert.That(response.Count, Is.EqualTo(1));
            Assert.That(response.Results.Count(), Is.EqualTo(1));
            Assert.That(response.Results.Select(x => x.Name).First(), Is.EqualTo("Acid Arrow"));
        }


        [Test]
        public void FetchesExpectedForFacet()
        {
            var spellBookController = new SpellBookController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var requested = new SearchCriteria
            {
                Chips = new List<SearchChip>
                {
                    new SearchChip
                    {
                        Name = "Magic School",
                        Value = $"{MagicSchool.Universal}"
                    }
                }
            };
            var response = spellBookController.Search(requested);

            Assert.That(response.SearchText, Is.EqualTo(requested.SearchText));
            Assert.That(response.Facets.Count(), Is.EqualTo(2));
            Assert.That(response.Count, Is.EqualTo(5));
            Assert.That(response.Results.Count(), Is.EqualTo(5));
        }
    }
}
