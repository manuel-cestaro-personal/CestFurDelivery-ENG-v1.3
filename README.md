# CestFurDelivery&#128715;&#129681;&#128230;
Cestaro Furniture Delivery Management WebApp

## Objective
CestFurDelivery is a simple web app created to manage the delivery calendar of the furniture shop Cestaro Arredamenti s.n.c. http://www.cestaroarredamenti.it/.<br>
<br>
The main target of this project is the accessibility to information on deliveries among multiple users based on their role within the company. For this purpose I have implemented the Identity framework on ASP.NET Core with a dedicated SQL Server database for user management.<br>
<br>
The web app pages mainly expose CRUD and Search operations of the delivery table. This is a very simple solution but it meets all the needs encountered together with the CEO of Cestaro Arredamenti.<br>

## Start the container
- clone the repo<br>
- ask me for a database&#128517;<br>
- `nano ~/CestFurDelivery/CestFurDelivery/CestFurDelivery.WebApp/appsettings.json` --> add connection string to the config file<br>
- `cd ~/CestFurDelivery/CestFurDelivery/`<br>
- `docker build -t cestfurdelivery:1.2.0 -f ./CestFurDelivery.WebApp/Dockerfile .` --> create the docker image<br>
- `cd ..`<br>
- `docker network create <Your_Network_Name>` --> create your docker network<br>
- `nano docker-compose.yml` --> edit the file with `<Your_Network_Name>`<br>
- `docker-compose up -d`<br>
- use `docker ps -a` to check the container

## Conclusions
Good!<br>
The customer is happy so I am happy&#128513;