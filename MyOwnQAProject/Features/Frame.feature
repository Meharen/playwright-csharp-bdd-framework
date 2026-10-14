Feature: Frames

@NestedFrames
Scenario: Verify text in nested frames
    Given user navigates to The Internet website
    When user opens Nested Frames
    Then user verifies LEFT text in left frame
    And user verifies MIDDLE text in middle frame