Feature: Activities endpoint POST

Background:
Given the current time is 2021-03-03T10:00:00Z

Scenario: New activity is uploaded
Given there are no activities in the database 
When new activity is created with following properties
| Property            | Value               |
| ActivityId          | 1                   |
| Name                | Cycling             |
| Description         | Just me and my bike |
| DistanceKm          | 10                  |
| DurationSeconds     | 20                  |
| AverageSpeedKmph    | 0.5                 |
Then there are following activities in the database
| ActivityId | Name    | Description         | DistanceKm | DurationSeconds | AverageSpeedKmph |
| 1          | Cycling | Just me and my bike | 10         | 20              | 0.5              |
And a JSON file is uploaded with name '1' and with content equivalent to
"""
{
    "ActivityId": "1",
    "Name": "Cycling",
    "Description": "Just me and my bike",
    "DistanceKm": 10,
    "DurationSeconds": 20,
    "AverageSpeedKmph": 0.5
}
"""

Scenario: Average speed is calculated for activities
Scenario: Uploading activity with ID already taken by another activity is not allowed
