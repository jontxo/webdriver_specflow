// <copyright file="DragAndDropSteps.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Specflow.Steps
{
    using Automation.Pages;
    using BoDi;
    using TechTalk.SpecFlow;

    /// <summary>
    /// The drag and drop steps.
    /// </summary>
    [Binding]
    public class DragAndDropSteps : Steps
    {
        /// <summary>
        /// The drag and drop page.
        /// </summary>
        private readonly DragAndDropPage dragAndDropPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="DragAndDropSteps"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public DragAndDropSteps(IObjectContainer objectContainer)
        {
            this.dragAndDropPage = objectContainer.Resolve<DragAndDropPage>();
        }

        /// <summary>
        /// The then the result should be that the first element is moved near the second element.
        /// </summary>
        [StepDefinition("the result should be that the first element is moved near the second element")]
        public static void ThenTheResultShouldBeThatTheFirstElementIsMovedNearTheSecondElement()
        {
        }

        /// <summary>
        /// The then the result should be that the element has changed his position.
        /// </summary>
        [StepDefinition("the result should be that the element has changed his position")]
        public static void ThenTheResultShouldBeThatTheElementHasChangedHisPosition()
        {
        }

        /// <summary>
        /// The given i click on the DRAGGABLE and sortable button.
        /// </summary>
        [StepDefinition("I Click on the draggable and sortable button")]
        public void GivenIClickOnTheDraggableAndSortableButton()
        {
            this.dragAndDropPage.ClickOnDraggableAndSortableButton();
        }

        /// <summary>
        /// Whens the i drag web element and drop over web element in the page.
        /// </summary>
        /// <param name="webElementToBeMovedName">Name of the web element to be moved.</param>
        /// <param name="targetWebElementName">Name of the target web element.</param>
        [StepDefinition("I drag webElement (.*) and drop over webElement (.*)")]
        public void WhenIDragWebElementAndDropOverWebElementInThePage(string webElementToBeMovedName, string targetWebElementName)
        {
            this.dragAndDropPage.DragElementAndDropToAnotherElement(webElementToBeMovedName, targetWebElementName);
        }

        /// <summary>
        /// The when i drag web element and drop at the coordinates x e y in the page drag and drop page.
        /// </summary>
        /// <param name="webElementName">The web element name.</param>
        /// <param name="positionX">The position x.</param>
        /// <param name="positionY">The position y.</param>
        [StepDefinition("I drag webElement (.*) and drop ot the coordinates: x: (.*), y:(.*)")]
        public void WhenIDragWebElementAndDropOtTheCoordinatesXy(string webElementName, int positionX, int positionY)
        {
            this.dragAndDropPage.DragAndDropToOffset(webElementName, positionX, positionY);
        }
    }
}