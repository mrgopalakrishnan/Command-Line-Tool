using AutoMoq;
using CommandLineTool.Attributes;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace Unittests.Attributes
{
    public class ParamArgumentAttributeTests
    {
        [Fact]
        public void TestConstructorWithNothing()
        {
            ParamArgumentAttribute paramArgumentAttribute = new();
            paramArgumentAttribute.Description.Should().BeNull();
        }
        [Fact]
        public void TestConstructorWithDescription()
        {
            ParamArgumentAttribute paramArgumentAttribute = new("mydescription");
            paramArgumentAttribute.Description.Should().Be("mydescription");
        }
    }
}
