﻿using OpenQA.Selenium;
using PIMS.Tests.Automation.Classes;

namespace PIMS.Tests.Automation.PageObjects
{
    public class SharedFileProperties : PageObjectBase
    {
        //Search Properties Section Elements
        private readonly By searchSectionTitle = By.XPath("//div[contains(text(),'Properties to include in this file')]");
        private readonly By searchSectionSubfileTitle = By.XPath("//div[contains(text(),'Properties to include in this sub-file')]");
        private readonly By searchSectionInstructions = By.XPath("//div[contains(text(),'Properties to include in this file')]/parent::div/parent::h2/following-sibling::div/div[1]");
        private readonly By searchSectionSubfileInstructions = By.XPath("//div[contains(text(),'Properties to include in this sub-file')]/parent::div/parent::h2/following-sibling::div/div[1]");

        //Locate on Map Elements
        private readonly By locateOnMapTab = By.XPath("//a[contains(text(),'Locate on Map')]");
        private readonly By locateOnMapSubtitle = By.XPath("//h3[contains(text(), 'Select a property')]");
        private readonly By locateOnMapBlueIcon = By.Id("Layer_2");
        private readonly By locateOnMapInstuction1 = By.XPath("//li[contains(text(),'Single-click blue marker above')]");
        private readonly By locateOnMapInstuction2 = By.XPath("//li[contains(text(),'Mouse to a parcel on the map')]");
        private readonly By locateOnMapInstuction3 = By.XPath("//li[contains(text(),'Single-click on parcel to select it')]");
        private readonly By locateOnMapSelectedLabel = By.XPath("//div[contains(text(),'Selected property attributes')]");
        private readonly By locateOnMapPIDLabel = By.XPath("//label[contains(text(),'PID')]");
        private readonly By locateOnMapPlanLabel = By.XPath("//label[contains(text(),'Plan #')]");
        private readonly By locateOnMapAddressLabel = By.XPath("//label[contains(text(),'Address')]");
        private readonly By locateOnMapRegionLabel = By.XPath("//label[contains(text(),'Region')]");
        private readonly By locateOnMapDistrictLabel = By.XPath("//label[contains(text(),'District')]");
        private readonly By locateOnMapLegalDescriptionLabel = By.XPath("//label[contains(text(),'Legal Description')]");

        //Search Tab Elements
        private readonly By searchByTab = By.XPath("//a[contains(text(),'Search')]");
        private readonly By searchBySubtitle = By.XPath("//h2/div/div[contains(text(), 'Search for a property')]");
        private readonly By searchBySelect = By.Id("input-searchBy");
        private readonly By searchByPIDInput = By.Id("input-pid");
        private readonly By searchByPINInput = By.Id("input-pin");
        private readonly By searchByAddressInput = By.Id("input-address");
        private readonly By searchByAddressInputSuggestionList = By.CssSelector("div[class='suggestionList']");
        private readonly By searchByAddressSuggestion1stOption = By.CssSelector("div[class='suggestionList'] option:nth-child(1)");
        private readonly By searchByPlanInput = By.Id("input-planNumber");
        private readonly By searchByLegalDescriptionInput = By.Id("input-legalDescription");
        private readonly By searchByLatDegreesInput = By.Id("number-input-coordinates.latitude.degrees");
        private readonly By searchByLatMinsInput = By.Id("number-input-coordinates.latitude.minutes");
        private readonly By searchByLatSecsInput = By.Id("number-input-coordinates.latitude.seconds");
        private readonly By searchByLatDirectionSelect = By.Id("input-coordinates.latitude.direction");
        private readonly By searchByLongDegreesInput = By.Id("number-input-coordinates.longitude.degrees");
        private readonly By searchByLongMinsInput = By.Id("number-input-coordinates.longitude.minutes");
        private readonly By searchByLongSecsInput = By.Id("number-input-coordinates.longitude.seconds");
        private readonly By searchByLongDirectionSelect = By.Id("input-coordinates.longitude.direction");
        private readonly By searchByButton = By.Id("search-button");
        private readonly By searchResetButton = By.Id("reset-button");

        //Search Results Elements
        private readonly By searchPropertiesResultTableHeader = By.CssSelector("div[class='thead thead-light']");
        private readonly By searchPropsResultSelectAllInput = By.CssSelector("input[data-testid='selectrow-parent']");
        private readonly By searchPropResultsPIDHeader = By.XPath("//div[@class='th']/div[contains(text(), 'PID')]");
        private readonly By searchPropResultsPINHeader = By.XPath("//div[@class='th']/div[contains(text(), 'PIN')]");
        private readonly By searchPropResultsPlanHeader = By.XPath("//div[@class='th']/div[contains(text(), 'Plan #')]");
        private readonly By searchPropResultsAddressHeader = By.XPath("//div[@class='th']/div[contains(text(), 'Address')]");

