# Docker
To run this app via docker open base directory, and run 
```sh
docker-compose up --build
```
after that navigate to http://localhost:8081/ Make sure that you do not have any applications that are using ports 8081, 5433, 5152 or you would get an error

# Starting Frontend localy
To start frontend first thing you need to go to `.\First-App.Client\`  and then install packages via
```sh
npm install
```
then run the application in development mode
```sh
ng serve
```
# Starting Backend localy

to run backend first thing you need to specify your connection string `CONNECTION_STRING` to the database. You can do it by adding a .env file with the connection string to your database to .\Backend\First-App.Server\ 
Then run 
```sh
dotnet restore
```
to install dependecies and packages.
Then run your application
```sh
#to run in the dev mode
dotnet run --environment Development
```
after your api will be available at http://localhost:5152/
you can also see the endpoints at Swagger at http://localhost:5152/swagger/index.html
