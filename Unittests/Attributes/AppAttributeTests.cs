using AutoMoq;
using CommandLineTool.Attributes;
using Moq;
using System;
using Xunit;
using FluentAssertions;
namespace Unittests.Attributes
{
    public class AppAttributeTests
    {
        [Fact]
        public void TestConstructorWithDescription()
        {
            // Arrange
            AppAttribute app = new("mydescription");
            app.Description.Should().BeEquivalentTo("mydescription");
            app.BindingFlags.Should().HaveValue(((decimal)AppAttribute.DefaultBindingFlags));
        }
        [Fact]
        public void TestConstructorWithBindingFlags()
        {
            // Arrange
            AppAttribute app = new(System.Reflection.BindingFlags.Public);
            app.Description.Should().BeNull();
            app.BindingFlags.Should().HaveValue((decimal)System.Reflection.BindingFlags.Public);
        }
        [Fact]
        public void TestConstructorWithDescriptionAndBindingFlags()
        {
            // Arrange
            AppAttribute app = new("mydescription", System.Reflection.BindingFlags.Public);
            app.Description.Should().BeEquivalentTo("mydescription");
            app.BindingFlags.Should().HaveValue((decimal)System.Reflection.BindingFlags.Public);
        }
    }
}
