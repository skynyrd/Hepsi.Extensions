﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Hepsi.Extensions.IQueryableExtensions;
using Hepsi.Extensions.IQueryableExtensions.Ordering;
using NUnit.Framework;


namespace Hepsi.Extensions.UnitTests.IQueryableExtensions.When_ordering_an_iqueryable
{
    class with_direction_and_property_name_instead_of_order_by_request
    {
        [Test]
        public void iqueryable_should_be_sorted_correctly()
        {
            var now = DateTime.Now;

            var source = new[]
            {
                new DummyDocument
                {
                    Name = "Middle",
                    CreatedAt = now
                },
                new DummyDocument
                {
                    Name = "Last",
                    CreatedAt = now.AddHours(1)
                },
                new DummyDocument
                {
                    Name = "First",
                    CreatedAt = now.AddHours(-1)
                }
            }
            .AsQueryable();

            var sortedData = source.DynamicOrderBy(
                propertyName: "createdAt",
                direction: "asceNding");
            var expectedSortedData = source.OrderBy(document => document.CreatedAt);

            sortedData.SequenceEqual(expectedSortedData).Should().BeTrue();
        }
    }
}
