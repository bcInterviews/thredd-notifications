Questions / Notes:
The test says to refactor, therefore I cannot change the observable behaviour, which means I cnanot chnage the contract,
If I could then I would ensure that the notification type was an Enum, I would also actually return the Guid of the notification if its not passed in.
What is the importance of the status, is this something that is a nice to have or is it crucial that the notificaiton is delivered.
If we need to persist the status does this take precedance over the sending of the notification?


Future Design:
Outbox pattern possibly to look persisting and performing the send
Seperate services for SMTP and SMS, API just then puts the relevant rquest on to a queue after persisting
Email templates could be used and something like Handlebars used to compose these.
Authentication and Authorization
OpenAPI
Validate settings
Better encapsulation through the stack in terms of validaiton
SMTP credentials possible pulled from an SSM
Email formatters / template building

Although advised to spend an hour on this I did go over this time allocated. I did therefore not complete all validation, html encoding (if needed) of email message, nor persistance. Unit tests Ive added one just to show an example but I would need more time.
