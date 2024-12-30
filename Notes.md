I decided to go with the CQRS pattern for storing and retreiving data. I went with that because it seperates the read and write logic 
which makes it easier to read and maintain code. It allows us to optimize each model independently, which can lead to efficient querying and scaling. 
I used a library called Mediatr for all the CQRS stuff. It simplifies the CQRS pattern in your application and it also makes it easy to validate your 
command and query request models. 

I faced some challenges with setting up RabbitMq (It was my first time using it). I initially tried to install it on my machine and the install kept failing but docker
came to the rescue! I created and ran a docker compose file and everything worked. I decided to go with a library called MassTransit to publish and consume messages
from rabbitmq, it's easy to setup and it does all the heavy lifting for you.

# Test Results
![image](https://github.com/user-attachments/assets/905daa45-f189-4fa0-9912-e3d40e866662)

# Unit Test results
![image](https://github.com/user-attachments/assets/4cfe5768-ba13-47a6-ac1b-1017d9d0e49f)

