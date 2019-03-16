// <copyright file="DragAndDropPage.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.Pages
{
    using Automation.Reflection;
    using Automation.WebDriverExtensions;
    using Automation.WebDriverHelper;
    using OpenQA.Selenium;
    using SeleniumExtras.PageObjects;

    /// <summary>
    /// The drag and drop page.
    /// </summary>
    public class DragAndDropPage
    {
        #region webelements
#pragma warning disable CA1823,0169,0649
        /// <summary>
        /// The web element High TATRAS.
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[contains(text(),'High Tatras') and not(contains(text(),'High Tatras '))]/../img")]
        private readonly IWebElement highTatras;

        /// <summary>
        /// The web element High TATRAS.
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[contains(text(),'High Tatras 2')]")]
        private readonly IWebElement highTatras2;

        /// <summary>
        /// The web element trash.
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='trash']")]
        private readonly IWebElement trash;

        /// <summary>
        /// The drag and drop to position tab.
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='Accepted Elements']")]
        private readonly IWebElement dragAndDropToPositionTab;

        /// <summary>
        /// The DRAGGABLE and sortable.
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@class='inside_contain']/div[@id='draggable']")]
        private readonly IWebElement dragMeAround;

        /// <summary>
        /// The drag and drop frame.
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@class='demo-frame' and @src='../../demoSite/practice/droppable/photo-manager.html']")]
        private readonly IWebElement dragAndDropFrame;
#pragma warning restore 0169,0649,CA1823
        #endregion webelements

        /// <summary>
        /// The webDriver.
        /// </summary>
        private readonly IWebDriver webDriver;

        /// <summary>
        /// Initializes a new instance of the <see cref="DragAndDropPage"/> class.
        /// </summary>
        /// <param name="webDriverContext">The web driver context.</param>
        public DragAndDropPage(WebDriverContext webDriverContext)
        {
            this.webDriver = webDriverContext.WebDriver;
            PageFactory.InitElements(webDriverContext.WebDriver, this);
        }

        /// <summary>
        /// Drags the element and drop to another element.
        /// </summary>
        /// <param name="element1Name">Name of the element1.</param>
        /// <param name="element2Name">Name of the element2.</param>
        public void DragElementAndDropToAnotherElement(string element1Name, string element2Name)
        {
            this.webDriver.SwitchToFrame(this.dragAndDropFrame);
            var element1 = this.GetFieldValue<IWebElement>(element1Name);
            var element2 = this.GetFieldValue<IWebElement>(element2Name);
            element1.DragIntoElement(element2);
            this.webDriver.SwitchToMainFrame();
        }

        /// <summary>
        /// Drags the and drop to offset.
        /// </summary>
        /// <param name="webElementName">Name of the web element.</param>
        /// <param name="positionX">The position x.</param>
        /// <param name="positionY">The position y.</param>
        public void DragAndDropToOffset(string webElementName, int positionX, int positionY)
        {
            this.webDriver.SwitchToFrame(this.dragAndDropFrame);
            var element = this.GetFieldValue<IWebElement>(webElementName);
            element.DragAndDropToOffset(positionX, positionY);
            this.webDriver.SwitchToMainFrame();
        }

        /// <summary>
        /// The click on DRAGGABLE and sortable button.
        /// </summary>
        public void ClickOnDraggableAndSortableButton()
        {
            this.dragAndDropToPositionTab.Click();
        }

        /// <summary>
        /// Clicks the on accepted elements tab.
        /// </summary>
        public void ClickOnAcceptedElementsTab()
        {
            this.dragAndDropToPositionTab.Click();
        }
    }
}