        private readonly By searchPropertiesNoRowsResult = By.CssSelector("div[data-testid='map-properties'] div[class='no-rows-message']");
        private readonly By searchProperties1stResultAddressOptions = By.CssSelector("div[data-testid='map-properties'] div[class='tbody'] div[class='tr-wrapper']:nth-child(1)");
        private readonly By searchProperties1stResultPropCheckbox = By.CssSelector("div[data-testid='map-properties'] div[class='tbody'] div[class='tr-wrapper']:nth-child(1) div[class='td']:nth-child(1) input");

        private readonly By searchPropertiesAddSelectionBttn = By.XPath("//button/div[contains(text(),'Add to selection')]");

        //Selected Properties Elements
        private readonly By searchPropertiesSelectedPropertiesSubtitle = By.XPath("//div[contains(text(),'Selected properties')]");
        private readonly By searchPropertiesSelectedIdentifierHeader = By.XPath("//div[@class='collapse show']/div/div[contains(text(),'Identifier')]");
        private readonly By searchPropertiesSelectedDescriptiveNameHeader = By.XPath("//div[@class='collapse show']/div/div[contains(text(),'Provide a descriptive name for this land')]");
        private readonly By searchPropertiesSelectedToolTipIcon = By.CssSelector("span[data-testid='tooltip-icon-property-selector-tooltip']");
        private readonly By searchPropertiesSelectedDefault = By.XPath("//span[contains(text(),'No Properties selected')]");
        private readonly By searchPropertiesPropertiesInFileTotal = By.CssSelector("div[class='align-items-center mb-3 no-gutters row']");
        private readonly By searchPropertiesPropertiesInLeaseTotal = By.CssSelector("div[class='align-items-center my-3 no-gutters row']");

        //File - Edit Properties button
        private readonly By fileEditPropertiesBttn = By.CssSelector("button[title='Change properties']");

        //File - Properties Elements
        private readonly By acquisitionProperty1stPropLink = By.CssSelector("div[data-testid='menu-item-row-1'] div:nth-child(3)");

        //File Confirmation Modal Elements
        private readonly By propertiesFileConfirmationModal = By.CssSelector("div[class='modal-content']");

        //Toast Element
        private readonly By duplicatePropToast = By.CssSelector("div[id='duplicate-property'] div[class='Toastify__toast-body']");

        private SharedModals sharedModals;

        public SharedFileProperties(IWebDriver webDriver) : base(webDriver)
        {
            sharedModals = new SharedModals(webDriver);
        }

        public void NavigateToSearchTab()
        {
            WaitUntilClickable(searchByTab);
            FocusAndClick(searchByTab);
        }

        public void SelectPropertyByPID(string PID)
        {
            WaitUntilClickable(searchBySelect);
            ChooseSpecificSelectOption(searchBySelect, "PID");

            WaitUntilVisible(searchByPIDInput);
            if (webDriver.FindElement(searchByPIDInput).GetDomProperty("value") != "")
            {
                ClearInput(searchByPIDInput);
            }
            webDriver.FindElement(searchByPIDInput).SendKeys(PID);

            FocusAndClick(searchByButton);
        }

        public void SelectPropertyByPIN(string PIN)
        {
            WaitUntilClickable(searchBySelect);
            ChooseSpecificSelectOption(searchBySelect, "PIN");

            WaitUntilVisible(searchByPINInput);
            if (webDriver.FindElement(searchByPINInput).GetDomProperty("value") != "")
            {
                ClearInput(searchByPINInput);
            }
            webDriver.FindElement(searchByPINInput).SendKeys(PIN);

            FocusAndClick(searchByButton);
        }

        public void SelectPropertyByAddress(string address)
        {
            Wait();
            ChooseSpecificSelectOption(searchBySelect, "Address");

            WaitUntilVisible(searchByAddressInput);
            if (webDriver.FindElement(searchByAddressInput).GetDomProperty("value") != "")
            {
                ClearInput(searchByAddressInput);
            }
            webDriver.FindElement(searchByAddressInput).SendKeys(address);

            WaitUntilClickable(searchByAddressInputSuggestionList);
            FocusAndClick(searchByAddressSuggestion1stOption);
        }

