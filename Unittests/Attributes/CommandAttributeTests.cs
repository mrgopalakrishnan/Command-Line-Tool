using AutoMoq;
using CommandLineTool.Attributes;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace Unittests.Attributes
{
    public class CommandAttributeTests
    {
        [Fact]
        public void TestConstructorWithName()
        {
            CommandAttribute cmdAttr = new("myname");
            cmdAttr.Name.Should().Be("myname");
            cmdAttr.Description.Should().BeNull();
        }
        [Fact]
        public void TestConstructorWithNameAndDescription()
        {
            CommandAttribute cmdAttr = new("myname","mydescription");
            cmdAttr.Name.Should().Be("myname");
            cmdAttr.Description.Should().Be("mydescription");
        }
    }
}
