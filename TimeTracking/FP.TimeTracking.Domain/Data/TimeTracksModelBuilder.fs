namespace FP.TimeTracking.Domain

module TimeTracksModelBuilder = 
    open Microsoft.EntityFrameworkCore

    let build (modelBuilder : ModelBuilder) = 
        modelBuilder.Entity<TimeTrack>().ToTable("issue_time_tracking") |> ignore

        modelBuilder.Entity<TimeTrack>()
            .Property(fun timeTrack -> timeTrack.Id)
            .HasColumnName("id") |> ignore

        modelBuilder.Entity<TimeTrack>()
            .Property(fun timeTrack -> timeTrack.IssueId)
            .HasColumnName("issue_id")
            .IsRequired() |> ignore

        modelBuilder.Entity<TimeTrack>()
            .Property(fun timeTrack -> timeTrack.TimeMinutes)
            .HasColumnName("time_minutes")
            .IsRequired() |> ignore

        modelBuilder.Entity<TimeTrack>()
            .Property(fun timeTrack -> timeTrack.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired() |> ignore

        modelBuilder.Entity<TimeTrack>()
            .Property(fun timeTrack -> timeTrack.ReportedDate)
            .HasColumnName("reported_date")
            .IsRequired() |> ignore

        modelBuilder.Entity<TimeTrack>()
            .Property(fun timeTrack -> timeTrack.CreatedBy)
            .HasColumnName("created_by")
            .IsRequired() |> ignore

        modelBuilder.Entity<TimeTrack>()
            .Property(fun timeTrack -> timeTrack.Description)
            .HasColumnName("description") |> ignore
        ()