        public void SelectPropertyByPlan(string plan)
        {
            Wait();
            ChooseSpecificSelectOption(searchBySelect, "Plan #");

            WaitUntilVisible(searchByPlanInput);
            if (webDriver.FindElement(searchByPlanInput).GetDomProperty("value") != "")
            {
                ClearInput(searchByPlanInput);
            }
            webDriver.FindElement(searchByPlanInput).SendKeys(plan);

            FocusAndClick(searchByButton);
        }

        public void SelectPropertyByLegalDescription(string legalDescription)
        {
            Wait();
            ChooseSpecificSelectOption(searchBySelect, "Legal Description");

            WaitUntilVisible(searchByLegalDescriptionInput);
            if (webDriver.FindElement(searchByLegalDescriptionInput).GetDomProperty("value") != "")
                ClearInput(searchByLegalDescriptionInput);

            webDriver.FindElement(searchByLegalDescriptionInput).SendKeys(legalDescription);

            FocusAndClick(searchByButton);
            Wait(5000);
        }

        public void SelectPropertyByLongLant(PropertyLatitudeLongitude coordinates)
        {
            Wait();
            ChooseSpecificSelectOption(searchBySelect, "Lat/Long");

            WaitUntilVisible(searchByLatDegreesInput);

            if (webDriver.FindElement(searchByLatDegreesInput).GetDomProperty("value") != "")
                ClearInput(searchByLatDegreesInput);

            if (webDriver.FindElement(searchByLatMinsInput).GetDomProperty("value") != "")
                ClearInput(searchByLatMinsInput);

            if (webDriver.FindElement(searchByLatSecsInput).GetDomProperty("value") != "")
                ClearInput(searchByLatSecsInput);

            if (webDriver.FindElement(searchByLongDegreesInput).GetDomProperty("value") != "")
                ClearInput(searchByLongDegreesInput);

            if (webDriver.FindElement(searchByLongMinsInput).GetDomProperty("value") != "")
                ClearInput(searchByLongMinsInput);

            if (webDriver.FindElement(searchByLongSecsInput).GetDomProperty("value") != "")
                ClearInput(searchByLongSecsInput);

            webDriver.FindElement(searchByLatDegreesInput).SendKeys(coordinates.LatitudeDegree);
            webDriver.FindElement(searchByLatMinsInput).SendKeys(coordinates.LatitudeMinutes);
            webDriver.FindElement(searchByLatSecsInput).SendKeys(coordinates.LatitudeSeconds);
            webDriver.FindElement(searchByLatDirectionSelect).SendKeys(coordinates.LatitudeDirection);

            webDriver.FindElement(searchByLongDegreesInput).SendKeys(coordinates.LongitudeDegree);
            webDriver.FindElement(searchByLongMinsInput).SendKeys(coordinates.LongitudeMinutes);
            webDriver.FindElement(searchByLongSecsInput).SendKeys(coordinates.LongitudeSeconds);
            webDriver.FindElement(searchByLongDirectionSelect).SendKeys(coordinates.LongitudeDirection);

            FocusAndClick(searchByButton);
            Wait(5000);
        }

        public void AddNameSelectedProperty(string name, int index)
        {
            WaitUntilVisible(By.Id("input-properties." + index + ".name"));
            webDriver.FindElement(By.Id("input-properties." + index + ".name")).SendKeys(name);
        }

        public void SelectFirstOptionFromSearch()
        {
            Wait();
            FocusAndClick(searchProperties1stResultPropCheckbox);

            webDriver.FindElement(searchPropertiesAddSelectionBttn).Click();

            Wait();
            while (webDriver.FindElements(propertiesFileConfirmationModal).Count > 0)
            {
                if (sharedModals.ModalContent().Contains("This property has already been added to one or more acquisition files."))
                {
                    Assert.Equal("User Override Required", sharedModals.ModalHeader());
                    Assert.Contains("This property has already been added to one or more acquisition files.", sharedModals.ModalContent());
                    Assert.Contains("Do you want to acknowledge and proceed?", sharedModals.ModalContent());
                }
                if (sharedModals.ModalContent().Contains("This property has already been added to one or more research files."))
                {
                    Assert.Equal("User Override Required", sharedModals.ModalHeader());
                    Assert.Contains("This property has already been added to one or more research files.", sharedModals.ModalContent());
                    Assert.Contains("Do you want to acknowledge and proceed?", sharedModals.ModalContent());
                }
                if (sharedModals.ModalContent().Contains("This property has already been added to one or more disposition files."))
                {
                    Assert.Equal("User Override Required", sharedModals.ModalHeader());
                    Assert.Contains("This property has already been added to one or more disposition files.", sharedModals.ModalContent());
                    Assert.Contains("Do you want to acknowledge and proceed?", sharedModals.ModalContent());
                }

                if (sharedModals.ModalContent().Contains("This property has already been added to one or more files."))
                {
                    Assert.Equal("User Override Required", sharedModals.ModalHeader());
                    Assert.Contains("This property has already been added to one or more files.", sharedModals.ModalContent());
                    Assert.Contains("Do you want to acknowledge and proceed?", sharedModals.ModalContent());
                }

                if (sharedModals.ModalContent().Contains("You have selected a property not previously in the inventory."))
                {
                    Assert.Equal("Not inventory property", sharedModals.ModalHeader());
                    Assert.Contains("You have selected a property not previously in the inventory. Do you want to add this property to the lease?", sharedModals.ModalContent());
                }

                sharedModals.ModalClickOKBttn();
                Wait();
            }
        }

