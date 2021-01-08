# EpicUniversity

A demo application to demonstrate the .NET 5 and Entity Framework 5

## Getting started

Apply migrations

```
$ dotnet ef database update --project EpicUniversity
```

## Reference

Add migration

```
$ dotnet ef migrations add "Name" --project EpicUniversity
```

Queries

```
select * from Courses
select * from CourseLabs

select * from Students
select * from CourseStudent
select * from Grades

select * from Professors
```
