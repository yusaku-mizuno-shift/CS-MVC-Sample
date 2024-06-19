using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TestDbContext : DbContext
{
	public TestDbContext(DbContextOptions<TestDbContext> options)
		: base(options)
	{
	}

	public DbSet<MvcMovie.Models.Movie> Movie { get; set; } = default!;
}