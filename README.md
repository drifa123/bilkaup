# README #

## Hrókering ##
**Product Owner** Birgir Hrafn Birgisson

**Scrum Master & Programmer** Fanney Þóra Vilhjálmsdóttir (fanney83@gmail.com)

**Programmer** Bríet Konráðsdóttir (brietkonn@gmail.com)

**Programmer** Drífa Örvarsdóttir (drifa123@gmail.com)

**Programmer** Ólöf Gyða Risten Svansdóttir (olofristen@gmail.com)

## About the project ##
BEST PROJECT EVER!! - TODO!!!!!

## Install For the Project##
Install Angular on your computer:
```
npm install @angular/cli --global
```

Install Angular dependencies:
```
npm install
```

Install .NET Core:
[Download](https://github.com/dotnet/core/blob/master/release-notes/download-archives/2.0.3.md)

## To Run the Project ##
To run the client.
Be in the 'Client' directory and write:
```
ng build
npm start
// ng serve --proxy-config proxy.config.json // NOT NEEDED ANYMORE
```

To run the server.
Be in the 'Server' directory and write:
```
dotnet build
dotnet watch run
```

By doing both steps as above you can have the client and the server running and as you save your files they will automatically build and show the changes.
The localhost you should use in your browser is:
```
http://localhost:4200/
```

## To Change the Structure of the Database ##
To change the structure of the database you first need to change the Entity model for that table. When that is done you should be in
the 'Server/API' directory and write:
```
dotnet ef migrations add NameOfMigration
dotnet ef database update
```

## How To Do Stuff ##
### How to create a new component ###
Be in the 'Client/src/app' directory and write:
```
ng generate component NameOfComponent
```

### How to branch ###
To create a new branch, be on the branch you want to branch out from and write:
```
git checkout -b NameOfBranch
```
REMEMBER! Name the branch the  tory name, e.g. "Krafa3" and it should start with capital letter!!

To change to another branch write:
```
git checkout NameOfBranch
```

To merge two branches, go to the parent branch and write:
```
git merge NameOfChildBranch
```