        public string noRowsResultsMessageFromSearch()
        {
            WaitUntilDisappear(tableLoadingSpinner);
            return webDriver.FindElement(searchPropertiesNoRowsResult).Text;
        }

        public void VerifyLocateOnMapFeature(string fileType)
        {
            Wait(2000);

            if (fileType == "Subfile")
            {
                AssertTrueIsDisplayed(searchSectionSubfileTitle);
                AssertTrueIsDisplayed(searchSectionSubfileInstructions);
            }
            else
            {
                AssertTrueIsDisplayed(searchSectionTitle);
                AssertTrueIsDisplayed(searchSectionInstructions);
            }

            AssertTrueIsDisplayed(locateOnMapTab);
            AssertTrueIsDisplayed(searchByTab);

            AssertTrueIsDisplayed(locateOnMapSubtitle);
            AssertTrueIsDisplayed(locateOnMapBlueIcon);
            AssertTrueIsDisplayed(locateOnMapInstuction1);
            AssertTrueIsDisplayed(locateOnMapInstuction2);
            AssertTrueIsDisplayed(locateOnMapInstuction3);
            AssertTrueIsDisplayed(locateOnMapSelectedLabel);
            AssertTrueIsDisplayed(locateOnMapPIDLabel);
            AssertTrueIsDisplayed(locateOnMapPlanLabel);
            AssertTrueIsDisplayed(locateOnMapAddressLabel);
            AssertTrueIsDisplayed(locateOnMapRegionLabel);
            AssertTrueIsDisplayed(locateOnMapDistrictLabel);
            AssertTrueIsDisplayed(locateOnMapLegalDescriptionLabel);

            AssertTrueIsDisplayed(searchPropertiesSelectedPropertiesSubtitle);
            AssertTrueIsDisplayed(searchPropertiesSelectedIdentifierHeader);
            AssertTrueIsDisplayed(searchPropertiesSelectedDescriptiveNameHeader);
            AssertTrueIsDisplayed(searchPropertiesSelectedToolTipIcon);

            if (webDriver.FindElements(searchPropertiesSelectedDefault).Count > 0)
            {
                WaitUntilVisible(searchPropertiesSelectedDefault);
                AssertTrueIsDisplayed(searchPropertiesSelectedDefault);
            }
        }

        public void VerifySearchPropertiesFeature()
        {
            WaitUntilSpinnerDisappear();

            AssertTrueIsDisplayed(searchByTab);
            AssertTrueIsDisplayed(searchBySubtitle);
            AssertTrueIsDisplayed(searchBySelect);
            AssertTrueIsDisplayed(searchByPIDInput);
            AssertTrueIsDisplayed(searchByButton);
            AssertTrueIsDisplayed(searchResetButton);
            AssertTrueIsDisplayed(searchPropertiesResultTableHeader);
            AssertTrueIsDisplayed(searchPropsResultSelectAllInput);
            AssertTrueIsDisplayed(searchPropResultsPIDHeader);
            AssertTrueIsDisplayed(searchPropResultsPINHeader);
            AssertTrueIsDisplayed(searchPropResultsPlanHeader);
            AssertTrueIsDisplayed(searchPropResultsAddressHeader);
        }

        public void NavigateToAddPropertiesToFile()
        {
            Wait();
            webDriver.FindElement(fileEditPropertiesBttn).Click();
        }

        public void SelectFirstPropertyOptionFromFile()
        {
            WaitUntilClickable(acquisitionProperty1stPropLink);
            webDriver.FindElement(acquisitionProperty1stPropLink).Click();
        }

