using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Moq;
using NUnit.Framework;
using Pathfinder.Api.Controllers;
using Pathfinder.Api.Searching;
using Pathfinder.Enums;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Test.ObjectMothers;
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

        private IRepository<ISpell> SpellRepository
        {
            get
            {
                var testClass = SpellMother.AcidArrow();
                var mockClassLibrary = new Mock<IRepository<ISpell>>();
                mockClassLibrary.Setup(foo => foo.GetAll()).Returns(new List<ISpell> {testClass});
                mockClassLibrary.Setup(foo => foo.GetQueryable()).Returns(new List<ISpell> {testClass}.AsQueryable());
                return mockClassLibrary.Object;
            }
        }

        [Test]
        public void FetchesExpectedForSearchText()
        {
            var spellBookController = new SpellBookController(SpellRepository)
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
            var spellBookController = new SpellBookController(SpellRepository)
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
                        Value = $"{MagicSchool.Conjuration}"
                    }
                }
            };
            var response = spellBookController.Search(requested);

            Assert.That(response.SearchText, Is.EqualTo(requested.SearchText));
            Assert.That(response.Facets.Count(), Is.EqualTo(2));
            Assert.That(response.Count, Is.EqualTo(1));
            Assert.That(response.Results.Count(), Is.EqualTo(1));
        }
    }
}
