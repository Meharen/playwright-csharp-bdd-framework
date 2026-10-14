Feature: Performance

Background: 
    Given user navigates to OrangeHRM login page
    When user logs in with username "Admin" and password "admin123"
    Then Dashboard header should be displayed


@PerformanceSearch
Scenario: Search Employee Performance
    When user searches employee performance