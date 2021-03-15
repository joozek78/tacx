Feature: Activities endpoint smoke test

Scenario: activity POST endpoint creates new entry in the database
Given there is a request with the following properties
| Property    | Type   | Value               |
| Name        | String | Cycling             |
| Description | String | Just me and my bike |
| Distance    | Double | 10                  |
| Duration    | Double | 20                  |
| AvgSpeed    | Double | 0.5                 |
And the request contains a generated ID in property 'ActivityId'
When a request is sent to POST /api/activities
Then the response code is OK
And a document exists in container 'Activity' with generated ID
