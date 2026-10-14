Feature: Automation Form

@AutomationForm
Scenario: Fill Automation Form
    Given user navigates to Automation website
    When user fills the automation form
    Then gender should be selected