        public void SelectNthPropertyOptionFromFile(int index)
        {
            var elementOrder = index++;
            By chosenProperty = By.CssSelector("div[data-testid='menu-item-row-" + elementOrder + "'] div:nth-child(3)");

            WaitUntilClickable(chosenProperty);
            webDriver.FindElement(chosenProperty).Click();
        }

        public void ResetSearch()
        {
            Wait();
            webDriver.FindElement(searchResetButton).Click();
        }

        public void DeleteLastPropertyFromFile()
        {
            Wait();
            var propertyIndex = webDriver.FindElements(searchPropertiesPropertiesInFileTotal).Count();

            WaitUntilClickable(By.XPath("//h2/div/div[contains(text(),'Selected properties')]/parent::div/parent::h2/following-sibling::div/div[@class='align-items-center mb-3 no-gutters row'][" + propertyIndex + "]/div[4]/button"));
            webDriver.FindElement(By.XPath("//h2/div/div[contains(text(),'Selected properties')]/parent::div/parent::h2/following-sibling::div/div[@class='align-items-center mb-3 no-gutters row'][" + propertyIndex + "]/div[4]/button")).Click();

            Wait();
            if (webDriver.FindElements(propertiesFileConfirmationModal).Count > 0)
            {
                Assert.True(sharedModals.ModalHeader() == "Removing Property from form");
                Assert.True(sharedModals.ModalContent() == "Are you sure you want to remove this property from this lease/license?");

                sharedModals.ModalClickOKBttn();
            }

            Wait();
            var propertiesAfterRemove = webDriver.FindElements(searchPropertiesPropertiesInFileTotal).Count();
            Assert.True(propertiesAfterRemove == propertyIndex - 1);

        }

        public void DeleteLastPropertyFromLease()
        {
            Wait();
            var propertyIndex = webDriver.FindElements(searchPropertiesPropertiesInLeaseTotal).Count();

            WaitUntilClickable(By.XPath("//h2/div/div[contains(text(),'Selected properties')]/parent::div/parent::h2/following-sibling::div/div[@class='align-items-center my-3 no-gutters row'][" + propertyIndex + "]/div[4]/button"));
            webDriver.FindElement(By.XPath("//h2/div/div[contains(text(),'Selected properties')]/parent::div/parent::h2/following-sibling::div/div[@class='align-items-center my-3 no-gutters row'][" + propertyIndex + "]/div[4]/button")).Click();

            Wait();
            if (webDriver.FindElements(propertiesFileConfirmationModal).Count > 0)
            {
                Assert.True(sharedModals.ModalHeader() == "Removing Property from Lease/Licence");
                Assert.True(sharedModals.ModalContent() == "Are you sure you want to remove this property from this lease/licence?");

                sharedModals.ModalClickOKBttn();
            }

            Wait();
            var propertiesAfterRemove = webDriver.FindElements(searchPropertiesPropertiesInLeaseTotal).Count();
            Assert.True(propertiesAfterRemove == propertyIndex - 1);

        }

        public void SaveFileProperties()
        {
            Wait();
            ButtonElement("Save");

            Assert.Equal("Confirm changes", sharedModals.ModalHeader());
            Assert.Equal("You have made changes to the properties in this file.", sharedModals.ConfirmationModalText1());
            Assert.Equal("Do you want to save these changes?", sharedModals.ConfirmationModalText2());

            sharedModals.ModalClickOKBttn();

            Wait();
            while (webDriver.FindElements(propertiesFileConfirmationModal).Count() > 0)
            {

                if (sharedModals.SecondaryModalContent().Contains("You have added one or more properties to the disposition file that are not in the MOTI Inventory"))
                {
                    Assert.Equal("User Override Required", sharedModals.SecondaryModalHeader());
                    Assert.Contains("You have added one or more properties to the disposition file that are not in the MOTI Inventory. Do you want to proceed?", sharedModals.SecondaryModalContent());
                }
                else
                {
                    Assert.Equal("User Override Required", sharedModals.SecondaryModalHeader());
                    Assert.Contains("The selected property already exists in the system's inventory. However, the record is missing spatial details.", sharedModals.SecondaryModalContent());
                    Assert.Contains("To add the property, the spatial details for this property will need to be updated. The system will attempt to update the property record with spatial information from the current selection.", sharedModals.SecondaryModalContent());
                }
                sharedModals.SecondaryModalClickOKBttn();
                Wait();
            }
        }
    }
}
