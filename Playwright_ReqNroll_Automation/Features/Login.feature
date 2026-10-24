Feature: Login

@Login
Scenario: Login with valid credentials and verify Dashboard
    Given user navigates to OrangeHRM login page
    When user logs in with username "Admin" and password "admin123"
    Then Dashboard header should be displayed