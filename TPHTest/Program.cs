// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using TPHTest;

Console.WriteLine("Hello, World!");
await using (var ctx = new TestDbContext())
{
    await ctx.Database.EnsureDeletedAsync();
    await ctx.Database.EnsureCreatedAsync();
}

await using (var ctx = new TestDbContext())
{
    var parentId = Guid.NewGuid();

    var d1 = new ChildDerived()
    {
        Id = Guid.NewGuid(),
        ParentId = parentId,
        Misc = new Misc()
        {
            Message = new Message() {Id = Guid.NewGuid()}
        }
    };
    var c1 = new Child()
    {
        Id = Guid.NewGuid(),
        ParentId = parentId
    };

    var parent = new Parent()
    {
        Id = parentId,
        Childs = [d1, c1]
    };

    ctx.Parents.Add(parent);
    await ctx.SaveChangesAsync();
}

await using (var ctx = new TestDbContext())
{
    // No problem with 'AsNoTracking'
    var parent = await ctx.Parents.Include(p => p.Childs).FirstOrDefaultAsync();
}