namespace FP.TimeTracking.Domain 

open Microsoft.EntityFrameworkCore
open FP.TimeTracking.Domain

type TimeTracksContext =
    inherit DbContext
    
    new() = { inherit DbContext() }
    new(options: DbContextOptions<TimeTracksContext>) = { inherit DbContext(options) }

    override __.OnModelCreating modelBuilder = 
            TimeTracksModelBuilder.build modelBuilder
            ()

    [<DefaultValue>]
    val mutable timeTracks:DbSet<TimeTrack>
    member x.TimeTracks 
        with get() = x.timeTracks 
        and set v = x.timeTracks <- v


module DbContextConfigurationRoot = 
    open FP.TimeTracking.Core
    let configureSqlServerContext = 
        (fun () ->
            let optionsBuilder = new DbContextOptionsBuilder<TimeTracksContext>();
            optionsBuilder.UseNpgsql(AppSettingsProvider.getConnectionString()) |> ignore
            new TimeTracksContext(optionsBuilder.Options)
        )

    let getContext = configureSqlServerContext()
