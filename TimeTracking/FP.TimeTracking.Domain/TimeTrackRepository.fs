namespace FP.TimeTracking.Domain

module TimeTrackRepository = 
    open System

    let private add (context: TimeTracksContext) (entity: TimeTrack) =
            context.TimeTracks.Add(entity) |> ignore
            context.SaveChanges true |> ignore
            entity

    let private delete (context: TimeTracksContext) (id: Guid) =
        let tr = context.TimeTracks.Find(id)
        context.TimeTracks.Remove(tr) |> ignore
        context.SaveChanges true |> ignore
        tr


    let private getByIssueId (context: TimeTracksContext) (issueId: Guid) =
        query {
            for tr in context.TimeTracks do
                where (tr.IssueId = issueId)
                select tr 
            } |> Seq.toList

    let private getReportedDate (context: TimeTracksContext) (issueId: Guid, createdBy: string, reportedDate: DateTime) =
        query {
            for tr in context.TimeTracks do
                where (tr.IssueId = issueId && tr.CreatedBy = createdBy && tr.ReportedDate = reportedDate)
                select tr 
            } |> Seq.toList

    let Add  = add DbContextConfigurationRoot.getContext
    let Delete  = delete DbContextConfigurationRoot.getContext
    let GetByIssueId  = getByIssueId DbContextConfigurationRoot.getContext
    let GetReportedDate = getReportedDate DbContextConfigurationRoot.getContext

