﻿#region Using directives
using BasicTestApp.Client;
using Blazorise.UnitTests.Infrastructure;
using Blazorise.UnitTests.Infrastructure.ServerFixtures;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Abstractions;
using DevHostServerProgram = BasicTestApp.Server.Program;
#endregion

namespace Blazorise.UnitTests
{
    public class SelectEditNavigationTest : BasicTestAppTestBase
    {
        public SelectEditNavigationTest( BrowserFixture browserFixture,
            ToggleExecutionModeServerFixture<DevHostServerProgram> serverFixture,
            ITestOutputHelper output )
            : base( browserFixture, serverFixture, output )
        {
            Navigate( ServerPathBase, noReload: !serverFixture.UsingAspNetHost );
            MountTestComponent<SelectEditNavigationsComponent>();
        }

        [Fact]
        public void TestNavigation()
        {
            var paragraph = Browser.FindElement( By.Id( "select-value-initialy-selected" ) );
            var select = new SelectElement( paragraph.FindElement( By.TagName( "select" ) ) );
            var result = paragraph.FindElement( By.Id( "select-value-initialy-selected-result" ) );
            var btnOne = paragraph.FindElement( By.ClassName( "btn-primary" ) );
            var btnTwo = paragraph.FindElement( By.ClassName( "btn-secondary" ) );

            WaitAssert.Equal( "two", () => select.SelectedOption.GetAttribute( "value" ) );
            WaitAssert.Equal( "two", () => result.Text );

            select.SelectByIndex( 0 );
            WaitAssert.Equal( "one", () => select.SelectedOption.GetAttribute( "value" ) );
            WaitAssert.Equal( "one", () => result.Text );

            btnOne.Click();
            WaitAssert.Equal( "one", () => select.SelectedOption.GetAttribute( "value" ) );
            WaitAssert.Equal( "one", () => result.Text );

            select.SelectByIndex( 1 );
            WaitAssert.Equal( "two", () => select.SelectedOption.GetAttribute( "value" ) );
            WaitAssert.Equal( "two", () => result.Text );

            btnTwo.Click();
            WaitAssert.Equal( "two", () => select.SelectedOption.GetAttribute( "value" ) );
            WaitAssert.Equal( "two", () => result.Text );

            btnOne.Click();
            WaitAssert.Equal( "one", () => select.SelectedOption.GetAttribute( "value" ) );
            WaitAssert.Equal( "one", () => result.Text );

            btnTwo.Click();
            WaitAssert.Equal( "two", () => select.SelectedOption.GetAttribute( "value" ) );
            WaitAssert.Equal( "two", () => result.Text );
        }
    }
}
