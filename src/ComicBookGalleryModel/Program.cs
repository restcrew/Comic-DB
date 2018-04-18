﻿using ComicBookGalleryModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookGalleryModel
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context())
            {
                var series1 = new Series()
                {
                    Title = "Amazing Spider-Man, The"
                };

                var series2 = new Series()
                {
                    Title = "Iron Man"
                };

                var artist1 = new Artist()
                {
                    Name = "Jorma"
                };

                var artist2 = new Artist()
                {
                    Name = "Jukka"
                };

                var artist3 = new Artist()
                {
                    Name = "Vesa"
                };

                var role1 = new Role()
                {
                    Name = "Script"

                };

                var role2 = new Role()
                {
                    Name = "Ink"
                };
             
                var comicBook1 = new ComicBook()
                {
                    Series = series1,
                    IssueNumber = 1,
                    PublishedOn = DateTime.Today,
                };
                comicBook1.AddArtist(artist1, role1);
                comicBook1.AddArtist(artist2, role2);

                var comicBook2 = new ComicBook()
                {
                    Series = series1,
                    IssueNumber = 2,
                    PublishedOn = DateTime.Today,

                };
                comicBook2.AddArtist(artist1, role1);
                comicBook2.AddArtist(artist3, role2);

                var comicBook3 = new ComicBook()
                {
                    Series = series2,
                    IssueNumber = 1,
                    PublishedOn = DateTime.Today,

                };
                comicBook3.AddArtist(artist3, role1);

                context.ComicBooks.Add(comicBook1);
                context.ComicBooks.Add(comicBook2);
                context.ComicBooks.Add(comicBook3);

                context.SaveChanges();

                var comicBooks = context.ComicBooks
                    .Include(cb => cb.Series)
                    .Include(cb => cb.Artists.Select(a => a.Artist))
                    .Include(cb => cb.Artists.Select(a => a.Role))
                    .ToList();
                foreach(var comicBook in comicBooks)
                {
                    var artistRoleNames = comicBook.Artists
                        .Select(a => $"{a.Artist.Name} - {a.Role.Name}").ToList();
                    var artistRolesDisplayText = string.Join(", ", artistRoleNames);

                    Console.WriteLine(comicBook.DisplayText + " " + artistRolesDisplayText);
                }

                Console.ReadLine();
            }
        }
    }
}