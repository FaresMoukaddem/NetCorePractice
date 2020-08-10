using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechParts.API.Models;

public class Seeder
{
    public static void SeedData(DbContext context)
    {
        System.Console.WriteLine("Seeding data...");

        Random r = new Random();

        string[] names = {"John","Jan","Jerry","Fin","Jake","Ron","Stan","Buck","Carl","Mark","Brad","Kyle","Tim","Steve","Nick","Eric"};

        foreach(string name in names)
        {
            User u = new User();
            u.username = name;
        }
    }
}