using System;
using System.Linq;
using FluentAssertions;
using Hepsi.Extensions.IQueryableExtensions;
using Hepsi.Extensions.IQueryableExtensions.Ordering;
using NUnit.Framework;


namespace Hepsi.Extensions.UnitTests.IQueryableExtensions.When_ordering_an_iqueryable
{
    [TestFixture]
    public class ascendingly_and_then_by_another_property
    {
        [Test]
        public void iquerable_should_be_sorted_correctly()
        {
            var now = DateTime.Now;

            var source = new[]
            {
                new DummyDocument
                {
                    Name = "Middle",
                    CreatedAt = now,
                    Count = 2
                },
                 new DummyDocument
                {
                    Name = "Middle",
                    CreatedAt = now,
                    Count = 0
                },
                new DummyDocument
                {
                    Name = "Middle-2",
                    CreatedAt = now,
                    Count = 10
                },
                new DummyDocument
                {
                    Name = "Middle",
                    CreatedAt = now,
                    Count = 1
                },
                new DummyDocument
                {
                    Name = "Last",
                    CreatedAt = now.AddHours(1),
                    Count = 20
                },
                new DummyDocument
                {
                    Name = "First",
                    CreatedAt = now.AddHours(-1),
                    Count= 0
                }
            }
            .AsQueryable();

            var orderByRequest = new OrderByRequest(
                propertyName: "createdAt",
                direction: OrderByDirection.Ascending);
            orderByRequest.AddThenByRequest("count", OrderByDirection.Ascending);

            var sortedData = source.DynamicOrderBy(orderByRequest);
            var expectedSortedData = source
                .OrderBy(document => document.CreatedAt)
                .ThenBy(document => document.Count);

            sortedData.SequenceEqual(expectedSortedData).Should().BeTrue();
        }
    }
}