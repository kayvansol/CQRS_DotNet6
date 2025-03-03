# Dot Net & CQRS
<br />
CQRS Pattern & Clean Architecture Design In Dot Net 6.0 based Api Project with Test units in xUnit tool & Duende Identity Server & RabbitMQ
<br /><br />
Project Structure :
<br /><br />

![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/structure.png?raw=true)

Swagger page :

![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/swagger.png?raw=true)

Duende Identity Server :

![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/identity.png?raw=true)

![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/identity2.png?raw=true)

<br />
RabbitMQ with Docker :

```
docker pull rabbitmq:4.0.7-management

docker run -d --hostname myrabbit --name rabbit -p 5672:5672 -p 5673:5673 -p 15672:15672 rabbitmq:4.0.7-management
```
![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/rabbitContainer.png?raw=true)

Order Message has been sent to rabbitmq :

![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/rabbit.png?raw=true)
