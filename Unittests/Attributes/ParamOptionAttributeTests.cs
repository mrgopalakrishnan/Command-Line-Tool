using AutoMoq;
using CommandLineTool.Attributes;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
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
        public void TestCtorWithAliasesArray()
        {
            ParamOptionAttribute paramOption = new(new []{ "aliases" });
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
        public void TestCtorWithAliasesArray_Description()
        {
            ParamOptionAttribute paramOption = new(new[] { "aliases" }, "mydescription");
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
        [Fact]
        public void TestCtorWithAliasesArray_Description_mandatory()
        {
            ParamOptionAttribute paramOption = new(new[] { "aliases" }, "mydescription", false);
            paramOption.Aliases.Should().Contain("aliases");
            paramOption.Description.Should().Be("mydescription");
            paramOption.IsMandatory.Should().Be(false);
        }
        [Fact]
        public void TestCtorWithAliases_mandatory()
        {
            ParamOptionAttribute paramOption = new("aliases", false);
            paramOption.Aliases.Should().Contain("aliases");
            paramOption.Description.Should().BeNull();
            paramOption.IsMandatory.Should().Be(false);
        }
        [Fact]
        public void TestCtorWithAliasesArray_mandatory()
        {
            ParamOptionAttribute paramOption = new(new[] { "aliases" }, false);
            paramOption.Aliases.Should().Contain("aliases");
            paramOption.Description.Should().BeNull();
            paramOption.IsMandatory.Should().Be(false);
        }
    }
}
