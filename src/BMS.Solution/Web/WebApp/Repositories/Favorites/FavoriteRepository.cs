﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using System.Threading.Tasks;
using WebApp.Models;
namespace WebApp.Repositories
{
/// <summary>
/// File: FavoriteRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 1/30/2021 9:49:56 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class FavoriteRepository  
    {
                 public static async Task<IEnumerable<Favorite>> GetByBookId(this IRepositoryAsync<Favorite> repository, int bookid)
          => await repository
                .Queryable()
                .Where(x => x.BookId==bookid).ToListAsync();
              
          
                 
	}
}



