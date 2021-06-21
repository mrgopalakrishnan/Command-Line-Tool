using AutoMoq;
using CommandLineTool.Attributes;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace Unittests.Attributes
{
    public class ParamOptionAttributeTests
    {
        [Fact]
        public void TestCtorWithAliases()
        {
            ParamOptionAttribute paramOption = new("aliases");
            paramOption.Aliases.Should().Contain("aliases");
            paramOption.Description.Should().BeNull();
            paramOption.IsMandatory.Should().Be(true);
        }
        [Fact]
        public void TestCtorWithAliases_Description()
        {
            ParamOptionAttribute paramOption = new("aliases","mydescription");
            paramOption.Aliases.Should().Contain("aliases");
            paramOption.Description.Should().Be("mydescription");
            paramOption.IsMandatory.Should().Be(true);
        }
        [Fact]
        public void TestCtorWithAliases_Description_mandatory()
        {
            ParamOptionAttribute paramOption = new("aliases", "mydescription",false);
            paramOption.Aliases.Should().Contain("aliases");
            paramOption.Description.Should().Be("mydescription");
            paramOption.IsMandatory.Should().Be(false);
        }
    }
}
