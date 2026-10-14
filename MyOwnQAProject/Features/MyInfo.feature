Feature: My Info

Background:
    Given user navigates to OrangeHRM login page
    When user logs in with username "Admin" and password "admin123"
    Then Dashboard header should be displayed

@MyInfoResumeUpload
Scenario: Update My Info and upload resume
    When user updates My Info and uploads resume