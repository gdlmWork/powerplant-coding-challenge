# powerplant-coding-challenge


## Welcome !

Below you can find the description of a coding challenge that we ask people to perform when applying
for a job in our team.

The goal of this coding challenge is to provide the applicant some insight into the business we're in and
as such provide the applicant an indication about the challenges she/he will be confronted with. Next, during the
first interview we will use the applicant's implementation as a seed to discuss all kinds of 
interesting software engineering topics.  

This is an implementation of the coding challenge performed by Glenn De la Marche

## How to build and run the API
1. Open the PowerplantAPI.sln file using Visual Studio.
2. Using Visual Studio make sure to select "IIS Express" as profile, in the toolbar at the top of the screen, and click it to run the API.
3. A browser window should open with a swagger interface.
4. Using the swagger interface click the POST operation labeled as "/Powerplant".
5. Click the "Try it out" button on the right hand side of the window.
6. In the request body, input the desired payload JSON file which to use for the calculations (as designed in the example_payloads folder).
7. Click the Execute button under the request body.
8. You should now be able to see the CURL request, as well as the Response body given in the format specified in the example_response.json file.
